using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundedState
{
    public IdleState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        player.SetAnimation("Idle");

        playerActions.PlayerInput.Move.performed += _ => player.SetState(StateFactory.GetWalkingState(player));
        playerActions.PlayerInput.CrouchSlide.performed += _ => player.SetState(StateFactory.GetCrouchingState(player));

    }

    public override void PhysicsProcess()
    {

    }

    public override void Process()
    {

    }
}
