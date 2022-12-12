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
    public int healthItemCounter {get;protected set;}
    public string damageAnim{get; protected set;}
    [SerializeField]private Transform _particlesystem;
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
        healthItemCounter = 0;
        damageAnim = "Wormii 1";
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
        gameObject.GetComponentInChildren<Animator>().Play(damageAnim); 
        if (CurrentHealth <= 0)
        {
            Debug.Log("Current Health: " + CurrentHealth);
            gameObject.GetComponentInChildren<ParticleSystem>().Play(true);
            healthItemCounter++;
            if(healthItemCounter >= _playerSettings.HealthDropCount)
            {
                Instantiate(_playerSettings.heartPrefab, gameObject.transform.position, Quaternion.identity);
                healthItemCounter =0;
            }
            if(_particlesystem !=null)
            {
                _particlesystem.parent = null;
                _particlesystem.position= transform.position;
                Destroy(_particlesystem.gameObject,2);
            }
                Destroy(gameObject);
            
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
        if (this is not ClockBoss)
            _rb.AddForce(direction * force, ForceMode.Impulse);
    }
    abstract protected void OnDamage();
    void OnDestroy()
    {
        _gameManager.EnemyKilled(this);
    }
}
