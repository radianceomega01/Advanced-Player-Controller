using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : GroundedState
{
    float walkingSpeed = 100f;
    public WalkingState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        player.SetAnimation("Walk");

        playerActions.PlayerInput.Sprint.performed += _ =>
        {
            if (moveInput.x == 0 && moveInput.y == 1)
            {
                player.SetState(StateFactory.GetRunningState(player));
            }
        };
        playerActions.PlayerInput.CrouchSlide.performed += _ => player.SetState(StateFactory.GetCrouchingState(player));
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();

        player.GetRigidBody().velocity = movementDir * walkingSpeed * Time.fixedDeltaTime;
    }

    public override void Process()
    {
        base.Process();

        if (moveInput.magnitude == 0f)
            player.SetState(StateFactory.GetIdleState(player));
    }
}
