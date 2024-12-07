using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputBehavior : MonoBehaviour
{

    public delegate void PlayerInputEventHandler();

    public event PlayerInputEventHandler OnAttack;

    [SerializeField] private PlayerControls playerInputActions;    //the player input action map


    [SerializeField] private CharacterController _controller;   //character controller
    [SerializeField] private Animator _animator;                //character animator

    [SerializeField] private float _speed;          
    [SerializeField] private Vector3 _playerMoveInput;

    

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        OnAttack += HandleAttack;


        playerInputActions = new PlayerControls();
        playerInputActions.Enable();

       

        playerInputActions.Default.Movement.performed += ctx => _playerMoveInput = ctx.ReadValue<Vector3>().normalized;
        playerInputActions.Default.Movement.performed += ctx => _animator.SetBool("IsMoving", true);

        playerInputActions.Default.Movement.canceled += ctx => _playerMoveInput = Vector3.zero;
        playerInputActions.Default.Movement.canceled += ctx => _animator.SetBool("IsMoving", false);

        playerInputActions.Default.Attack.performed += ctx => OnAttack?.Invoke();
    }

    private void OnDisable()
    {
        OnAttack -= HandleAttack;

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
       Vector3 moveVec = transform.TransformDirection(_playerMoveInput);

        _controller.Move(moveVec * _speed * Time.deltaTime);
    }


    public void HandleAttack()
    {
        Debug.Log("attacking");   
    }
    
}
