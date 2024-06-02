
using UnityEngine;

public class JumpedState : InAirState
{
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
