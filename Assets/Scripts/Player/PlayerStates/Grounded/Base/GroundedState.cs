
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GroundedState : BaseMovementState
{
    public GroundedState(PlayerMovement player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
        player.JumpCount = 0; 
        player.PlayerInput.Jump.performed += SwitchToJumpingState;
        player.OnMovementInputTypeChanged += MovementInputTypeChanged;

        if (player.GetPreviousState() != null && player.GetPreviousState().GetType() == typeof(FallingState))
        {
            if (player.JumpCount <= 1)
                player.SetAnimation("Landing");
            else
                player.SetAnimation("DLanding");
        }
    }

    public override void PhysicsProcess() 
    {
        base.PhysicsProcess();
        CheckAndMoveToFallingState();
    }

    public override void OnExit() 
    {
        base.OnExit();
        player.PlayerInput.Jump.performed -= SwitchToJumpingState;
        player.OnMovementInputTypeChanged -= MovementInputTypeChanged;
    }
    protected virtual void MovementInputTypeChanged() => StateFactory.GetGroundedStateBasedOnMovementInputType(player);

    private void SwitchToJumpingState(InputAction.CallbackContext ctx)
    {
        player.ChangeState(StateFactory.GetPlayerState(typeof(JumpedState), player));
    }

    protected void SwitchToSlidingState(InputAction.CallbackContext ctx)
    {
        player.ChangeState(StateFactory.GetPlayerState(typeof(SlidingState), player));
    }
    private void CheckAndMoveToFallingState()
    {
        if (!player.IsGrounded())
            player.ChangeState(StateFactory.GetPlayerState(typeof(FallingState), player));
    }
}
