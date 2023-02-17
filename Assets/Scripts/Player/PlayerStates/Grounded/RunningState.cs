using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : GroundedState
{
    Vector2 moveInput;
    float pressTime;
    float runningSpeed = 250f;

    Vector3 playerForwardDir;
    Vector3 playerRightDir;

    Vector3 movementDir;

    public RunningState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        player.SetAnimation("Run");
    }

    public override void PhysicsProcess()
    {
        
        player.GetRigidBody().velocity = movementDir * runningSpeed * Time.fixedDeltaTime;
    }

    public override void Process()
    {
        moveInput = playerActions.PlayerInput.Move.ReadValue<Vector2>();
        pressTime = playerActions.PlayerInput.Sprint.ReadValue<float>();

        playerRightDir = player.transform.right;
        playerRightDir.y = 0;
        playerRightDir.Normalize();

        playerForwardDir = player.transform.forward;
        playerForwardDir.y = 0;
        playerForwardDir.Normalize();

        movementDir = playerRightDir * moveInput.x + playerForwardDir * moveInput.y;

        if (moveInput.magnitude == 0f)
            player.SetState(StateFactory.GetIdleState(player));
        if (pressTime <= 0.1f)
            player.SetState(StateFactory.GetWalkingState(player));
    }
}
