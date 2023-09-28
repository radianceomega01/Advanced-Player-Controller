
using UnityEngine;

public class JumpedState : InAirState
{
    float force = 5.5f;
    bool hasJumped;

    public JumpedState(Player player) : base(player) { }

    public override void OnEnter()
    {
        hasJumped = false;
        jumpCount++;
        if (jumpCount == 1)
            player.SetAnimation("Jumping");
        else
            player.SetAnimation("DJumping");
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
        if (!hasJumped)
        {
            player.GetRigidBody().AddForce(force * Vector3.up, ForceMode.Impulse);
            player.SetState(StateFactory.GetPlayerState(typeof(FallingState), player));
            hasJumped = true;
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
