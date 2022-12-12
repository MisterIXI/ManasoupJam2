using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy
{
    [SerializeField] private GameObject _enemyProjectilePrefab;
    private float _lastShotTime;
    protected override void Init()
    {
        
        damageAnim =  "ShooterEnemy 1";
    }
    public override void EnemyTick()
    {
        float distance = Vector3.Distance(transform.position, _playerTransform.position);
        Vector3 target = _playerTransform.position;
        target.y = transform.position.y;
        transform.LookAt(target);
        if (distance < _playerSettings.SE_ShotRange)
        {
            Shoot();
        }
        else
        {
            Vector3 moveTarget = transform.position + transform.forward * _playerSettings.EnemySpeed * Time.fixedDeltaTime;
            moveTarget.y = transform.position.y;
            _rb.MovePosition(moveTarget);
        }
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }
    protected override void OnDamage()
    {

    }
    private void Shoot()
    {
        if (Time.time - _lastShotTime > _playerSettings.SE_ShotCooldown)
        {
            _lastShotTime = Time.time;
            GameObject projectile = Instantiate(_enemyProjectilePrefab, transform.position, transform.rotation);
        }
    }

}
