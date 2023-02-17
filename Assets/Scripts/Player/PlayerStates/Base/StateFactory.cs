
public static class StateFactory
{
    static PlayerState idleState;
    static PlayerState walkingState;
    static PlayerState runningState;
    static PlayerState crouchingState;
    static PlayerState jumpedState;
    static PlayerState fallingState;

    public static PlayerState GetIdleState(Player player)
    {
        if (idleState == null)
            idleState = new IdleState(player);
        return idleState;
    }

    public static PlayerState GetWalkingState(Player player)
    {
        if (walkingState == null)
            walkingState = new WalkingState(player);
        return walkingState;
    }

    public static PlayerState GetCrouchingState(Player player)
    {
        if (crouchingState == null)
            crouchingState = new CrouchingState(player);
        return crouchingState;
    }

    public static PlayerState GetRunningState(Player player)
    {
        if (runningState == null)
            runningState = new RunningState(player);
        return runningState;
    }

    public static PlayerState GetJumpedState(Player player)
    {
        if (jumpedState == null)
            jumpedState = new JumpedState(player);
        return jumpedState;
    }

    public static PlayerState GetFallingState(Player player)
    {
        if (fallingState == null)
            fallingState = new FallingState(player);
        return fallingState;
    }

}
