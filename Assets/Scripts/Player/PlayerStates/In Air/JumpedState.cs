
using UnityEngine;

public class JumpedState : InAirState
{
    float force = 5.5f;

    public JumpedState(Player player) : base(player) { }

    public override void OnEnter()
    {
        jumpCount++;
        if(jumpCount == 1)
            player.SetAnimation("Jump");
        else
            player.SetAnimation("DJump");
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
        player.GetRigidBody().AddForce(force * Vector3.up, ForceMode.Impulse);
        player.SetState(StateFactory.GetFallingState(player));
        /*if (player.GetRigidBody().velocity.y <= 0f)
            player.SetState(StateFactory.GetFallingState(player));*/
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
