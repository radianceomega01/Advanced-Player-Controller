using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingState : GroundedState
{
    public WalkingState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        playerActions.PlayerInput.Sprint.performed += SwitchToRunningState;
        playerActions.PlayerInput.CrouchSlide.performed += SwitchToSlidingState;

        player.SetAnimation("Walking");
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();

        player.GetRigidBody().velocity = movementDir * player.walkingSpeed * Time.fixedDeltaTime;
    }

    public override void Process()
    {
        base.Process();

        if (moveInput.magnitude == 0f)
            player.SetState(StateFactory.GetPlayerState(typeof(IdleState), player));
    }

    public override void OnExit()
    {
        base.OnExit();
        playerActions.PlayerInput.Sprint.performed -= SwitchToRunningState;
        playerActions.PlayerInput.CrouchSlide.performed -= SwitchToSlidingState;
    }

    private void SwitchToRunningState(InputAction.CallbackContext ctx)
    {
        if (moveInput.x == 0 && moveInput.y == 1)
        {
            player.SetState(StateFactory.GetPlayerState(typeof(RunningState), player));
        }
    }
}
