using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    public float Speed= 15f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Speed *Time.deltaTime,0 );
        
    }
}
