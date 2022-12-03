using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : Enemy
{
    private bool IsCharging;
    private float _lastChargeTime;
    protected override void Init()
    {
        CurrentHealth = Random.Range(_playerSettings.ChargeEnemyHealthRange.x, _playerSettings.ChargeEnemyHealthRange.y + 1);
        _rb.centerOfMass = Vector3.zero;
    }

    public override void EnemyTick()
    {
        float Distance = Vector3.Distance(transform.position, _playerTransform.position);
        if (!IsCharging)
        {
            Vector3 Target = _playerTransform.position;
            Target.y = transform.position.y;
            transform.LookAt(Target);
            if (Time.time - _lastChargeTime > _playerSettings.CE_Cooldown && Distance < _playerSettings.CE_Range)
            {
                IsCharging = true;
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
                StartCoroutine(Charge());
            }
            else
            {
                Vector3 MoveTarget = transform.position + transform.forward * _playerSettings.EnemySpeed * Time.fixedDeltaTime;
                MoveTarget.y = transform.position.y;
                _rb.MovePosition(MoveTarget);
            }
        }
    }

    IEnumerator Charge()
    {
        yield return new WaitForSeconds(_playerSettings.CE_ChargeTime);
        _lastChargeTime = Time.time;

        IsCharging = false;
        // impluse forward
        Debug.Log("Charge");
        _rb.AddForce(transform.forward * _playerSettings.CE_ImpluseMultiplier * 20f, ForceMode.Impulse);
    }
}
