using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputBehavior : MonoBehaviour
{

    public delegate void PlayerInputEventHandler();

    public static event PlayerInputEventHandler OnDodgeStarted;
    public static event PlayerInputEventHandler OnDodgeEnded;
    public static event PlayerInputEventHandler OnAttack;

    [SerializeField] private PlayerControls playerInputActions;    //the player input action map


    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;                //character animator
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private CapsuleCollider _playerCollider;

    [SerializeField] private float _speed;
  
    [SerializeField] private float _rotationSpeed;

    [Header("DashProperties")]
    [SerializeField] private bool _isDashing;
    [SerializeField] private bool _canDash = true;
    [SerializeField] private float _dashPower;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCooldown = 1f;

   




    private void Awake()
    {
       
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _playerCollider = GetComponent<CapsuleCollider>();
       
       
    }


    private void OnEnable()
    {
        playerInputActions = new PlayerControls();
        playerInputActions.Enable();

       
        playerInputActions.Default.Movement.performed += ctx => _animator.SetBool("IsMoving", true);
        playerInputActions.Default.Movement.canceled += ctx => _animator.SetBool("IsMoving", false);

        playerInputActions.Default.Attack.performed +=ctx => OnAttack?.Invoke();

        playerInputActions.Default.Dodge.performed += ctx => StartCoroutine(Dash());
    }

    private void OnDisable()
    {
        playerInputActions.Disable();

        playerInputActions.Default.Movement.performed -= ctx => _animator.SetBool("IsMoving", true);
        playerInputActions.Default.Movement.canceled -= ctx => _animator.SetBool("IsMoving", false);

        playerInputActions.Default.Attack.performed -= ctx => OnAttack?.Invoke();

        playerInputActions.Default.Dodge.performed -= ctx => StartCoroutine(Dash());
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
       
    }


    private void FixedUpdate()
    {
        //HandleMovement();
    }

    public void HandleMovement()
    {
        if (_isDashing)
        {
            return;
        }

        Vector3 movementDirection = new Vector3(playerInputActions.Default.Movement.ReadValue<Vector3>().x / Time.timeScale,  0, playerInputActions.Default.Movement.ReadValue<Vector3>().y /Time.timeScale);
        movementDirection.Normalize();

        transform.Translate(movementDirection * _speed  * Time.unscaledDeltaTime,  Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.unscaledDeltaTime);
            
        }

    }


    public void HandleAttack()
    {
        Debug.Log("attacking");   
    }

   public IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;

        _rigidbody.isKinematic = false;

      
        
        //get the current direction and store it in a variable
        Vector3 currentDirection = playerInputActions.Default.Movement.ReadValue<Vector3>().normalized;


        //dash in the direction the player is moving
        _rigidbody.AddForce(currentDirection.x * _dashPower / Time.timeScale, 0, currentDirection.z * _dashPower / Time.timeScale, ForceMode.VelocityChange);

        //_rigidbody.linearVelocity = new Vector3(currentDirection.x * _dashPower * Time.fixe, currentDirection.y * _dashPower, currentDirection.z * _dashPower);
        _trailRenderer.emitting = true;

        //enable the dash dodge trigger
        OnDodgeStarted?.Invoke();
        _playerCollider.enabled = false;

        yield return new WaitForSecondsRealtime(_dashingTime);
    

        //disable the dash dodge trigger
        OnDodgeEnded?.Invoke();
        _playerCollider.enabled = true;


        _rigidbody.isKinematic = true;
        _trailRenderer.emitting = false;

        _isDashing = false;
        yield return new WaitForSecondsRealtime(_dashingCooldown);
    }
    
}
