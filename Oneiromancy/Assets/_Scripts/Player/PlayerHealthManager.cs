using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public int CurrentHealth { get; private set; }
    private PlayerSettings _playerSettings;
    private PlayerController _playerController;
    private PlayerAnimation _playerAnim;
    private void Start()
    {
        _playerController = ReferenceManager.PlayerController;
        _playerSettings = _playerController.PlayerSettings;
        _playerAnim = GetComponentInChildren<PlayerAnimation>();
        CurrentHealth = 3;
    }
    private void AddHealth(int amount)
    {
        
        if(CurrentHealth != _playerSettings.MaxHealth)
        {
        CurrentHealth += amount;
        
        Debug.Log("Health increase + " + amount + " : Current Health: "+ CurrentHealth);
        }
        else{
            Debug.Log("MaxHealth -  WantToAdd: " + amount);
        }
        ReferenceManager.GameManager.UpdateHealth(CurrentHealth);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Hit with tag: " + other.tag);
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyProjectile"))
        {
            CurrentHealth -= 1;
            _playerAnim.PlayerDamage();
            Debug.Log("Hit! CurrentHealth: " + CurrentHealth);
            if(other.CompareTag("EnemyProjectile"))
                Destroy(other.gameObject);
            ReferenceManager.GameManager.UpdateHealth(CurrentHealth);
            if (CurrentHealth <= 0)
            {
                _playerAnim.PlayerDeath();
                ReferenceManager.GameManager.SetState(GameManager.GameState.GameOver);
            }
        }
        if(other.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            AddHealth(1);
        }
    }
}
