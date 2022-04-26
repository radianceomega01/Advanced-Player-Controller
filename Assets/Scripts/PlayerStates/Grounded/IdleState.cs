using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundedState
{

    public override void OnEnter()
    {
        base.OnEnter();

        playerAction.PlayerInput.Move.performed += _ => PlayerStateManager.Instance.ChangeState(new WalkingState());
        playerAction.PlayerInput.Jump.performed += _ => PlayerStateManager.Instance.ChangeState(new LaunchingState());
        playerAction.PlayerInput.Sprint.performed += _ => PlayerStateManager.Instance.ChangeState(new RunningState());
        playerAction.PlayerInput.CrouchSlide.performed += _ => PlayerStateManager.Instance.ChangeState(new CrouchingState());
    }

    public override void PhysicsProcess()
    {

    }

    public override void Process()
    {

    }
}
