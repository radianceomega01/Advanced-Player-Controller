
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

        PlayAnim(player.InstantaneousVaultHeight);

    }
    private void PlayAnim(float vaultableobjectHeight)
    {
        player.ToggleAnimatorRootMotion(true);
        player.ToggleCharacterController(false);

        if (player.stepOverData.CanVault(vaultableobjectHeight))
        {
            player.ModifyVaultPointOnZAxis(0.15f);
            player.SetAnimation("StepOver", 0);
            SetAnimationMatchTarget(player.stepOverData);
        }
        else if (player.smallVaultData.CanVault(vaultableobjectHeight))
        {
            player.ModifyVaultPointOnZAxis(-0.2f);
            player.SetAnimation("SmallVault", 0);
            SetAnimationMatchTarget(player.smallVaultData);
        }
        else
        {
            player.ModifyVaultPointOnZAxis(-0.3f);
            player.SetAnimation("LargeVault", 0);
            SetAnimationMatchTarget(player.largeVaultData);
        }
    }

    private async void SetAnimationMatchTarget(VaultDataSO playerVaultData)
    {
        await Task.Delay(10); // Ensure the animation is ready to match target
        player.SetAnimationMatchTarget(playerVaultData.AvatarTarget, playerVaultData.weight,
                playerVaultData.startNormalizedTime, playerVaultData.targetNormalizedTime);
    }

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
