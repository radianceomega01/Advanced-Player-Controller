using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : GroundedState
{
    Vector2 movement;
    float pressTime;
    float runningSpeed = 250f;

    public RunningState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        player.SetAnimation("Run");
    }

    public override void PhysicsProcess()
    {
        player.GetRigidBody().velocity = new Vector3(player.transform.forward.x * movement.x, 0f, player.transform.forward.z * movement.y)
            * runningSpeed * Time.fixedDeltaTime;
    }

    public override void Process()
    {
        movement = playerActions.PlayerInput.Move.ReadValue<Vector2>();
        pressTime = playerActions.PlayerInput.Sprint.ReadValue<float>();

        if (movement.magnitude == 0f)
            player.SetState(StateFactory.GetIdleState(player));
        if (pressTime <= 0.1f)
            player.SetState(StateFactory.GetWalkingState(player));
    }
}
