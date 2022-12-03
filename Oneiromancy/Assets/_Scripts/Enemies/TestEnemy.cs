using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    private float _lastShootTime;
    private Transform _playerTransform;
    [SerializeField] private GameObject _projectilePrefab;

    private void Start()
    {
        _lastShootTime = Time.time;
        _playerTransform = ReferenceManager.PlayerController.transform;
    }

    private void Update()
    {   
        // rotate towards player
        transform.LookAt(_playerTransform);
        if (Time.time - _lastShootTime > 0.1f)
        {
            _lastShootTime = Time.time;
            Shoot();
        }

    }

    private void Shoot()
    {
        // Debug.Log("Shoot");
        GameObject projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
        // projectile.transform.forward = transform.forward;
    }
}
