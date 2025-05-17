
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
        // if (!isFirstFrame)
        // {
        //     CheckAndMoveToHangingState();
        // }
        // else
        //     isFirstFrame = false;
    }

    private void TransitionToFalling()
    {
        if(player.PreviousState.GetType().IsSubclassOf(typeof(InAirState)))
        {
            if (player.JumpCount <= 1)
                player.SetAnimation("Falling");
            else
                player.SetAnimation("DFalling");
        }
        else
            player.SetAnimation("Falling");
    }
    private void CheckAndMoveToGroundedState()
    {
        if (player.IsGrounded())
            player.ChangeState(StateFactory.GetGroundedStateBasedOnMovementInputType(player));
    }
    protected override void CheckAndMoveToHangingState()
    {
        if (!player.DidPalmDetectObject() && palmTouchOnPreviousFrame)
            player.ChangeState(StateFactory.GetPlayerState(typeof(HangingState), player));
    }
}
