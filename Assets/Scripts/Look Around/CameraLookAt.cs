using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLookAt : MonoBehaviour
{
    [SerializeField] ReferencesSO referencesSO;
    [SerializeField] float camYOffsetFromPlayer = 1.7f;
    [SerializeField] float camRotationSpeedX = 13f;
    [SerializeField] float camRotationSpeedY = 8f;
    [SerializeField] float camMaxLookUpAngle = 60f;
    [SerializeField] float camMaxLookDownAngle = 60f;

    float camAngularIncrementY;
    float camAngularIncrementX;
    PlayerActions playerActions;

    void Awake()
    {
        playerActions = new PlayerActions();
    }
    void OnEnable()
    {
        playerActions.PlayerInput.Enable();
    }
    void OnDisable()
    {
        playerActions.PlayerInput.Disable();
    }

    void Update()
    {
#if UNITY_EDITOR
        HandleCameraRotation(playerActions.PlayerInput.LookAround.ReadValue<Vector2>());
#elif UNITY_ANDROID || UNITY_IOS
        if (referencesSO == null)
            return;
        HandleCameraRotation(referencesSO.SwipeDistance);
#else
        HandleCameraRotation(playerActions.PlayerInput.LookAround.ReadValue<Vector2>());
#endif
        
    }
    void LateUpdate()
    {
        if (referencesSO == null)
            return;
        UpdatePos(referencesSO.PlayerPosition);
    }

    private void HandleCameraRotation(Vector2 moveDelta)
    {
        camAngularIncrementY = Time.deltaTime * camRotationSpeedY * moveDelta.y;
        camAngularIncrementX = Time.deltaTime * camRotationSpeedX * moveDelta.x;

        // Get the current rotation angles.
        Vector3 eulerAngles = transform.localEulerAngles;

        // Returned angles are in the range 0...360. Map that back to -180...180 for convenience.
        if (eulerAngles.x > 180f)
            eulerAngles.x -= 360f;

        // Increment the pitch angle, respecting the clamped range.
        eulerAngles.x = Mathf.Clamp(eulerAngles.x - camAngularIncrementY, -camMaxLookDownAngle, camMaxLookUpAngle);
        eulerAngles.y = eulerAngles.y + camAngularIncrementX;

        // Orient to match the new angles.
        transform.localEulerAngles = eulerAngles;
        referencesSO.CameraRotation = eulerAngles;

        //cameraTarget.Rotate(Vector3.up * Time.deltaTime * rotationSpeedX * -moveDelta.x);
        //cameraTarget.Rotate(Vector3.right * Time.deltaTime * rotationSpeedY * moveDelta.y);
    }

    private void UpdatePos(Vector3 playerPos)
    {
        transform.position = playerPos + Vector3.up * camYOffsetFromPlayer;
    }
}
