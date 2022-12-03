using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSync : MonoBehaviour
{
    Transform _playerTransform;
    PlayerSettings _playerSettings;

    private void Start()
    {
        _playerTransform = ReferenceManager.PlayerController.transform;
        _playerSettings = ReferenceManager.PlayerController.PlayerSettings;
    }

    private void Update()
    {
        transform.position = _playerTransform.position + _playerSettings.CameraOffset;
        transform.LookAt(_playerTransform);
    }
}
