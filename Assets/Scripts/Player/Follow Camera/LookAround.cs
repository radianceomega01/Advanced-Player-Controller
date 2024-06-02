using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    [SerializeField] Transform cameraTarget;
    [SerializeField] float rotationSpeedX = 13f;
    [SerializeField] float rotationSpeedY = 8f;
    [SerializeField] float turnSpeed = 0.01f;
    [SerializeField] float maxLookUpAngle = 60f;
    [SerializeField] float maxLookDownAngle = -60f;

    Vector2 moveDelta;

    float playerInterpolant;
    Player player;
    PlayerActions playerActions;

    float camAngularIncrementY;
    float camAngularIncrementX;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        playerActions = player.GetPlayerActions();
    }

    private void Update()
    {
        moveDelta = playerActions.PlayerInput.LookAround.ReadValue<Vector2>();
        HandleCameraRotation();
        HandlePlayerRotation();
    }

    private void HandleCameraRotation()
    {
        camAngularIncrementY = Time.deltaTime * rotationSpeedY * moveDelta.y;
        camAngularIncrementX = Time.deltaTime * rotationSpeedX * moveDelta.x;

        // Get the current rotation angles.
        Vector3 eulerAngles = cameraTarget.localEulerAngles;

        // Returned angles are in the range 0...360. Map that back to -180...180 for convenience.
        if (eulerAngles.x > 180f)
            eulerAngles.x -= 360f;

        // Increment the pitch angle, respecting the clamped range.
        eulerAngles.x = Mathf.Clamp(eulerAngles.x - camAngularIncrementY, -maxLookDownAngle, maxLookUpAngle);
        eulerAngles.y = eulerAngles.y + camAngularIncrementX;

        // Orient to match the new angles.
        cameraTarget.localEulerAngles = eulerAngles;

        //cameraTarget.Rotate(Vector3.up * Time.deltaTime * rotationSpeedX * -moveDelta.x);
        //cameraTarget.Rotate(Vector3.right * Time.deltaTime * rotationSpeedY * moveDelta.y);
    }

    private void HandlePlayerRotation()
    {
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation,
            Quaternion.Euler(player.transform.rotation.eulerAngles.x, cameraTarget.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z)
            , playerInterpolant);
        playerInterpolant += Time.deltaTime * turnSpeed;
        playerInterpolant = playerInterpolant >= 1 ? 0 : playerInterpolant;
    }
}
