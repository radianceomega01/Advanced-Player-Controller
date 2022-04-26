
public abstract class GroundedState : PlayerState
{
    protected PlayerActions playerAction;
    public override void OnEnter()
    {
        playerAction = ActionBehaviours.Instance.playerActions;
        playerAction.PlayerInput.Jump.performed += _ => PlayerStateManager.Instance.ChangeState(new LaunchingState());
    }
}
