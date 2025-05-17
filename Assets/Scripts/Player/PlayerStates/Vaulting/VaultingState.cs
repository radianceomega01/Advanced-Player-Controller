
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class VaultingState : BaseMovementState
{
    float distanceToMoveForwardOnVault = 0.5f;
    float timeToMoveForwardAfterVault = 0.25f;
    public VaultingState(PlayerMovement player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
        player.OnAnimComplete += MoveSlightlyForward;
        player.GetInputSO().PlayerRotationLockToggleEvent.Invoke(true);
        player.GetInputSO().ChangePlayerLookAtEvent.Invoke(LookAtType.Rig);

        float vaultableobjectHeight = player.GetVaultableObjectHeight();
        if (vaultableobjectHeight <= player.CharacterController.height)
            player.SetAnimation("StepOver", 0f);
        else
            player.SetAnimation("Vault", 0f);

        player.transform.position +=  Vector3.up * vaultableobjectHeight;
    }
    private void MoveSlightlyForward()
    {
        player.transform.DOMove(player.transform.position + player.transform.forward * distanceToMoveForwardOnVault, timeToMoveForwardAfterVault)
            .OnComplete(OnVaultComplete);
        player.GetInputSO().PlayerRotationLockToggleEvent.Invoke(false);    
    }

    private void OnVaultComplete()
    {
        player.GetInputSO().ChangePlayerLookAtEvent.Invoke(LookAtType.Player);
        player.ChangeState(StateFactory.GetGroundedStateBasedOnMovementInputType(player));
    }

    public override void OnExit()
    {
        base.OnExit();
        player.OnAnimComplete -= MoveSlightlyForward;
    }
}
