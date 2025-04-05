

using UnityEngine;

public class SlidingState : GroundedState
{
    public SlidingState(PlayerMovement player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
        player.SetVerticalVelocityWithHorizontalVelocity(player.slidingSpeed);
        //player.SetPlayerCollider(1);
        player.OnAnimComplete += ChangeState;
        player.SetAnimation("Sliding");
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
        player.CharacterController.Move(Time.fixedDeltaTime * (player.MovementDir * player.slidingSpeed + Vector3.up * player.VerticalVelocity));
    }

    protected override void MovementInputTypeChanged() { }

    private void ChangeState() => StateFactory.GetGroundedStateBasedOnMovementInputType(player);
    public override void OnExit()
    {
        base.OnExit();
        //player.SetPlayerCollider(0);
        player.OnAnimComplete -= ChangeState;
    }

}
