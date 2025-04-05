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
        player.PlayerInput.Sprint.performed += SwitchToRunningState;

        player.SetVerticalVelocityWithHorizontalVelocity(player.walkingSpeed);
        player.SetAnimation("Walking");
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();

        player.CharacterController.Move(Time.fixedDeltaTime * (player.MovementDir * player.walkingSpeed + Vector3.up * player.VerticalVelocity));
    }

    public override void OnExit()
    {
        base.OnExit();
        player.PlayerInput.Sprint.performed -= SwitchToRunningState;
        player.PlayerInput.CrouchSlide.performed -= SwitchToSlidingState;
    }

    private void SwitchToRunningState(InputAction.CallbackContext ctx) => player.ChangeState(StateFactory.GetPlayerState(typeof(RunningState), player));

}
