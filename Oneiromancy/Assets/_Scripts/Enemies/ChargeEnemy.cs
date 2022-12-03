using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : Enemy
{
    private bool IsCharging;
    private float ChargeStartTime;
    protected override void Init()
    {
        CurrentHealth = Random.Range(_playerSettings.ChargeEnemyHealthRange.x, _playerSettings.ChargeEnemyHealthRange.y + 1);
        _rb.centerOfMass = Vector3.zero;
    }

    public override void EnemyTick()
    {
        // float Distance = Vector3.Distance(transform.position, _playerTransform.position);
        // if(Distance < _playerSettings.ChargeEnemyChargeRange)
        // {
        //     if(!IsCharging)
        //     {
        //         IsCharging = true;
        //         ChargeStartTime = Time.time;
        //     }
        //     if(Time.time - ChargeStartTime > _playerSettings.ChargeEnemyChargeTime)
        //     {
        //         Charge();
        //     }
        // }
        // else
        // {
        //     IsCharging = false;
        //     Vector3 Target = _playerTransform.position;
        //     Target.y = transform.position.y;
        //     transform.LookAt(Target);
        //     Vector3 MoveTarget = transform.position + transform.forward * _playerSettings.EnemySpeed * Time.fixedDeltaTime;
        //     MoveTarget.y = transform.position.y;
        //     _rb.MovePosition(MoveTarget);
        // }
    }
}
