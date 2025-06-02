
public class FallingState : InAirState
{
    public FallingState(PlayerMovement player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
        TransitionToFalling();
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
        CheckAndMoveToGroundedState();
    }

    private void TransitionToFalling()
    {
        if(player.PreviousState.GetType().IsSubclassOf(typeof(InAirState)))
        {
            if (player.JumpCount <= 1)
                player.SetAnimation(NamingUtility.Falling);
            else
                player.SetAnimation(NamingUtility.DFalling);
        }
        else
            player.SetAnimation(NamingUtility.Falling);
    }
    private void CheckAndMoveToGroundedState()
    {
        if (player.IsGrounded())
            player.ChangeState(StateFactory.GetGroundedStateBasedOnMovementInputType(player));
    }
}
