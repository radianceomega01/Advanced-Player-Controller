using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingState : GroundedState
{
    public SlidingState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
        player.SetPlayerCollider(1);
        player.OnAnimComplete += ChangeState;
        player.SetAnimation("Sliding");
        player.GetRigidBody().AddForce(player.transform.forward * player.slideForce, ForceMode.VelocityChange);
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
        /*if (player.GetRigidBody().velocity.magnitude <= 0.35f)
            ChangeState();*/
    }

    public override void Process()
    {
        base.Process();
    }

    private void ChangeState() => player.SetState(StateFactory.GetPlayerState(typeof(IdleState), player));
    public override void OnExit()
    {
        base.OnExit();
        player.SetPlayerCollider(0);
        player.OnAnimComplete -= ChangeState;
    }

}
