using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : GroundedState
{
    Vector2 movement;
    float pressTime;

    public RunningState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void PhysicsProcess()
    {

    }

    public override void Process()
    {
        movement = playerActions.PlayerInput.Move.ReadValue<Vector2>();
        pressTime = playerActions.PlayerInput.Sprint.ReadValue<float>();

        if (movement.magnitude == 0f)
            player.ChangeState(new IdleState(player));
        if (pressTime <= 0.1f)
            player.ChangeState(new WalkingState(player));
    }
}
