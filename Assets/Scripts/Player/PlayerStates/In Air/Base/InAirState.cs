
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InAirState : PlayerState
{
    public static int jumpCount;
    public InAirState(Player player) : base(player) { }

    public override void OnEnter()
    {
        playerActions.PlayerInput.Jump.performed += SwitchToJumpingState;
    }

    public override void PhysicsProcess()
    {
        
    }

    public override void Process()
    {
        
    }

    public override void OnExit()
    {
        playerActions.PlayerInput.Jump.performed -= SwitchToJumpingState;
    }

    private void SwitchToJumpingState(InputAction.CallbackContext ctx)
    {
        if (jumpCount == 2)
            return;
        player.SetState(new JumpedState(player));
    }
}
