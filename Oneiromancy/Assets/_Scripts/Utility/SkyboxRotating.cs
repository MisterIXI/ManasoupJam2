using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotating : MonoBehaviour
{
    public float Speed = 0.4f;
    
    // Update is called once per frame
    void Update () {
    RenderSettings.skybox.SetFloat("_Rotation", Time.time * Speed);
}
}
