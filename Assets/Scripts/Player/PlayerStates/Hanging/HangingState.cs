using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HangingState : BaseMovementState
{
    public HangingState(PlayerMovement player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
        player.SetAnimation("Hanging");
        player.PlayerInput.CrouchSlide.performed += SwitchToFallingState;
        player.VerticalVelocity = 0f;
    }

    public override void OnExit()
    {
        base.OnExit();
        player.PlayerInput.CrouchSlide.performed -= SwitchToFallingState;
    }

    private void SwitchToFallingState(InputAction.CallbackContext ctx)
    {
        player.ChangeState(StateFactory.GetPlayerState(typeof(FallingState), player));
    }
}
