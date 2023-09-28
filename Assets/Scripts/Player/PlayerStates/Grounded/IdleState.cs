using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : GroundedState
{
    public IdleState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        player.SetAnimation("Idle");

        playerActions.PlayerInput.Sprint.performed += SwitchToRunningState;
        playerActions.PlayerInput.CrouchSlide.performed += SwitchToCrouchingState;

        //playerActions.PlayerInput.Move.performed += _ => player.SetState(StateFactory.GetWalkingState(player));

    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
    }

    public override void Process()
    {
        base.Process();
        if (moveInput.magnitude != 0f)
        {
            player.SetState(StateFactory.GetPlayerState(typeof(WalkingState), player));
        }
            
    }

    public override void OnExit()
    {
        base.OnExit();
        player.ResetAnimation("Idle");
        playerActions.PlayerInput.Sprint.performed -= SwitchToRunningState;
        playerActions.PlayerInput.CrouchSlide.performed -= SwitchToCrouchingState;
    }

    private void SwitchToRunningState(InputAction.CallbackContext ctx)
    {
        if (moveInput.x == 0 && moveInput.y == 1)
        {
            player.SetState(StateFactory.GetPlayerState(typeof(RunningState), player));
        }
    }

    private void SwitchToCrouchingState(InputAction.CallbackContext ctx)
    {
        player.SetState(StateFactory.GetPlayerState(typeof(CrouchingState), player));
    }
}
