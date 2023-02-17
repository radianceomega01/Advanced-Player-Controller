using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : GroundedState
{
    Vector2 moveInput;
    float pressTime;
    float walkingSpeed = 100f;

    Vector3 playerForwardDir;
    Vector3 playerRightDir;

    Vector3 movementDir;
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
        player.GetRigidBody().velocity = movementDir * walkingSpeed * Time.fixedDeltaTime;
    }

    public override void Process()
    {
        moveInput = playerActions.PlayerInput.Move.ReadValue<Vector2>();
        pressTime = playerActions.PlayerInput.Sprint.ReadValue<float>();

        player.SetAnimation("InpX", moveInput.x);
        player.SetAnimation("InpY", moveInput.y);

        playerRightDir = player.transform.right;
        playerRightDir.y = 0;
        playerRightDir.Normalize();

        playerForwardDir = player.transform.forward;
        playerForwardDir.y = 0;
        playerForwardDir.Normalize();

        movementDir = playerRightDir * moveInput.x + playerForwardDir * moveInput.y;

        if (moveInput.magnitude == 0f)
            player.SetState(StateFactory.GetIdleState(player));
    }
}
