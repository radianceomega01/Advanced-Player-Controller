using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundedState
{
    public IdleState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        if(player.GetPreviousState() == StateFactory.GetFallingState(player))
            player.SetAnimation("Land");

        player.SetAnimation("Idle");
        player.ResetAnimation("Land");

        playerActions.PlayerInput.Sprint.performed += _ =>
        {
            if (moveInput.x == 0 && moveInput.y == 1)
            {
                player.SetState(StateFactory.GetRunningState(player));
            }
        };

        //playerActions.PlayerInput.Move.performed += _ => player.SetState(StateFactory.GetWalkingState(player));
        playerActions.PlayerInput.CrouchSlide.performed += _ => player.SetState(StateFactory.GetCrouchingState(player));

    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
    }

    public override void Process()
    {
        base.Process();
        if (moveInput.magnitude != 0f)
            player.SetState(StateFactory.GetWalkingState(player));
    }
}
