
using UnityEngine;

public class JumpedState : InAirState
{
    public JumpedState(PlayerMovement player) : base(player) { }

    private bool isFirstFrame;

    public override void OnEnter()
    {
        base.OnEnter();
        isFirstFrame = true;
        player.JumpCount++;
        if (player.JumpCount == 1)
            player.SetAnimation("Jumping");
        else
            player.SetAnimation("DJumping");

        player.VerticalVelocity = Mathf.Sqrt(-2f * player.jumpHeight * player.gravity); //u = -2gh (initial velocity for jump)
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
        if (!isFirstFrame)
            CheckAndMoveToFallingOrGroundedStateOnJump();
        else
            isFirstFrame = false;
    }

    public override void OnExit()
    {
        base.OnExit();
        isFirstFrame = false;
    }
    private void CheckAndMoveToFallingOrGroundedStateOnJump()
    {
        if (player.IsGrounded())
            StateFactory.GetGroundedStateBasedOnMovementInputType(player);
        else
        {
            if (player.transform.position.y < player.PreviousYPos)
            {
                player.ChangeState(StateFactory.GetPlayerState(typeof(FallingState), player));
            }
        }
    }
}
