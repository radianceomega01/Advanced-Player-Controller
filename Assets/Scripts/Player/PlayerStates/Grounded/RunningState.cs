using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : GroundedState
{
    public RunningState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        playerActions.PlayerInput.CrouchSlide.performed += SwitchToSlidingState;

        player.SetAnimation("Running");
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();

        player.GetRigidBody().velocity = movementDir * player.runningSpeed * Time.fixedDeltaTime;
    }

    public override void Process()
    {
        base.Process();

        if (moveInput.magnitude == 0f)
            player.SetState(StateFactory.GetPlayerState(typeof(IdleState), player));
#if (PLATFORM_ANDROID && !UNITY_EDITOR)
        if (CanSwitchToWalkingState())
#else
        if (pressTime <= 0.1f)
#endif
            player.SetState(StateFactory.GetPlayerState(typeof(WalkingState), player));
    }

    public override void OnExit()
    {
        base.OnExit();
        playerActions.PlayerInput.CrouchSlide.performed -= SwitchToSlidingState;
    }

    private bool CanSwitchToWalkingState()
    {
        if (moveInput.magnitude < 1 && moveInput.y <= 0)
            return true;
        else
            return false;
    }

}
