
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InAirState : PlayerState
{
    LayerMask layerMask;
    Collider[] colliders;

    public static int jumpCount;
    public InAirState(Player player) : base(player) { }

    public override void OnEnter()
    {
        layerMask = 1 << 3;
        playerActions.PlayerInput.Jump.performed += SwitchToJumpingState;
    }

    public override void PhysicsProcess()
    {
        colliders = Physics.OverlapSphere(player.GetFootPos(), 0.1f, layerMask);
        if (colliders.Length > 0)
        {
            player.SetState(StateFactory.GetIdleState(player));
        }
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
