
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GroundedState : PlayerState
{
    Vector3 playerForwardDir;
    Vector3 playerRightDir;
    LayerMask layerMask;
    Collider[] colliders;

    protected float pressTime;
    protected Vector2 moveInput;
    protected Vector3 movementDir;

    public GroundedState(Player player) : base(player) { }

    public override void OnEnter()
    {
        playerActions.PlayerInput.Jump.performed += SwitchToJumpingState;

        layerMask = 1 << 3;

        if (player.GetPreviousState() != null && player.GetPreviousState().GetType() == typeof(FallingState))
        {
            if (player.JumpCount == 1)
                player.SetAnimation("Landing");
            else
                player.SetAnimation("DLanding");

            player.JumpCount = 0;
        }
    }

    public override void PhysicsProcess()
    {
        colliders =  Physics.OverlapSphere(player.GetFootPos(), 0.1f, layerMask);
        if (colliders.Length == 0)
        {
            player.SetState(StateFactory.GetPlayerState(typeof(FallingState), player)); 
        }
    }

    public override void Process()
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

    public override void OnExit() 
    {
        playerActions.PlayerInput.Jump.performed -= SwitchToJumpingState;
    }

    private void SwitchToJumpingState(InputAction.CallbackContext ctx)
    {
        player.SetState(StateFactory.GetPlayerState(typeof(JumpedState), player));
    }

    protected void SwitchToSlidingState(InputAction.CallbackContext ctx)
    {
        player.SetState(StateFactory.GetPlayerState(typeof(SlidingState), player));
    }
}
