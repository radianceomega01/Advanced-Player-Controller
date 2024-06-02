
using UnityEngine;

public class FallingState : InAirState
{
    LayerMask layerMask;
    Collider[] colliders;

    public FallingState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
        layerMask = 1 << 3;
        if (player.JumpCount <= 1)
            TransitionToFalling();
        else
            player.SetAnimationTrigger("DFall");
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
        colliders = Physics.OverlapSphere(player.GetFootPos(), 0.1f, layerMask);
        if (colliders.Length > 0)
        {
            player.SetState(StateFactory.GetPlayerState(typeof(IdleState), player));
        }
    }

    public override void Process()
    {
        base.Process();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    private void TransitionToFalling()
    {
        if (player.GetPreviousState().GetType().IsSubclassOf(typeof(GroundedState)))
            player.SetAnimation("Falling");
        else
            player.SetAnimationTrigger("Fall");
    }
}
