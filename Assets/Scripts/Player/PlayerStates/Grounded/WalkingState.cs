using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingState : GroundedState
{
    public WalkingState(PlayerMovement player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        player.PlayerInput.CrouchSlide.performed += SwitchToSlidingState;

        player.SetVerticalVelocityWithHorizontalVelocity(player.walkingSpeed);
        player.SetAnimation(NamingUtility.Walking, player.PreviousState.GetType() == typeof(SlidingState) ? 0.2f: 0.05f);
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
        player.CharacterController.Move(Time.fixedDeltaTime * (player.MovementDir * player.walkingSpeed + Vector3.up * player.VerticalVelocity));
    }

    public override void OnExit()
    {
        base.OnExit();
        player.PlayerInput.CrouchSlide.performed -= SwitchToSlidingState;
    }

}
