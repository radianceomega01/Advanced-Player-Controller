
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InAirState : BaseMovementState
{
    protected bool isFirstFrame;
    protected bool palmTouchOnPreviousFrame;

    public InAirState(PlayerMovement player) : base(player) { }

    protected abstract void CheckAndMoveToHangingState();

    public override void OnEnter() 
    {
        base.OnEnter();
        isFirstFrame = true;
        player.PlayerInput.Jump.performed += SwitchToJumpingState;
    }

    public override void PhysicsProcess() 
    {
        base.PhysicsProcess();
        player.CharacterController.Move(Time.fixedDeltaTime * (player.MovementDir * player.GetMovementSpeed() + Vector3.up * player.VerticalVelocity));
    }

    public override void Process() 
    {
        base.Process();
        player.VerticalVelocity += player.gravity * Time.deltaTime;
    }

    public override void LateProcess()
    {
        base.LateProcess();
        palmTouchOnPreviousFrame = player.DidPalmDetectObject();
    }

    public override void OnExit() 
    {
        base.OnExit();
        player.PlayerInput.Jump.performed -= SwitchToJumpingState;
    }

    private void SwitchToJumpingState(InputAction.CallbackContext ctx)
    {
        if (player.JumpCount == 2)
            return;
        player.ChangeState(StateFactory.GetPlayerState(typeof(JumpedState), player));
    }

}
