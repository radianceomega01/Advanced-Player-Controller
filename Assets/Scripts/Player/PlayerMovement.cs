using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputEventsSO inputEventSO;
    [SerializeField] Transform footTransform;

    //public values to be shared with player states
    [SerializeField] public float walkingSpeed = 1.75f;
    [SerializeField] public float runningSpeed = 4f;
    [SerializeField] public float jumpHeight = 1.5f;
    [SerializeField] public float slidingSpeedAddition = 3f;
    [SerializeField] public float slidingDeAccelerationMultiplier = 0.3f;
    [SerializeField] public float gravity = -15f;

    /*[Header("Colliders")]
    [SerializeField] Collider[] playerColliders;*/
    LayerMask layerMask;

    PlayerActions playerActions;
    BaseMovementState state;
    BaseMovementState previousState;
    Animator animator;
    Vector2 moveInput;
    Vector3 playerForwardDir;
    Vector3 playerRightDir;
    float sprintPressTime;
    Vector3 checkBoxHalfExtents;
    MovementInputType previousType;

    public float JoystickAxisValueOnMaxXAndY { get; private set; }
    public PlayerActions.PlayerInputActions PlayerInput { get; private set; }
    public CharacterController CharacterController { get; private set; }
    public int JumpCount { get; set; }
    public float VerticalVelocity { get; set; }
    public float PreviousYPos { get; private set; }
    public MovementInputType MovementInputType { get; private set; }
    public Vector3 MovementDir { get; private set; }

    public event Action OnAnimComplete;
    public event Action OnMovementInputTypeChanged;

    void Awake()
    {
        playerActions = new PlayerActions();
        PlayerInput = playerActions.PlayerInput;
        animator = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();
        layerMask = 1 << LayerMask.NameToLayer("Ground");
        JoystickAxisValueOnMaxXAndY = Mathf.Sqrt(2) / 2;
        checkBoxHalfExtents = new Vector3(CharacterController.radius, 0.1f, CharacterController.radius);
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void Start()
    {
        ChangeState(StateFactory.GetPlayerState(typeof(IdleState), this));
    }
    private void FixedUpdate()
    {
        state.PhysicsProcess();
    }

    void Update()
    {
        GetPlayerMovInput();
        SetPlayerMovementType();
        state.Process();
        inputEventSO.PlayerPositionEvent.Invoke(transform.position);
    }

    private void LateUpdate()
    {
        PreviousYPos = transform.position.y;
        state.LateProcess();
    }
    public void ChangeState(BaseMovementState newState)
    {
        previousState = state;
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

        SetAnimation("InpX", moveInput.x);
        SetAnimation("InpY", moveInput.y);

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
        return (moveInput.magnitude > 0.9f && moveInput.y > JoystickAxisValueOnMaxXAndY);
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
        return Physics.CheckBox(footTransform.position, checkBoxHalfExtents, Quaternion.identity,layerMask);
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
    private RaycastHit RayCastHitToBottom()
    {
        RaycastHit hitInfo;
        Physics.Raycast(footTransform.position, Vector3.down, out hitInfo, 10f);
        return hitInfo;
    }

    public BaseMovementState GetPreviousState() => previousState;

    public void SetAnimation(string name) => animator.Play(name);
    public void SetAnimationTrigger(string name) => animator.SetTrigger(name);
    public void ResetAnimationTrigger(string name) => animator.ResetTrigger(name);
    public void SetAnimation(string name, float value) =>animator.SetFloat(name, value);

    public void AnimComplete() => OnAnimComplete?.Invoke();

    private void OnDisable()
    {
        playerActions.Disable();
    }
}

public enum MovementInputType
{ 
    Idle,
    Moving,
    Sprinting
}
