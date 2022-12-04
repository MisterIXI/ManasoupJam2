using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public PlayerSettings PlayerSettings;
    public PlayerAction PlayerAction;
    private Vector2 _moveInput;
    private Vector3 _aimInput;
    private Vector3 _aimReticle = Vector3.forward * 5;
    private bool _isReticleMouseMode = true;
    private Rigidbody _rb;
    private Vector3 _actualMovement;
    private GameManager _gameManager;

    private void Awake()
    {
        ReferenceManager.PlayerController = this;
    }
    private void Start()
    {
        _gameManager = ReferenceManager.GameManager;
        _rb = GetComponent<Rigidbody>();
        PlayerAction = GetComponent<PlayerAction>();
        SubscribeToInputEvents();
    }

    private void FixedUpdate()
    {
        Move();
        Aim();
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y) * PlayerSettings.MoveSpeedMult;
        // ease in movement
        _actualMovement = Vector3.MoveTowards(_actualMovement, moveDirection, PlayerSettings.MoveAcceleration * Time.fixedDeltaTime);
        // moveDirection = PlayerAction.AdjustMovement(moveDirection);
        
        _rb.MovePosition(transform.position + _actualMovement);
        if (_isReticleMouseMode)
            _aimInput += _actualMovement;
    }
    private void KnockBack(Vector3 source, float force)
    {
        Vector3 direction = transform.position - source;
        _rb.AddForce(direction.normalized * force, ForceMode.Impulse);
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
        // Debug.Log(deltaY);
        if (deltaY < -PlayerSettings.SlashDirectionDeltaMin)
            PlayerAction.SetSlashDir(true);
        else if (deltaY > PlayerSettings.SlashDirectionDeltaMin)
            PlayerAction.SetSlashDir(false);
    }

    private void OnDrawGizmos()
    {
        // draw aim reticle
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_aimReticle, 0.3f);

        // draw Projectile bounds
        Gizmos.color = Color.blue;
        //right
        float boundLength = PlayerSettings.BoundsLength;
        Vector3 topRight = new(boundLength, 0, boundLength);
        Vector3 bottomRight = new(boundLength, 0, -boundLength);
        Vector3 topLeft = new(-boundLength, 0, boundLength);
        Vector3 bottomLeft = new(-boundLength, 0, -boundLength);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
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
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Bounds"))
        {
            _gameManager.SetState(GameManager.GameState.GameOver);
        }
    }


    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Portal"))
        {
            _gameManager.AdvanceLayer();
        }
    }
    #region Input Subscriptions
    public void SubscribeToInputEvents()
    {
        var input = GetComponent<PlayerInput>().currentActionMap;
        input["Move"].started += this.OnMove;
        input["Move"].performed += this.OnMove;
        input["Move"].canceled += this.OnMove;
        input["Aim"].started += this.OnAim;
        input["Aim"].performed += this.OnAim;
        input["Aim"].canceled += this.OnAim;
        input["Fire"].started += PlayerAction.Shoot;
        input["Fire"].performed += PlayerAction.Shoot;
        input["Fire"].canceled += PlayerAction.Shoot;
        input["Slash"].started += PlayerAction.Slash;
        input["Slash"].performed += PlayerAction.Slash;
        input["Slash"].canceled += PlayerAction.Slash;
        input["Block"].started += PlayerAction.Block;
        input["Block"].performed += PlayerAction.Block;
        input["Block"].canceled += PlayerAction.Block;
    }

    public void UnSubscribeToInputEvents()
    {
        var input = GetComponent<PlayerInput>().currentActionMap;
        input["Move"].started -= this.OnMove;
        input["Move"].performed -= this.OnMove;
        input["Move"].canceled -= this.OnMove;
        input["Aim"].started -= this.OnAim;
        input["Aim"].performed -= this.OnAim;
        input["Aim"].canceled -= this.OnAim;
        input["Fire"].started -= PlayerAction.Shoot;
        input["Fire"].performed -= PlayerAction.Shoot;
        input["Fire"].canceled -= PlayerAction.Shoot;
        input["Slash"].started -= PlayerAction.Slash;
        input["Slash"].performed -= PlayerAction.Slash;
        input["Slash"].canceled -= PlayerAction.Slash;
        input["Block"].started -= PlayerAction.Block;
        input["Block"].performed -= PlayerAction.Block;
        input["Block"].canceled -= PlayerAction.Block;
    }
    #endregion
}
