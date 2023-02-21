using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : GroundedState
{
    float runningSpeed = 250f;

    public RunningState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        player.SetAnimation("Run");
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();

        player.GetRigidBody().velocity = movementDir * runningSpeed * Time.fixedDeltaTime;
    }

    public override void Process()
    {
        base.Process();

        if (moveInput.magnitude == 0f)
            player.SetState(StateFactory.GetIdleState(player));
        if (pressTime <= 0.1f)
            player.SetState(StateFactory.GetWalkingState(player));
    }
}
