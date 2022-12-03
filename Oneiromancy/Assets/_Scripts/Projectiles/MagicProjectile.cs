using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MagicProjectile : MonoBehaviour
{
    [SerializeField] private PlayerSettings playerSettings;

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
        _rigidbody.MovePosition(transform.position + transform.forward * playerSettings.MagicProjectileSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // destroy on collision
        if (other.CompareTag("Enemy") || other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Bounds")) {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        ReferenceManager.PlayerController.PlayerAction.OnProjectileDestroy();
    }
}
