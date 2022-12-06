using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{


    private PlayerSettings _playerSettings;
    private PlayerAnimation _playerAnim;
    private void Start()
    {
        _playerSettings = ReferenceManager.PlayerController.PlayerSettings;
        _playerAnim = GetComponentInChildren<PlayerAnimation>();
        SwordSetup();
        MagicSetup();
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
    private bool _onSlashCD;
    private bool _isSlashing;
    private float _slashStartTime;
    private bool _slashToLeft;
    private bool _isSlashingHeld;
    public void SetSlashDir(bool slashToLeft)
    {
        // if (_isSlashingHeld)
        //     return;
        if ( _slashToLeft != slashToLeft)
        {
            if (_playerSettings.DebugSword)
                Debug.Log("Slash direction changed to " + slashToLeft);
            _slashToLeft = slashToLeft;
            if (!_isSlashing &&_slashToLeft)
                _sword.localRotation = Quaternion.Euler(0, 90, 0);
            else
                _sword.localRotation = Quaternion.Euler(0, -90, 0);
        }

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
        {
            _playerAnim.PlayerAttack();
            SlashIncrement();
        }
    }
    public void Slash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isSlashingHeld = true;
            if (!_onSlashCD)
                StartNewSlash();
        }
        if (context.canceled)
        {
            _isSlashingHeld = false;
        }
    }
    private void StartNewSlash()
    {
        _onSlashCD = true;
        _slashStartTime = Time.time;
        _isSlashing = true;

        SlashIncrement();
        _swordTrail.Clear();
        _swordTrail.enabled = true;
        _swordCollider.enabled = true;
        if (_playerSettings.DebugSword)
            Debug.Log("Slash Started");
    }
    IEnumerator SlashCooldown()
    {
        yield return new WaitForSeconds(_playerSettings.SlashCooldown);
        _onSlashCD = false;
        if (_isSlashingHeld)
        {
            StartNewSlash();
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
            _slashToLeft = !_slashToLeft;
            StartCoroutine(SlashCooldown());

            if (_playerSettings.DebugSword)
                Debug.Log("Slash End");

            return;
        }
        float lerp = _playerSettings.Lerp(t, _playerSettings.SlashLerpType) * 180f - 90f;
        if (_slashToLeft)
            lerp = -lerp;

        _sword.localRotation = Quaternion.Euler(0, lerp, 0);
        // lerp += transform.eulerAngles.y;
        // _swordRb.MoveRotation(Quaternion.Euler(0, lerp, 0));
    }

    #endregion
    #region Magic
    [SerializeField] private GameObject _magicPrefab;
    [SerializeField] private GameObject _smokePrefab;
    [SerializeField] private GameObject _magicLaserPointer;
    private int _magicCount;
    private void MagicSetup()
    {
        GameObject ProjectileBounds = new GameObject("ProjectileBounds");
        ProjectileBounds.tag = "Bounds";
        ProjectileBounds.AddComponent<BoxCollider>();
        ProjectileBounds.GetComponent<BoxCollider>().isTrigger = true;
        ProjectileBounds.transform.localScale = _playerSettings.BoundsLength * 2f * Vector3.one;
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _magicLaserPointer.SetActive(true);
        }
        if (context.canceled)
        {
            _playerAnim.PlayerCast();
            _magicLaserPointer.SetActive(false);
            if (_magicCount < _playerSettings.MaxProjectiles)
            {
                Vector3 spawnPoint = transform.position + transform.forward;
                GameObject magic = Instantiate(_magicPrefab, spawnPoint, transform.rotation);
                _magicCount++;
            }
            else{
                Vector3 spawnPoint = transform.position + transform.forward;
                GameObject magic = Instantiate(_smokePrefab, spawnPoint, transform.rotation);
            }
        }
    }

    public void OnProjectileDestroy()
    {
        _magicCount--;
    }

    #endregion

    #region Block


    private bool _isBlocking;


    public Vector3 AdjustMovement(Vector3 movement)
    {
        if (_isBlocking)
            return movement * _playerSettings.BlockSlowdown;
        else
            return movement;
    }

    public void Block(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }
    #endregion


}
