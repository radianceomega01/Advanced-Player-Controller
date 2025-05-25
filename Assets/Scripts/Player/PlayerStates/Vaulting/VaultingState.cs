
using System.Collections;
using DG.Tweening;
using UnityEngine;
using System.Threading.Tasks;

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

        float vaultableobjectHeight = player.GetVaultableObjectHeight();
        player.transform.position +=  Vector3.up * (vaultableobjectHeight);
        
        PlayAnim(vaultableobjectHeight);

    }
    private async void PlayAnim(float vaultableobjectHeight)
    {
        await Task.Delay(10);
        player.ToggleAnimatorRootMotion(true);
        if (vaultableobjectHeight <= player.CharacterController.height)
            player.SetAnimation("StepOver", 0f);
        else
            player.SetAnimation("Vault", 0f);
    }

    private void MoveSlightlyForward()
    {
        player.transform.DOMove(player.transform.position + player.transform.forward * distanceToMoveForwardOnVault, timeToMoveForwardAfterVault)
            .OnComplete(OnVaultComplete);
        player.GetInputSO().PlayerRotationLockToggleEvent.Invoke(false);    
    }

    private void OnVaultComplete()
    {
        player.ToggleAnimatorRootMotion(false);
        player.ChangeState(StateFactory.GetGroundedStateBasedOnMovementInputType(player));
    }

    public override void OnExit()
    {
        base.OnExit();
        player.OnAnimComplete -= MoveSlightlyForward;
    }
}
