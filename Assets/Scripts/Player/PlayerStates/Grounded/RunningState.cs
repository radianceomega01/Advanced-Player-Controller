using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : GroundedState
{
    public RunningState(PlayerMovement player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        player.PlayerInput.CrouchSlide.performed += SwitchToSlidingState;
        player.SetVerticalVelocityWithHorizontalVelocity(player.runningSpeed);
        player.SetAnimation("Running");
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
        player.CharacterController.Move(Time.fixedDeltaTime * (player.MovementDir * player.runningSpeed + Vector3.up * player.VerticalVelocity));
    }

    public override void OnExit()
    {
        base.OnExit();
        player.PlayerInput.CrouchSlide.performed -= SwitchToSlidingState;
    }
}
