using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraDeathController : MonoBehaviour
{
    float lookHorizontalSpeed =2f;
    float lookVerticalSpeed = 2f;
    float zoomSpeed = 2f;
    float dragSpeed = 6f;

    float yaw = 0f;
    float pitch= 0f;
    private void FixedUpdate() {
        MoveInTimeStop();
    }
    private void MoveInTimeStop()
    {
        if(Input.GetMouseButton(1))
        {
            yaw += lookHorizontalSpeed * Input.GetAxis("Mouse X");
            pitch -= lookVerticalSpeed * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
        if(Input.GetMouseButton(2))
        {
            transform.Translate(-Input.GetAxisRaw("Mouse X") * Time.unscaledDeltaTime* dragSpeed,
            -Input.GetAxisRaw("Mouse Y") * Time.unscaledDeltaTime * dragSpeed, 0);
        }
        transform.Translate(0,0,Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);
    }
}
