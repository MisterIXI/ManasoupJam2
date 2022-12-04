using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockBoss : Enemy
{
    [SerializeField] private GameObject _ShotPrefab;
    private float _startAngle = 0;
    private float _randomAngle;
    private bool _turningLeft = true;
    private bool _isPausing = false;
    private float _lastTriggerTime;
    private float _targetTime;
    private float _lastShotTime;
    protected override void Init()
    {
        RollRotation();
        CurrentHealth = Random.Range(_playerSettings.BossHealthRange.x, _playerSettings.BossHealthRange.y + 1);
        _gameManager.SetBossHealthBarMax();
    }
    public override void EnemyTick()
    {
        transform.LookAt(_playerTransform);
        transform.RotateAround(transform.position, Vector3.up, 90);
        if (_isPausing)
        { //Pause shooting
            if (Time.time >= _targetTime)
            {
                _isPausing = false;
                RollRotation();
            }
        }
        else
        { // shoot and rotate
            if (Time.time >= _targetTime)
            {
                _isPausing = true;
                _targetTime = Time.time + _playerSettings.Boss_PauseTime;
                _startAngle = _randomAngle;
                return;
            }
            float t = (Time.time - _lastTriggerTime) / (_targetTime - _lastTriggerTime);
            float angle = Mathf.Lerp(_startAngle, _randomAngle, t);
            ShootAtAngle(angle);
        }
    }

    private void RollRotation()
    {
        _randomAngle = Random.Range(_playerSettings.BossAngleRange.x, _playerSettings.BossAngleRange.y);
        if (Random.value > 0.5f)
            _randomAngle *= -1;
        _lastTriggerTime = Time.time;
        _targetTime = Time.time + Mathf.Abs(_randomAngle - _startAngle) / _playerSettings.Boss_AnglePerSecond;

    }

    private void ShootAtAngle(float angle)
    {
        if (Time.time >= _lastShotTime + _playerSettings.Boss_ShotDelay)
        {
            _lastShotTime = Time.time;
            for (int i = 0; i < 4; i++)
            {
                Quaternion rotation = Quaternion.Euler(0, angle + i * 90, 0);
                GameObject projectile = Instantiate(_ShotPrefab, transform.position, rotation);
            }
        }
    }
    protected override void OnDamage()
    {
        _gameManager.UpdateBossHealthBar();
        Debug.Log("Boss health: " + CurrentHealth);
    }
}
