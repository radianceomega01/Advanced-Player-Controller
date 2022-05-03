
public abstract class GroundedState : PlayerState
{
    public GroundedState(Player player) : base(player) { }

    public override void OnEnter()
    {
        playerActions.PlayerInput.Jump.performed += _ => player.ChangeState(new LaunchingState(player));
    }
}
