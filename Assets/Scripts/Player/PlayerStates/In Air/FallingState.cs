
public class FallingState : InAirState
{
    public FallingState(Player player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
        if(player.GetPreviousState().GetType().IsSubclassOf(typeof(GroundedState)))
            player.SetAnimation("Fall");
    }

    public override void PhysicsProcess()
    {
        base.PhysicsProcess();
    }

    public override void Process()
    {
        base.Process();
    }

    public override void OnExit()
    {
        base.OnExit();
        player.ResetAnimation("Fall");
    }
}
