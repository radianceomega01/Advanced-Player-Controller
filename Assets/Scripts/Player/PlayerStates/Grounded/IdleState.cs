using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundedState
{
    public IdleState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();

        playerActions.PlayerInput.Move.performed += _ => player.ChangeState(new WalkingState(player));
        playerActions.PlayerInput.CrouchSlide.performed += _ => player.ChangeState(new CrouchingState(player));

    }

    public override void PhysicsProcess()
    {

    }

    public override void Process()
    {

    }
}
