using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputEvents", menuName = "ScriptableObjects/InputEvents")]
public class InputEventsSO : ScriptableObject
{
    [HideInInspector] public UnityEvent<Vector2> LookAroundEvent;
    [HideInInspector] public UnityEvent<Vector3> CameraRotationEvent;
    [HideInInspector] public UnityEvent<Vector3> PlayerPositionEvent;
    [HideInInspector] public UnityEvent<bool> PlayerRotationLockToggleEvent;
    [HideInInspector] public UnityEvent<LookAtType> ChangePlayerLookAtEvent;
}
