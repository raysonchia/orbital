using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    //public Transform lookAt;
    //public float boundX = 0.15f;
    //public float boundY = 0.15f;

    //private void LateUpdate()
    //{
        //Vector3 delta = Vector3.zero;

        //// Check whether player inside the bounds on the X axis
        //float deltaX = lookAt.position.x - transform.position.x;
        //if (deltaX > boundX || deltaX < -boundX)
        //{
        //    if (transform.position.x < lookAt.position.x)
        //    {
        //        delta.x = deltaX - boundX;
        //    }
        //    else
        //    {
        //        delta.x = deltaX + boundX;
        //    }
        //}

        //float deltaY = lookAt.position.y - transform.position.y;
        //if (deltaY > boundY || deltaY < -boundY)
        //{
        //    if (transform.position.y < lookAt.position.y)
        //    {
        //        delta.y = deltaY - boundY;
        //    }
        //    else
        //    {
        //        delta.y = deltaY + boundY;
        //    }
        //}

        //transform.position += new Vector3(delta.x, delta.y, 0);
    //}


    [SerializeField] private Transform playerTransform;
    [SerializeField] private Camera mainCamera;
    [Range(2, 100)] [SerializeField] private float cameraTargetDivider;

    private void Update()
    {
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var cameraTargetPosition = (mousePosition + (cameraTargetDivider - 1) * playerTransform.position) / cameraTargetDivider;
        cameraTargetPosition.z = -10;
        transform.position = cameraTargetPosition;
    }
}
