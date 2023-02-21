
using UnityEngine;

public class JumpedState : InAirState
{
    Vector3 playerForwardDir;
    Vector3 playerRightDir;
    float force = 5f;

    protected Vector2 moveInput;
    protected Vector3 movementDir;

    public JumpedState(Player player) : base(player) { }

    public override void OnEnter()
    {
        moveInput = playerActions.PlayerInput.Move.ReadValue<Vector2>();

        playerRightDir = player.transform.right;
        playerRightDir.y = 0;
        playerRightDir.Normalize();

        playerForwardDir = player.transform.forward;
        playerForwardDir.y = 0;
        playerForwardDir.Normalize();

        movementDir = (playerRightDir * moveInput.x + playerForwardDir * moveInput.y).normalized;
    }

    public override void PhysicsProcess()
    {
        player.GetRigidBody().AddForce(force * new Vector3(movementDir.x, 1f, movementDir.z), ForceMode.Acceleration);
        if(player.GetRigidBody().velocity.y <= 0f)
            player.SetState(StateFactory.GetFallingState(player));
    }

    public override void Process()
    {

    }

}
