using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField]private float Angle = 0.1f;
    [SerializeField]private float Distance = 40f;
    [SerializeField]private float Speed = 15f;
    public GameObject targetObject;

    // Update is called once per frame
    void Update()
    {
        targetObject.transform.Rotate(0, Speed *Time.deltaTime,0 );
    }
}
