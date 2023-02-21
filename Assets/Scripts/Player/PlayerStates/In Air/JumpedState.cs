
using UnityEngine;

public class JumpedState : InAirState
{
    float force = 7f;

    public JumpedState(Player player) : base(player) { }

    public override void OnEnter()
    {
        player.SetAnimation("Jump");
    }

    public override void PhysicsProcess()
    {
        player.GetRigidBody().AddForce(force * Vector3.up, ForceMode.Impulse);
        player.SetState(StateFactory.GetFallingState(player));
        /*if (player.GetRigidBody().velocity.y <= 0f)
            player.SetState(StateFactory.GetFallingState(player));*/
    }

    public override void Process()
    {

    }

}
