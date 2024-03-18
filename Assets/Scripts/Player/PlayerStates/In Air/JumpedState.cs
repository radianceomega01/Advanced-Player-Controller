
using UnityEngine;

public class JumpedState : InAirState
{
    bool hasSkipedFirstFrame;

    public JumpedState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
        player.JumpCount++;
        if (player.JumpCount == 1)
            player.SetAnimation("Jumping");
        else
            player.SetAnimation("DJumping");
        player.GetRigidBody().AddForce(player.jumpForce * Vector3.up, ForceMode.Impulse);
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
        if (!hasSkipedFirstFrame)
            hasSkipedFirstFrame = true;
        else
        {
            player.SetState(StateFactory.GetPlayerState(typeof(FallingState), player));
        }
    }

    public override void Process()
    {
        base.Process();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
