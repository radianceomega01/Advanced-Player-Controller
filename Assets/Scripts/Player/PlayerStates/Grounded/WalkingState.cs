using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingState : GroundedState
{
    float walkingSpeed = 100f;
    public WalkingState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        player.SetAnimation("Walk");

        playerActions.PlayerInput.Sprint.performed += SwitchToRunningState;
        playerActions.PlayerInput.CrouchSlide.performed += SwitchToCrouchingState;
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();

        player.GetRigidBody().velocity = movementDir * walkingSpeed * Time.fixedDeltaTime;
    }

    public override void Process()
    {
        base.Process();

        if (moveInput.magnitude == 0f)
            player.SetState(StateFactory.GetIdleState(player));
    }

    public override void OnExit()
    {
        base.OnExit();
        playerActions.PlayerInput.Sprint.performed -= SwitchToRunningState;
        playerActions.PlayerInput.CrouchSlide.performed -= SwitchToCrouchingState;
    }

    private void SwitchToRunningState(InputAction.CallbackContext ctx)
    {
        if (moveInput.x == 0 && moveInput.y == 1)
        {
            player.SetState(StateFactory.GetRunningState(player));
        }
    }

    private void SwitchToCrouchingState(InputAction.CallbackContext ctx)
    {
        player.SetState(StateFactory.GetCrouchingState(player));
    }
}
