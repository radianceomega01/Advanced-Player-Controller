using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : GroundedState
{
    Vector2 movement;
    float pressTime;
    float walkingSpeed = 2f;

    public WalkingState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        playerActions.PlayerInput.Sprint.performed += _ => player.ChangeState(new RunningState(player));
        playerActions.PlayerInput.CrouchSlide.performed += _ => player.ChangeState(new CrouchingState(player));
    }

    public override void PhysicsProcess()
    {
        player.GetRigidBody().velocity = new Vector3(movement.x, 0f, movement.y) * walkingSpeed;
    }

    public override void Process()
    {
        movement = playerActions.PlayerInput.Move.ReadValue<Vector2>();
        pressTime = playerActions.PlayerInput.Sprint.ReadValue<float>();

        if (movement.magnitude == 0f)
            player.ChangeState(new IdleState(player));
        if(pressTime >= 0.1f)
            player.ChangeState(new RunningState(player));
    }
}
