
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GroundedState : PlayerState
{
    Vector3 playerForwardDir;
    Vector3 playerRightDir;

    protected float pressTime;
    protected Vector2 moveInput;
    protected Vector3 movementDir;

    public GroundedState(Player player) : base(player) { }

    public override void OnEnter()
    {
        playerActions.PlayerInput.Jump.performed += SwitchToJumpingState;

        if (player.GetPreviousState() != null && player.GetPreviousState().GetType() == typeof(InAirState))
        {
            if (player.JumpCount == 1)
                player.SetAnimation("Landing");
            else
                player.SetAnimation("DLanding");
        }
        player.JumpCount = 0;
    }

    public override void PhysicsProcess() { }

    public override void Process()
    {
        GetPlayerMovInput();
        player.CheckAndMoveToFallingState();
    }

    public override void OnExit() 
    {
        playerActions.PlayerInput.Jump.performed -= SwitchToJumpingState;
    }

    private void SwitchToJumpingState(InputAction.CallbackContext ctx)
    {
        player.SetState(StateFactory.GetPlayerState(typeof(JumpedState), player));
    }

    private void GetPlayerMovInput()
    {
        moveInput = playerActions.PlayerInput.Move.ReadValue<Vector2>();
        pressTime = playerActions.PlayerInput.Sprint.ReadValue<float>();

        player.SetAnimation("InpX", moveInput.x);
        player.SetAnimation("InpY", moveInput.y);

        playerRightDir = player.transform.right;
        playerRightDir.y = 0;
        playerRightDir.Normalize();

        playerForwardDir = player.transform.forward;
        playerForwardDir.y = 0;
        playerForwardDir.Normalize();

        movementDir = (playerRightDir * moveInput.x + playerForwardDir * moveInput.y).normalized;
    }

    protected void SwitchToSlidingState(InputAction.CallbackContext ctx)
    {
        player.SetState(StateFactory.GetPlayerState(typeof(SlidingState), player));
    }
}
