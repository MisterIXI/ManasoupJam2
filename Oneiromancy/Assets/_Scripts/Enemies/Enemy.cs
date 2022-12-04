using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    protected Transform _playerTransform;
    protected GameManager _gameManager;
    protected PlayerSettings _playerSettings;
    protected Rigidbody _rb;
    public int CurrentHealth { get; protected set; }

    private void Start()
    {
        _playerTransform = ReferenceManager.PlayerController.transform;
        _gameManager = ReferenceManager.GameManager;
        _playerSettings = ReferenceManager.GameManager.PlayerSettings;
        Debug.Log("GameManager: " + _gameManager);
        Debug.Log("PlayerSettings: " + _playerSettings);
        _rb = GetComponent<Rigidbody>();
        // default Health 
        CurrentHealth = 10;
        Init();
    }
    abstract protected void Init();
    private void FixedUpdate()
    {
        EnemyTick();
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        GetComponent<Animator>().Play(1);
        if (CurrentHealth <= 0)
        {
            GetComponentInChildren<ParticleSystem>().Play(false);
            Destroy(gameObject);
            // TODO: maybe spawn a heart?
        }
    }
    abstract public void EnemyTick();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            TakeDamage(_playerSettings.MagicDamage);
            Vector3 direction = transform.position - other.transform.position;
            direction.Normalize();
            KnockBack(direction, _playerSettings.EnemyKnockBackForce);
            Destroy(other.gameObject);
            OnDamage();
        }
        if (other.CompareTag("Sword"))
        {
            TakeDamage(_playerSettings.SwordDamage);
            Vector3 direction = transform.position - other.transform.position;
            direction.Normalize();
            KnockBack(direction, _playerSettings.EnemyKnockBackForce);
            OnDamage();
        }
    }

    private void KnockBack(Vector3 direction, float force)
    {
        _rb.AddForce(direction * force, ForceMode.Impulse);
    }
    abstract protected void OnDamage();
    private void OnDestroy()
    {
        _gameManager.EnemyKilled(this);
    }
}
