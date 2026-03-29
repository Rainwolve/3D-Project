using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    #region Variables/References

    //StateManager Itself
    PlayerStateFactory stateFactory;
    private PlayerBaseState currentState;
    private bool isTransitioning;
    [SerializeField] bool sendStateChangeDebug;


    //Animator
    private Animator animator;

    private readonly int walkAnimationHash = Animator.StringToHash("IsWalking");
    private readonly int runAnimationHash = Animator.StringToHash("IsRunning");
    private readonly int danceAnimationHash = Animator.StringToHash("IsDancing");

    //Movement
    [SerializeField] private float movementSpeed = 2.0f;

     CharacterController characterController;
    private InputSystem_Actions inputSystem;

    private Vector2 currentMovementInput;
    private Vector3 appliedMovement;
    private bool isMovementPressed;
    private bool isRunPressed;
    private bool isDancePressed;

    private float rotationFactorPerFrame = 15;
    private float runMultiplier = 4.0f;

    #region Getters/Setters

    public int WalkAnimationHash
    {
        get { return walkAnimationHash; }
    }

    public int RunAnimationHash
    {
        get { return runAnimationHash; }
    }

    public int DanceAnimationHash
    {
        get { return danceAnimationHash; }
    }

    public CharacterController CharacterController
    {
        get { return characterController; }
    }

    public PlayerBaseState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    public Animator Animator
    {
        get { return animator; }
    }

    public bool IsMovementPressed
    {
        get { return isMovementPressed; }
    }

    public float RunMultiplier
    {
        get { return runMultiplier; }
    }

    public bool IsRunPressed
    {
        get { return isRunPressed; }
    }

    public bool IsDancePressed
    {
        get { return isDancePressed; }
    }
    public Vector2 CurrentMovementInput
    {
        get { return currentMovementInput; }
    }

    public float AppliedMovementX
    {
        get { return appliedMovement.x; }
        set { appliedMovement.x = value; }
    }

    public float AppliedMovementZ
    {
        get { return appliedMovement.z; }
        set { appliedMovement.z = value; }
    }

    #endregion
    #endregion

    private void Awake()
    {
        inputSystem = new InputSystem_Actions();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        stateFactory = new PlayerStateFactory(this);
        currentState = stateFactory.CreateIdleState();
        currentState.EnterState();
        
    
    }
    private void Start()
    {
        appliedMovement.y = -9.81f;
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        Debug.Log(isMovementPressed);
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    private void OnDance(InputAction.CallbackContext context)
    {
        isDancePressed = context.ReadValueAsButton();
        Debug.Log(isDancePressed);
    }

   

    private void OnEnable()
    {
        inputSystem.Enable();
        
        inputSystem.Player.Move.started += OnMovement;
        inputSystem.Player.Move.canceled += OnMovement;
        inputSystem.Player.Move.performed += OnMovement;
        inputSystem.Player.Sprint.started += OnSprint;
        inputSystem.Player.Sprint.canceled += OnSprint;
    }

    private void OnDisable()
    {
        inputSystem.Disable();
        
        inputSystem.Player.Move.started -= OnMovement;
        inputSystem.Player.Move.canceled -= OnMovement;
        inputSystem.Player.Move.performed -= OnMovement;
        inputSystem.Player.Sprint.started -= OnSprint;
        inputSystem.Player.Sprint.canceled -= OnSprint;
    }

    private void Update()
    {
        HandleRotation();
        currentState.UpdateState();
        characterController.Move((Time.deltaTime * movementSpeed * appliedMovement));
    }

    private void HandleRotation()
    {
        //What to look at
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovementInput.x;
        positionToLookAt.y = 0;
        positionToLookAt.z = currentMovementInput.y;

        //currrent rotation
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            //New Rotation
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);

            //rotate Character
            transform.rotation =
                Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }

    public void SwitchState(PlayerBaseState newState)
    {
        if (isTransitioning)
        {
            return;
        }

        isTransitioning = true;
        currentState = newState;
        currentState.EnterState();
        isTransitioning = false;
    }

    public void SendStateDebug(string message)
    {
        if(sendStateChangeDebug)
            Debug.Log(message);
    }
}