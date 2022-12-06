using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public int CurrentHealth { get; private set; }
    private PlayerSettings _playerSettings;
    private PlayerController _playerController;
    private void Start()
    {
        _playerController = ReferenceManager.PlayerController;
        _playerSettings = _playerController.PlayerSettings;
        CurrentHealth = _playerSettings.MaxHealth;
    }
    public void AddHealth(int amount)
    {
        CurrentHealth += amount;
        Debug.Log("Health increase + " + amount + " : Current Health:"+ CurrentHealth);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Hit with tag: " + other.tag);
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyProjectile"))
        {
            CurrentHealth -= 1;
            Debug.Log("Hit! CurrentHealth: " + CurrentHealth);
            Destroy(other.gameObject);
            ReferenceManager.GameManager.UpdateHealth(CurrentHealth);
            if (CurrentHealth <= 0)
            {
                ReferenceManager.GameManager.SetState(GameManager.GameState.GameOver);
            }
        }
        if(other.CompareTag("Collectable"))
        {
            AddHealth(1);
            Destroy(other.gameObject);
        }
    }
}
