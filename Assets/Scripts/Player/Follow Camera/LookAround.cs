using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    [SerializeField] Transform cameraTarget;

    Player player;
    PlayerActions playerActions;

    Vector2 moveDelta;
    float rotationSpeedX = 13f;
    float rotationSpeedY = 8f;
    float turnSpeed = 0.01f;
    float interpolant;

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
        cameraTarget.Rotate(Vector3.up * Time.deltaTime * rotationSpeedX * -moveDelta.x);
        cameraTarget.Rotate(Vector3.right * Time.deltaTime * rotationSpeedY * moveDelta.y);

        player.transform.rotation = Quaternion.Slerp(player.transform.rotation,
            Quaternion.Euler(player.transform.rotation.eulerAngles.x, cameraTarget.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z)
            , interpolant);
        interpolant += Time.deltaTime * turnSpeed;
    }
}
