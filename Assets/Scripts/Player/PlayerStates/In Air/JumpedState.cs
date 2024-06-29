
using UnityEngine;

public class JumpedState : InAirState
{
    public JumpedState(Player player) : base(player) { }

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
        player.GetRigidBody().AddForce(player.jumpForce * Vector3.up, ForceMode.Impulse);
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
        if(!isFirstFrame)
            player.CheckAndMoveToGroundedState();
        else
            isFirstFrame = false;
    }

    public override void Process()
    {
        base.Process();
        player.CheckAndMoveToFallingState();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
