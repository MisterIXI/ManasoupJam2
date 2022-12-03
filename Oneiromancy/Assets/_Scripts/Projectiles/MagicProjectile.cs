using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MagicProjectile : MonoBehaviour
{
    [SerializeField] private PlayerSettings playerSettings;
    private enum ProjectileType
    {
        Enemy,
        Friendly
    }
    [SerializeField] private ProjectileType projectileType;
    private float _spawnTime;
    private Rigidbody _rigidbody;
    private void Start()
    {
        _spawnTime = Time.time;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // move 
        if (projectileType == ProjectileType.Friendly)
        {
            _rigidbody.MovePosition(transform.position + transform.forward * playerSettings.MagicProjectileSpeed * Time.fixedDeltaTime);
        }
        else
        {
            _rigidbody.MovePosition(transform.position + transform.forward * playerSettings.EnemyProjectileSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (projectileType == ProjectileType.Friendly)
        {
            // destroy on collision
            if (other.CompareTag("Enemy") || other.CompareTag("Obstacle"))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // destroy on collision
            if (other.CompareTag("Player"))
            {
                // Destroy(gameObject);
                Debug.Log("Hit Player");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bounds"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        ReferenceManager.PlayerController.PlayerAction.OnProjectileDestroy();
    }
}
