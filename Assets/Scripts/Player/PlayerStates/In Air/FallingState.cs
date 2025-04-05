
public class FallingState : InAirState
{
    public FallingState(PlayerMovement player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
        if (player.JumpCount <= 1)
            TransitionToFalling();
        else
            player.SetAnimationTrigger("DFall");
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
        CheckAndMoveToGroundedState();
    }

    private void TransitionToFalling()
    {
        if (player.GetPreviousState().GetType().IsSubclassOf(typeof(GroundedState)))
            player.SetAnimation("Falling");
        else
            player.SetAnimationTrigger("Fall");
    }
    private void CheckAndMoveToGroundedState()
    {
        if (player.IsGrounded())
            StateFactory.GetGroundedStateBasedOnMovementInputType(player);
    }
}
