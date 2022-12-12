using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : Enemy
{
    protected override void Init()
    {
        CurrentHealth = Random.Range(_playerSettings.FollowEnemyHealthRange.x, _playerSettings.FollowEnemyHealthRange.y + 1);
        _rb.centerOfMass = Vector3.zero;
        damageAnim =  "Wormii 1";
    }

    public override void EnemyTick()
    {
        // look at player
        Vector3 target = _playerTransform.position;
        target.y = transform.position.y;
        transform.LookAt(target);
        // move forwads
        Vector3 moveTarget = transform.position + transform.forward * _playerSettings.EnemySpeed * Time.fixedDeltaTime;
        moveTarget.y = transform.position.y;
        _rb.MovePosition(moveTarget);
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }
    protected override void OnDamage()
    {

    }
}
