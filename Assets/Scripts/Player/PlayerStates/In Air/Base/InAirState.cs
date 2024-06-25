
using UnityEngine.InputSystem;

public abstract class InAirState : PlayerState
{

    public InAirState(Player player) : base(player) { }

    public override void OnEnter() 
    {
        playerActions.PlayerInput.Jump.performed += SwitchToJumpingState;
    }

    public override void PhysicsProcess() { }

    public override void Process() { }

    public override void OnExit() 
    {
        playerActions.PlayerInput.Jump.performed -= SwitchToJumpingState;
    }

    private void SwitchToJumpingState(InputAction.CallbackContext ctx)
    {
        if (player.JumpCount == 2)
            return;
        player.SetState(StateFactory.GetPlayerState(typeof(JumpedState), player));
    }
}
