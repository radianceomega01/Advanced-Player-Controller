using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAround : MonoBehaviour
{
    [SerializeField] InputEventsSO inputEventSO;
    [SerializeField] float playerRotDuration = 0.5f;

    private float elapsedTime = 0f;
    void Awake()
    {
        inputEventSO.CameraRotationEvent.AddListener(OnCameraRotationReceived);
    }

    private void OnDestroy()
    {
        inputEventSO.CameraRotationEvent.RemoveListener(OnCameraRotationReceived);
    }
    private void OnCameraRotationReceived(Vector3 cameraRotation)
    {
        HandlePlayerRotation(cameraRotation);
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
