using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputBehavior : MonoBehaviour
{

    public delegate void PlayerInputEventHandler();

 

    [SerializeField] private PlayerControls playerInputActions;    //the player input action map


    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;                //character animator


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
    }


    private void OnEnable()
    {
        playerInputActions = new PlayerControls();
        playerInputActions.Enable();

       
        playerInputActions.Default.Movement.performed += ctx => _animator.SetBool("IsMoving", true);
        playerInputActions.Default.Movement.canceled += ctx => _animator.SetBool("IsMoving", false);

        playerInputActions.Default.Attack.performed += ctx => HandleAttack();

        playerInputActions.Default.Dodge.performed += ctx => StartCoroutine(Dash());
    }

    private void OnDisable()
    {
        

        playerInputActions.Disable();
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        if (_isDashing)
        {
            return;
        }

        Vector3 movementDirection = new Vector3(playerInputActions.Default.Movement.ReadValue<Vector3>().x,  0, playerInputActions.Default.Movement.ReadValue<Vector3>().y);
        movementDirection.Normalize();

        transform.Translate(movementDirection * _speed  * Time.deltaTime,  Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
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
        
        //dash in the direction the player is moving
        
        //get the current direction and store it in a variable

        Vector3 currentDirection = playerInputActions.Default.Movement.ReadValue<Vector3>().normalized;

        Debug.Log("Dash at " +  currentDirection);

        _rigidbody.linearVelocity = new Vector3(currentDirection.x * _dashPower, 0, currentDirection.z * _dashPower);

        yield return new WaitForSeconds(_dashingTime);

        _rigidbody.isKinematic = true;

        _isDashing = false;
        yield return new WaitForSeconds(_dashingCooldown);
    }
    
}
