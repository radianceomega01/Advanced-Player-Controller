using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Transform player;

    private float yOffset = 1.7f;

    private void Update()
    {
        transform.position = player.transform.position + Vector3.up*yOffset;
    }
}
