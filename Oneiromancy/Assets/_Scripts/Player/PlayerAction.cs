using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{


    [SerializeField] private GameObject _magicPrefab;
    private PlayerSettings _playerSettings;

    private void Start()
    {
        _playerSettings = ReferenceManager.PlayerController.PlayerSettings;
        SwordSetup();
    }

    private void Update()
    {
        SwordUpdate();
    }

    #region Sword
    [Header("Sword")]
    [SerializeField] private Transform _sword;
    [SerializeField] private TrailRenderer _swordTrail;
    private Rigidbody _swordRb;
    private Collider _swordCollider;
    private bool _isSlashing;
    private float _slashStartTime;
    private bool _slashToLeft;
    public void SetSlashDir(bool slashToLeft)
    {
        if (!_isSlashing && _slashToLeft != slashToLeft)
        {
            _slashToLeft = slashToLeft;
            if (_slashToLeft)
                _sword.localRotation = Quaternion.Euler(0, 90, 0);
            else
                _sword.localRotation = Quaternion.Euler(0, -90, 0);
        }
        else
            _slashToLeft = slashToLeft;
    }
    private void SwordSetup()
    {
        _swordRb = _sword.gameObject.GetComponent<Rigidbody>();
        _swordTrail.Clear();
        _swordTrail.enabled = false;
        _swordCollider = _sword.gameObject.GetComponent<Collider>();
        _swordCollider.enabled = false;
    }
    private void SwordUpdate()
    {
        if (_isSlashing)
            SlashIncrement();
    }
    public void Slash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _slashStartTime = Time.time;
            _isSlashing = true;
            _slashToLeft = !_slashToLeft;
            SlashIncrement();
            _swordTrail.Clear();
            _swordTrail.enabled = true;
            _swordCollider.enabled = true;

            if (_playerSettings.DebugSword)
                Debug.Log("Slash Started");
        }
    }

    private void SlashIncrement()
    {
        float t = (Time.time - _slashStartTime) / _playerSettings.SlashDuration;
        if (t >= 1f)
        {
            _isSlashing = false;
            _swordTrail.enabled = false;
            _swordCollider.enabled = false;
            if (_playerSettings.DebugSword)
                Debug.Log("Slash End");
            return;
        }
        float lerp = _playerSettings.Lerp(t, _playerSettings.SlashLerpType) * 180f - 90f;
        if (_slashToLeft)
            lerp = -lerp;

        _sword.localRotation = Quaternion.Euler(0, lerp, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //TODO: deal damage to enemny
            Debug.Log("Hit enemy");
        }
    }
    #endregion
    public void Shoot(InputAction.CallbackContext context)
    {
        Vector3 spawnPoint = transform.position + transform.forward;
        GameObject magic = Instantiate(_magicPrefab, spawnPoint, transform.rotation);
    }

    public void Block(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            
        }
    }
}
