
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GroundedState : BaseMovementState
{
    public GroundedState(PlayerMovement player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
        player.JumpCount = 0; 
        player.PlayerInput.Jump.performed += SwitchStateOnJumpInput;
        player.OnMovementInputTypeChanged += MovementInputTypeChanged;
    }

    public override void PhysicsProcess() 
    {
        base.PhysicsProcess();
        CheckAndMoveToFallingState();
    }

    public override void OnExit() 
    {
        base.OnExit();
        player.PlayerInput.Jump.performed -= SwitchStateOnJumpInput;
        player.OnMovementInputTypeChanged -= MovementInputTypeChanged;
    }
    protected virtual void MovementInputTypeChanged() => player.ChangeState(StateFactory.GetGroundedStateBasedOnMovementInputType(player));

    private void SwitchStateOnJumpInput(InputAction.CallbackContext ctx)
    {
        if (player.DidDetectAVaultableObject() )
            player.ChangeState(StateFactory.GetPlayerState(typeof(VaultingState), player));
        else
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
