using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDoor : MonoBehaviour
{
    private PlayerSettings _playerSettings;
    private GameManager _gameManager;
    [SerializeField] private ParticleSystem _spawnParticles;
    private float _startTime;
    private float _startY;
    private float _targetY;
    private enum state
    {
        Idle,
        Spawning,
        Despawning
    }
    private state animState;
    private void Awake()
    {
        ReferenceManager.PortalDoor = this;
    }

    private void Start()
    {
        _playerSettings = ReferenceManager.PlayerController.PlayerSettings;
        _gameManager = ReferenceManager.GameManager;
        _gameManager.OnStateChange += OnStateChange;
    }

    private void Update()
    {
        if (animState != state.Idle)
        {
            float t = (Time.time - _startTime) / _playerSettings.PortalAnimDuration;
            _playerSettings.Lerp(t, PlayerSettings.LerpType.EaseInOut);
            float newY = Mathf.Lerp(_startY, _targetY, t);
        }
    }
    public void PlaySpawnAnimation()
    {
        _startTime = Time.time;
        _startY = transform.position.y;
        _targetY = _playerSettings.PortalMaxPosition;
        animState = state.Spawning;
    }


    public void PlayDespawnAnimation()
    {
        _startTime = Time.time;
        _startY = transform.position.y;
        _targetY = _playerSettings.PortalMinPosition;
        animState = state.Despawning;
    }

    public void OnStateChange(GameManager.GameState oldState, GameManager.GameState newState)
    {
        if (newState == GameManager.GameState.Portal)
        {
            PlaySpawnAnimation();
        }
        else if (oldState == GameManager.GameState.Portal)
        {
            PlayDespawnAnimation();
        }
    }

    private void OnDrawGizmos()
    {
        if (_playerSettings != null && _playerSettings.PortalGizmos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(new Vector3(0, _playerSettings.PortalMinPosition, 0), 3f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(new Vector3(0, _playerSettings.PortalMaxPosition, 0), 3f);
        }
    }
}
