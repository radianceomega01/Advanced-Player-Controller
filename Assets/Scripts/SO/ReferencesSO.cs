using UnityEngine;

[CreateAssetMenu(fileName = "ReferencesSO", menuName = "Scriptable Objects/ReferencesSO")]
public class ReferencesSO : ScriptableObject
{
    [HideInInspector] public Vector2 SwipeDistance;
    [HideInInspector] public Vector3 PlayerPosition;
    [HideInInspector] public Vector3 CameraRotation;
}
