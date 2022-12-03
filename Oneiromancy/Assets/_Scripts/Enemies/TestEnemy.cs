using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    private float _lastShootTime;
    [SerializeField] private GameObject _projectilePrefab;

    private void Start()
    {
        _lastShootTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - _lastShootTime > 1.5f)
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
