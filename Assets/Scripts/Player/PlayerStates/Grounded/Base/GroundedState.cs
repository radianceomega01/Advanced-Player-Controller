
using UnityEngine;

public abstract class GroundedState : PlayerState
{
    Vector3 playerForwardDir;
    Vector3 playerRightDir;

    protected float pressTime;
    protected Vector2 moveInput;
    protected Vector3 movementDir;

    public GroundedState(Player player) : base(player) { }

    public override void OnEnter()
    {
        playerActions.PlayerInput.Jump.performed += _ => player.SetState(new JumpedState(player));
    }

    public override void Process()
    {
        moveInput = playerActions.PlayerInput.Move.ReadValue<Vector2>();
        pressTime = playerActions.PlayerInput.Sprint.ReadValue<float>();

        player.SetAnimation("InpX", moveInput.x);
        player.SetAnimation("InpY", moveInput.y);

        playerRightDir = player.transform.right;
        playerRightDir.y = 0;
        playerRightDir.Normalize();

        playerForwardDir = player.transform.forward;
        playerForwardDir.y = 0;
        playerForwardDir.Normalize();

        movementDir = (playerRightDir * moveInput.x + playerForwardDir * moveInput.y).normalized;
    }
}
