using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
   // VARIABLES:  projectile, pos, 
   //player, cooldown, bool canShoot, shootCount, playerDistance, shootingDistance

   // BEHAVIOR: move, shoot, ColliderCheck, facePlayerDir
   // UPDATE: Move,FacePlayerDir 
   [SerializeField]private GameObject _projectile;
   [SerializeField]private GameObject _Player;
   public float Shooting_Cooldown = 0.5f;
   public float Shooting_Distance = 10.0f;
   public float Shooting_Player_Min_Distance = 1.0f;
   private Vector3 direction;
   public bool canShoot = true;
   private float waitTime =0f;

   private void Move()
   {

   }
   private void Shoot()
   {
        canShoot = true;
        GameObject _projectileSpawn = Instantiate(_projectile,transform.position,Quaternion.identity);
        _projectileSpawn.GetComponent<Bullet>().Init(_Player,Shooting_Distance);
        
   }
   private void ColliderCheck()
   {

   }
   private void FacePlayerDir()
   {
        direction = _Player.transform.position - transform.position;
        float angle= Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90f,Vector3.forward);
        if (Time.time > waitTime)
        {
            waitTime = Time.time + Shooting_Cooldown;
            Shoot();
        }
   }
}
