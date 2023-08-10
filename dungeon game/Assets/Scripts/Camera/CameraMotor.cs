using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Camera mainCamera;
    [Range(2, 100)] [SerializeField] private float cameraTargetDivider;

    private void Awake()
    {
        playerTransform = GameObject.FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var cameraTargetPosition = (mousePosition + (cameraTargetDivider - 1) * playerTransform.position) / cameraTargetDivider;
        cameraTargetPosition.z = -10;
        transform.position = cameraTargetPosition;
    }
}
