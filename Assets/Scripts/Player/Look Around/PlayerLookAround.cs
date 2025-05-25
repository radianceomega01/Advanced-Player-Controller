using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAround : MonoBehaviour
{
    [SerializeField] EventsSO inputEventSO;
    [SerializeField] ReferencesSO referencesSO;
    [SerializeField] float playerRotDuration = 0.5f;

    private float elapsedTime = 0f;
    private bool rotationLocked = false;
    void Awake()
    {
        if (inputEventSO != null)
        {
            inputEventSO.PlayerRotationLockToggleEvent.AddListener(OnPlayerRotationLockToggle);
        }
    }
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void OnDestroy()
    {
        if (inputEventSO != null)
        { 
            inputEventSO.PlayerRotationLockToggleEvent.RemoveListener(OnPlayerRotationLockToggle);
        }
    }
    private void OnPlayerRotationLockToggle(bool value)
    {
        rotationLocked = value;
    }

    void Update()
    {
        if(!rotationLocked && referencesSO != null)
            HandlePlayerRotation(referencesSO.CameraRotation);
    }

    private void HandlePlayerRotation(Vector3 cameraRotation)
    {
        if (elapsedTime < playerRotDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / playerRotDuration;
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(transform.rotation.eulerAngles.x, cameraRotation.y, transform.rotation.eulerAngles.z)
                , t);
        }
        else
            elapsedTime = 0;
    }
}
