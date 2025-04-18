using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : GroundedState
{
    public IdleState(PlayerMovement player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        if (player.GetPreviousState() != null && player.GetPreviousState().GetType() == typeof(FallingState))
        {
            player.SetAnimation("Landing");
        }
        //player.PlayerInput.CrouchSlide.performed += SwitchToCrouchingState;
        player.SetAnimation("Idle");
        player.SetVerticalVelocityWithHorizontalVelocity(player.walkingSpeed);

    }
    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
        player.CharacterController.Move(Time.fixedDeltaTime * Vector3.up * player.VerticalVelocity);
    }

    public override void OnExit()
    {
        base.OnExit();

        //player.PlayerInput.CrouchSlide.performed -= SwitchToCrouchingState;
    }

    /*private void SwitchToCrouchingState(InputAction.CallbackContext ctx)
    {
        player.ChangeState(StateFactory.GetPlayerState(typeof(CrouchingState), player));
    }*/
}
