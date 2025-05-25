using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Events", menuName = "ScriptableObjects/Events")]
public class EventsSO : ScriptableObject
{
    [HideInInspector] public UnityEvent<Vector3> CameraRotationEvent;
    [HideInInspector] public UnityEvent<bool> PlayerRotationLockToggleEvent;
}
