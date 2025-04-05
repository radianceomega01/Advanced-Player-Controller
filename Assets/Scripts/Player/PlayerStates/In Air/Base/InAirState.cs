
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InAirState : BaseMovementState
{
    public InAirState(PlayerMovement player) : base(player) { }

    public override void OnEnter() 
    {
        base.OnEnter();
        player.PlayerInput.Jump.performed += SwitchToJumpingState;
    }

    public override void PhysicsProcess() 
    {
        base.PhysicsProcess();
        player.CharacterController.Move(Time.fixedDeltaTime * (player.MovementDir * player.GetMovementSpeed() + Vector3.up * player.VerticalVelocity));
    }

    public override void Process() 
    {
        base.Process();
        player.VerticalVelocity += player.gravity * Time.deltaTime;
    }

    public override void OnExit() 
    {
        base.OnExit();
        player.PlayerInput.Jump.performed -= SwitchToJumpingState;
    }

    private void SwitchToJumpingState(InputAction.CallbackContext ctx)
    {
        if (player.JumpCount == 2)
            return;
        player.ChangeState(StateFactory.GetPlayerState(typeof(JumpedState), player));
    }

}
