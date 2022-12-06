using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearth : MonoBehaviour
{
     [SerializeField] private PlayerHealthManager playerHealthManager;
    
    private void OnTriggerEnter(Collider other) {
        if(gameObject.tag== "Player")
        {
            playerHealthManager.AddHealth(1);
            Destroy(gameObject);
        }
    }
}
