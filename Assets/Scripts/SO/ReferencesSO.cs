using UnityEngine;

[CreateAssetMenu(fileName = "References", menuName = "ScriptableObjects/References")]
public class ReferencesSO : ScriptableObject
{
    [HideInInspector] public Vector2 SwipeDistance;
    [HideInInspector] public Vector3 PlayerPosition;
    [HideInInspector] public Vector3 CameraRotation;
}
