
using UnityEngine;

public class JumpedState : InAirState
{
    public JumpedState(PlayerMovement player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
        player.JumpCount++;
        if (player.JumpCount == 1)
            player.SetAnimation(NamingUtility.Jumped, 0f);
        else
            player.SetAnimation(NamingUtility.DJumped);

        player.VerticalVelocity = Mathf.Sqrt(-2f * player.jumpHeight * player.gravity); //u = -2gh (initial velocity for jump)
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();

        if (!isFirstFrame)
        {
            CheckAndMoveToFallingOrGroundedState();
        }
        else
            isFirstFrame = false;

    }

    private void CheckAndMoveToFallingOrGroundedState()
    {
        if (player.IsGrounded())
            player.ChangeState(StateFactory.GetGroundedStateBasedOnMovementInputType(player));
        else
        {
            if (player.transform.position.y < player.PreviousPos.y)
            {
                player.ChangeState(StateFactory.GetPlayerState(typeof(FallingState), player));
            }
        }
    }
}
