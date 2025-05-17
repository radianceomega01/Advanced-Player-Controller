
using UnityEngine;

public class SlidingState : GroundedState
{
    private float slidingSpeed;
    private float currentSpeed;
    public SlidingState(PlayerMovement player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
        slidingSpeed = player.GetMovementSpeed() + player.slidingSpeedAddition;
        currentSpeed = slidingSpeed;
        player.SetVerticalVelocityWithHorizontalVelocity(currentSpeed);
        //player.SetPlayerCollider(1);
        player.OnAnimComplete += ChangeState;
        player.SetAnimation("Slide");
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
        player.CharacterController.Move(Time.fixedDeltaTime * (player.transform.forward * currentSpeed + Vector3.up * player.VerticalVelocity));
    }

    public override void Process()
    {
        base.Process();
        currentSpeed += player.slidingDeAccelerationMultiplier * player.gravity * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, slidingSpeed);
    }

    protected override void MovementInputTypeChanged() { }

    private void ChangeState() => player.ChangeState(StateFactory.GetGroundedStateBasedOnMovementInputType(player));
    public override void OnExit()
    {
        base.OnExit();
        //player.SetPlayerCollider(0);
        player.OnAnimComplete -= ChangeState;
    }

}
