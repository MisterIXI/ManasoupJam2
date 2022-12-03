using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public PlayerSettings PlayerSettings;
    private Vector2 _moveInput;
    private Vector3 _aimInput;
    private Vector3 _aimReticle = Vector3.forward * 5;
    private bool _isReticleMouseMode = true;
    private Rigidbody _rb;
    private Vector3 _movement;
    private PlayerAction _playerAction;
    private void Awake()
    {
        ReferenceManager.PlayerController = this;
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerAction = GetComponent<PlayerAction>();
        SubscribeToInputEvents();
    }

    private void FixedUpdate()
    {
        Move();
        Aim();
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        _rb.MovePosition(transform.position + moveDirection * PlayerSettings.MoveSpeed * Time.deltaTime);
    }

    private void Aim()
    {
        float oldY = transform.eulerAngles.y;
        if (_isReticleMouseMode)
        { // if aiming with mouse
            Vector3 target = new Vector3(_aimInput.x, transform.position.y, _aimInput.z);
            transform.LookAt(target);
            _aimReticle = _aimInput;
        }
        else
        { // if aiming with controller
            Vector3 target = new Vector3(_aimInput.x, 0, _aimInput.z) + transform.position;
            transform.LookAt(target);
            _aimReticle = transform.forward * 5 + transform.position;
        }
        float deltaY = Mathf.DeltaAngle(oldY, transform.eulerAngles.y);
        if (deltaY < -PlayerSettings.SlashDirectionDeltaMin)
            _playerAction.SetSlashDir(true);
        else if (deltaY > PlayerSettings.SlashDirectionDeltaMin)
            _playerAction.SetSlashDir(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_aimReticle, 0.3f);
    }

    #region Input Events
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _moveInput = Vector2.zero;
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.control.device is Mouse)
        {
            Ray ray = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, LayerMask.GetMask("Ground")))
            {
                _aimInput = hit.point;
            }
            _isReticleMouseMode = true;
        }
        else if (context.control.device is Gamepad)
        {
            Vector2 input = context.ReadValue<Vector2>();
            _aimInput = new Vector3(input.x, 0f, input.y) * 5;
            _isReticleMouseMode = false;
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
    }


    public void OnSlash(InputAction.CallbackContext context)
    {

    }
    public void OnBlock(InputAction.CallbackContext context)
    {
    }
    #endregion

    #region Input Subscriptions
    private void SubscribeToInputEvents()
    {
        var input = GetComponent<PlayerInput>().currentActionMap;
        input["Move"].started += this.OnMove;
        input["Move"].performed += this.OnMove;
        input["Move"].canceled += this.OnMove;
        input["Aim"].started += this.OnAim;
        input["Aim"].performed += this.OnAim;
        input["Aim"].canceled += this.OnAim;
        input["Fire"].started += _playerAction.Shoot;
        input["Fire"].performed += _playerAction.Shoot;
        input["Fire"].canceled += _playerAction.Shoot;
        input["Slash"].started += _playerAction.Slash;
        input["Slash"].performed += _playerAction.Slash;
        input["Slash"].canceled += _playerAction.Slash;
        input["Block"].started += _playerAction.Block;
        input["Block"].performed += _playerAction.Block;
        input["Block"].canceled += _playerAction.Block;
    }

    private void UnSubscribeToInputEvents()
    {
        var input = GetComponent<PlayerInput>().currentActionMap;
        input["Move"].started -= this.OnMove;
        input["Move"].performed -= this.OnMove;
        input["Move"].canceled -= this.OnMove;
        input["Aim"].started -= this.OnAim;
        input["Aim"].performed -= this.OnAim;
        input["Aim"].canceled -= this.OnAim;
        input["Fire"].started -= _playerAction.Shoot;
        input["Fire"].performed -= _playerAction.Shoot;
        input["Fire"].canceled -= _playerAction.Shoot;
        input["Slash"].started -= _playerAction.Slash;
        input["Slash"].performed -= _playerAction.Slash;
        input["Slash"].canceled -= _playerAction.Slash;
        input["Block"].started -= _playerAction.Block;
        input["Block"].performed -= _playerAction.Block;
        input["Block"].canceled -= _playerAction.Block;
    }
    #endregion
}
