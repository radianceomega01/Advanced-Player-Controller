using UnityEngine;

[CreateAssetMenu(fileName = "VaultDataSO", menuName = "Scriptable Objects/VaultDataSO")]
public class VaultDataSO : ScriptableObject
{
    public float MinHeight;
    public float MaxHeight;
    public AvatarTarget AvatarTarget;
    public float startNormalizedTime;
    public float targetNormalizedTime;
    public Vector3 weight;

    public bool CanVault(float vaultHeight)
    {
        if (vaultHeight >= MinHeight && vaultHeight <= MaxHeight)
            return true;
        return false;
    }
}
