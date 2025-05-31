
using System.Collections;
using DG.Tweening;
using UnityEngine;
using System.Threading.Tasks;

public class VaultingState : BaseMovementState
{
    public VaultingState(PlayerMovement player) : base(player) { }

    public override void OnEnter()
    {
        base.OnEnter();
        player.OnAnimComplete += OnVaultComplete;
        player.GetInputSO().PlayerRotationLockToggleEvent.Invoke(true);

        //player.transform.position +=  Vector3.up * (vaultableobjectHeight);

        PlayAnim(player.InstantaneousVaultHeight);

    }
    private void PlayAnim(float vaultableobjectHeight)
    {
        player.ToggleAnimatorRootMotion(true);
        player.ToggleCharacterController(false);

        if (player.stepOverData.CanVault(vaultableobjectHeight))
        {
            player.SetAnimation("StepOver", 0);
            player.SetAnimationMatchTarget(player.stepOverData.AvatarTarget, player.stepOverData.weight,
                player.stepOverData.startNormalizedTime, player.stepOverData.targetNormalizedTime);
        }
        else if (player.smallVaultData.CanVault(vaultableobjectHeight))
        {
            player.SetAnimation("SmallVault", 0);
            player.SetAnimationMatchTarget(player.smallVaultData.AvatarTarget, player.smallVaultData.weight,
                player.smallVaultData.startNormalizedTime, player.smallVaultData.targetNormalizedTime);
        }
        else
        {
            player.SetAnimation("LargeVault", 0);
            player.SetAnimationMatchTarget(player.largeVaultData.AvatarTarget, player.largeVaultData.weight,
                player.largeVaultData.startNormalizedTime, player.largeVaultData.targetNormalizedTime);
        }
    }

    // private void MoveSlightlyForward()
    // {
    //     player.transform.DOMove(player.transform.position + player.transform.forward * distanceToMoveForwardOnVault, timeToMoveForwardAfterVault)
    //         .OnComplete(OnVaultComplete);
    //     player.GetInputSO().PlayerRotationLockToggleEvent.Invoke(false);    
    // }

    private void OnVaultComplete()
    {
        player.ToggleAnimatorRootMotion(false);
        player.ToggleCharacterController(true);
        player.ChangeState(StateFactory.GetGroundedStateBasedOnMovementInputType(player));
    }

    public override void OnExit()
    {
        base.OnExit();
        player.GetInputSO().PlayerRotationLockToggleEvent.Invoke(false);
        player.OnAnimComplete -= OnVaultComplete;
    }
}
