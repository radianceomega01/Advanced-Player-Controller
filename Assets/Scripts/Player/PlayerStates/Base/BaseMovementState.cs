
public abstract class BaseMovementState
{
    protected PlayerMovement player;

    public BaseMovementState(PlayerMovement player)
    {
        this.player = player;
    }

    public virtual void OnEnter() { }
    public virtual void PhysicsProcess() { }

    //public abstract void OnAnimComplete();
    public virtual void Process() { }
    public virtual void LateProcess() { }
    public virtual void OnExit() { }

}
