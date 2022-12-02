using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerSettings _playerSettings;
    private Vector2 _moveInput;
    private Vector3 _aimInput;
    private Vector3 _aimReticle = Vector3.forward * 5;
    private bool _isReticleMouseMode = true;
    private Rigidbody _rb;
    private Vector3 _movement;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
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
        _rb.MovePosition(transform.position + moveDirection * _playerSettings.MoveSpeed * Time.deltaTime);
    }

    private void Aim()
    {
        Vector3 target = new Vector3(_aimInput.x, 0, _aimInput.z) + transform.position;
        transform.LookAt(target);
        if (_isReticleMouseMode) // if aiming with mouse
            _aimReticle = _aimInput + transform.position;
        else // if aiming with controller
            _aimReticle = transform.forward * 5 + transform.position;
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
                _aimInput = hit.point - transform.position;
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
        input["Fire"].started += this.OnFire;
        input["Fire"].performed += this.OnFire;
        input["Fire"].canceled += this.OnFire;
        input["Slash"].started += this.OnSlash;
        input["Slash"].performed += this.OnSlash;
        input["Slash"].canceled += this.OnSlash;
        input["Block"].started += this.OnBlock;
        input["Block"].performed += this.OnBlock;
        input["Block"].canceled += this.OnBlock;
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
        input["Fire"].started -= this.OnFire;
        input["Fire"].performed -= this.OnFire;
        input["Fire"].canceled -= this.OnFire;
        input["Slash"].started -= this.OnSlash;
        input["Slash"].performed -= this.OnSlash;
        input["Slash"].canceled -= this.OnSlash;
        input["Block"].started -= this.OnBlock;
        input["Block"].performed -= this.OnBlock;
        input["Block"].canceled -= this.OnBlock;
    }
    #endregion
}
