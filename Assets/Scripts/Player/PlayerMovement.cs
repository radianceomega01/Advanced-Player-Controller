using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputEventsSO inputEventSO;
    [SerializeField] Transform rigTransform;
    [SerializeField] Transform footTransform;
    [SerializeField] Transform kneeTransform;
    [SerializeField] Transform hangTransform;

    //public values to be shared with player states
    [SerializeField] public float walkingSpeed = 1.75f;
    [SerializeField] public float runningSpeed = 4f;
    [SerializeField] public float jumpHeight = 1.5f;
    [SerializeField] public float slidingSpeedAddition = 3f;
    [SerializeField] public float slidingDeAccelerationMultiplier = 0.3f;
    [SerializeField] public float gravity = -15f;
    [SerializeField] public float minVaultableHeight = 1f;
    [SerializeField] public float MaxVaultableHeight = 4f;
    [SerializeField] public float vaultTimeInSecs = 1f;

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
    Transform currentLookAtTransform;

    private const float JOYSTICK_AXIS_VALUE_ON_MAX_X_AND_Y = 0.5f;
    public PlayerActions.PlayerInputActions PlayerInput { get; private set; }
    public CharacterController CharacterController { get; private set; }
    public int JumpCount { get; set; }
    public float VerticalVelocity { get; set; }
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
        layerMask = 1 << LayerMask.NameToLayer("Ground");
    }

    private void OnEnable()
    {
        playerActions.Enable();
        inputEventSO.ChangePlayerLookAtEvent.AddListener(ChangeLookAtTransform);
    }
    private void OnDisable()
    {
        playerActions.Disable();
        inputEventSO.ChangePlayerLookAtEvent.RemoveListener(ChangeLookAtTransform);
    }

    private void Start()
    {
        overlapSphereRadius = CharacterController.radius - 0.1f;
        hangHalfExtents = new Vector3(CharacterController.radius, 0.1f, CharacterController.radius);

        ChangeLookAtTransform(LookAtType.Player);
        ChangeState(StateFactory.GetPlayerState(typeof(IdleState), this));
    }
    private void FixedUpdate()
    {
        state.PhysicsProcess();
        inputEventSO.PlayerPositionEvent.Invoke(new Vector3(transform.position.x, currentLookAtTransform.position.y, transform.position.z));
    }

    void Update()
    {
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

        SetAnimationWithFloatVal("InpX", moveInput.x);
        SetAnimationWithFloatVal("InpY", moveInput.y);

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
    public bool DidPalmDetectObject()
    {
        return Physics.CheckBox(hangTransform.position, hangHalfExtents, Quaternion.identity, layerMask);
    }
    public bool DidDetectAVaultableObject()
    {
        Collider collider = GetVaultableObjectColldier();
        if (collider == null || 
            collider.bounds.size.y < minVaultableHeight ||
            collider.bounds.size.y > MaxVaultableHeight)
            return false;

        return true;
    }
    public float GetVaultableObjectHeight()
    {
        Collider collider = GetVaultableObjectColldier();
        if(collider == null)
            return 0f;
        return collider.bounds.size.y;
    }
    private Collider GetVaultableObjectColldier()
    {
        Collider[] colliders = Physics.OverlapSphere(kneeTransform.position + transform.forward * CharacterController.radius, 0.2f, layerMask);
        if (colliders.Length > 0)
            return colliders[0];
        else
            return null;
    }

    private void ChangeLookAtTransform(LookAtType lookAtType)
    {
        currentLookAtTransform = lookAtType == LookAtType.Player? transform: rigTransform;
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
        if(fadeDuration > 0)
            animator.CrossFade(name, fadeDuration);
    }
    public InputEventsSO GetInputSO() =>inputEventSO;

    private void SetAnimationWithFloatVal(string name, float value) =>animator.SetFloat(name, value);

    public void AnimComplete() => OnAnimComplete?.Invoke();

}

public enum MovementInputType
{ 
    Idle,
    Moving,
    Sprinting
}
public enum LookAtType
{
    Player,
    Rig
}
