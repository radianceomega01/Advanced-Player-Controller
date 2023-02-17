using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : GroundedState
{
    Vector2 movement;
    float pressTime;
    float walkingSpeed = 100f;

    public WalkingState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        player.SetAnimation("Walk");

        playerActions.PlayerInput.Sprint.performed += _ =>
        {
            if (movement.x == 0 && movement.y == 1)
            {
                player.SetState(StateFactory.GetRunningState(player));
            }
        };
        playerActions.PlayerInput.CrouchSlide.performed += _ => player.SetState(StateFactory.GetCrouchingState(player));
    }

    public override void PhysicsProcess()
    {
        player.GetRigidBody().velocity = new Vector3(movement.x, 0f, movement.y) * walkingSpeed * Time.fixedDeltaTime;
    }

    public override void Process()
    {
        movement = playerActions.PlayerInput.Move.ReadValue<Vector2>();
        pressTime = playerActions.PlayerInput.Sprint.ReadValue<float>();

        player.SetAnimation("InpX", movement.x);
        player.SetAnimation("InpY", movement.y);

        if (movement.magnitude == 0f)
            player.SetState(StateFactory.GetIdleState(player));
    }
}
