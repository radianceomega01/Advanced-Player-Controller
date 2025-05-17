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

        //player.PlayerInput.CrouchSlide.performed += SwitchToCrouchingState;
        player.SetAnimation("Idle");
        player.VerticalVelocity = 0f;

    }
    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
        player.CharacterController.Move(Time.fixedDeltaTime * Vector3.up * -player.runningSpeed);
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
