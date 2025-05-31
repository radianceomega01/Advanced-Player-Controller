using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EventsSO", menuName = "Scriptable Objects/EventsSO")]
public class EventsSO : ScriptableObject
{
    [HideInInspector] public UnityEvent<Vector3> CameraRotationEvent;
    [HideInInspector] public UnityEvent<bool> PlayerRotationLockToggleEvent;
}
