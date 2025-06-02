using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] EventsSO inputEventSO;
    [SerializeField] ReferencesSO referencesSO;
    [SerializeField] Transform footTransform;
    [SerializeField] Transform kneeTransform;

    //public values to be shared with player states
    [SerializeField] public float walkingSpeed = 1.75f;
    [SerializeField] public float runningSpeed = 4f;
    [SerializeField] public float jumpHeight = 1.5f;
    [SerializeField] public float slidingSpeedAddition = 3f;
    [SerializeField] public float slidingDeAccelerationMultiplier = 0.3f;
    [SerializeField] public float gravity = -15f;
    [SerializeField] public VaultDataSO stepOverData;
    [SerializeField] public VaultDataSO smallVaultData;
    [SerializeField] public VaultDataSO largeVaultData;

    /*[Header("Colliders")]
    [SerializeField] Collider[] playerColliders;*/
    LayerMask layerMask;

    PlayerActions playerActions;
    BaseMovementState state;
    Animator animator;
    Vector2 moveInput;
    Vector3 playerForwardDir;
    Vector3 playerRightDir;
    float sprintPressTime;
    float overlapSphereRadius;
    MovementInputType previousType;
    Vector3 hangHalfExtents;
    Vector3 instantaneousVaultHitPoint;

    private const float JOYSTICK_AXIS_VALUE_ON_MAX_X_AND_Y = 0.5f;
    public PlayerActions.PlayerInputActions PlayerInput { get; private set; }
    public CharacterController CharacterController { get; private set; }
    public int JumpCount { get; set; }
    public float VerticalVelocity { get; set; }
    public float InstantaneousVaultHeight{ get; set; }
    public Vector3 PreviousPos { get; private set; }
    public MovementInputType MovementInputType { get; private set; }
    public Vector3 MovementDir { get; private set; }
    public BaseMovementState PreviousState{ get; private set; }

    public event Action OnAnimComplete;
    public event Action OnMovementInputTypeChanged;

    void Awake()
    {
        playerActions = new PlayerActions();
        PlayerInput = playerActions.PlayerInput;
        animator = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();
        layerMask = 1 << LayerMask.NameToLayer(NamingUtility.Ground);
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }
    private void OnDisable()
    {
        playerActions.Disable();
    }

    private void Start()
    {
        overlapSphereRadius = CharacterController.radius - 0.1f;
        hangHalfExtents = new Vector3(CharacterController.radius, 0.1f, CharacterController.radius);

        ChangeState(StateFactory.GetPlayerState(typeof(IdleState), this));
    }
    private void FixedUpdate()
    {
        state.PhysicsProcess();
    }

    void Update()
    {
        if(referencesSO != null)
            referencesSO.PlayerPosition = transform.position;
        GetPlayerMovInput();
        SetPlayerMovementType();
        state.Process();
    }

    private void LateUpdate()
    {
        PreviousPos = transform.position;
        state.LateProcess();
    }
    public void ChangeState(BaseMovementState newState)
    {
        PreviousState = state;
        if(state != null)
            state.OnExit();
        state = newState;
        state.OnEnter();
    }
    private void GetPlayerMovInput()
    {
        moveInput = playerActions.PlayerInput.Move.ReadValue<Vector2>();

        playerRightDir = transform.right;
        playerRightDir.y = 0;
        playerRightDir.Normalize();

        playerForwardDir = transform.forward;
        playerForwardDir.y = 0;
        playerForwardDir.Normalize();

        MovementDir = (playerRightDir * moveInput.x + playerForwardDir * moveInput.y).normalized;
    }

    private void SetPlayerMovementType()
    {
        previousType = MovementInputType;
        moveInput = playerActions.PlayerInput.Move.ReadValue<Vector2>();
        sprintPressTime = playerActions.PlayerInput.Sprint.ReadValue<float>();

        SetAnimationWithFloatVal(NamingUtility.InpX, moveInput.x);
        SetAnimationWithFloatVal(NamingUtility.InpY, moveInput.y);

        if (IsSprintingTypeInput())
            MovementInputType = MovementInputType.Sprinting;
        else if (IsMovingTypeInput())
            MovementInputType = MovementInputType.Moving;
        else
            MovementInputType = MovementInputType.Idle;
        if (previousType != MovementInputType)
            OnMovementInputTypeChanged?.Invoke();
    }
    private bool IsSprintingTypeInput()
    {
#if (PLATFORM_ANDROID && !UNITY_EDITOR)
        return (moveInput.magnitude > 0.9f && moveInput.y > JOYSTICK_AXIS_VALUE_ON_MAX_X_AND_Y);
#else
        return (sprintPressTime >= 0.1f && moveInput.y > 0);
#endif
    }
    private bool IsMovingTypeInput()
    {
        return (Mathf.Abs(moveInput.x) > 0 || Mathf.Abs(moveInput.y) > 0);
    }

    public void SetVerticalVelocityWithHorizontalVelocity(float horizontalVelocity)
    {
        VerticalVelocity = -Mathf.Tan(CharacterController.slopeLimit * Mathf.Deg2Rad) * horizontalVelocity;
    }
    public bool IsGrounded()
    {
        return Physics.CheckSphere(footTransform.position, overlapSphereRadius, layerMask);
    }
    public bool DidDetectAVaultableObject()
    {
        RaycastHit touchingRaycastHit;
        if (IsTouchingAnObject(out touchingRaycastHit))
        {
            if (IsAVaultableObject(out float hitPointY))
            {
                transform.rotation = Quaternion.LookRotation(-touchingRaycastHit.normal);
                instantaneousVaultHitPoint = new Vector3(touchingRaycastHit.point.x, hitPointY, touchingRaycastHit.point.z);
                return true;
            }
        }
        InstantaneousVaultHeight = 0f;
        return false;
    }
    private bool IsTouchingAnObject(out RaycastHit hit)
    {
        return Physics.Raycast(kneeTransform.position, transform.forward, out hit, 1f, layerMask);
    }
    private bool IsAVaultableObject(out float hitPointY)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + (transform.forward * (CharacterController.radius + 1f)) + (transform.up * 10f), Vector3.down, out hit, 10f, layerMask))
        {
            InstantaneousVaultHeight = hit.point.y - transform.position.y;
            Debug.DrawRay(hit.point, Vector3.up, Color.blue);
            if (InstantaneousVaultHeight >= stepOverData.MinHeight && InstantaneousVaultHeight <= largeVaultData.MaxHeight)
            {
                hitPointY = hit.point.y;
                return true;
            }
        }
        InstantaneousVaultHeight = 0f;
        hitPointY = 0f;
        return false;
    }
    public void ModifyVaultPointOnZAxis(float zOffset)
    {
        instantaneousVaultHitPoint += transform.forward * zOffset;
    }

    public float GetMovementSpeed()
    {
        if (MovementInputType == MovementInputType.Sprinting)
            return runningSpeed;
        else if (MovementInputType == MovementInputType.Moving)
            return walkingSpeed;
        else
            return 0f;
    }

    public void SetAnimation(string name, float fadeDuration = 0.2f)
    {
        animator.Play(name);
        if (fadeDuration > 0)
            animator.CrossFadeInFixedTime(name, fadeDuration);
    }

    public void SetAnimationMatchTarget(AvatarTarget targetBodyPart,
        Vector3 weight, float startNormalizedTime, float targetNormalizedTime)
    {
        animator.MatchTarget(instantaneousVaultHitPoint, transform.rotation, targetBodyPart, new MatchTargetWeightMask(weight, 0), startNormalizedTime, targetNormalizedTime);
    }
    public EventsSO GetInputSO() => inputEventSO;
    public void ToggleAnimatorRootMotion(bool value) => animator.applyRootMotion = value;
    public void ToggleCharacterController(bool value) => CharacterController.enabled = value;

    private void SetAnimationWithFloatVal(string name, float value) =>animator.SetFloat(name, value);

    public void AnimComplete() => OnAnimComplete?.Invoke();

}

public enum MovementInputType
{ 
    Idle,
    Moving,
    Sprinting
}
