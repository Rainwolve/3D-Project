using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    #region Variables/References

    //References
    [SerializeField] private AttackManager attackManager;

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
    private readonly int jumpAnimationHash = Animator.StringToHash("IsJumping");
    private readonly int danceStartAnimationHash = Animator.StringToHash("StartDance");

    //Movement
    [SerializeField] private float movementSpeed = 2.0f;

    CharacterController characterController;
    private InputSystem_Actions inputSystem;

    private Vector2 currentMovementInput;
    private Vector3 appliedMovement;
    private bool isMovementPressed;
    private bool isRunPressed;
    private bool isDancePressed;

    //Attack
    [SerializeField] private int hp;
    private bool isAttacking;
    private bool isHitboxActive;
    bool isMovementStopped;
    

    //Hurt/Damage
    [SerializeField]private bool isHurt;
    private bool canTakeDamage;


    private float rotationFactorPerFrame = 15;
    private float runMultiplier = 4.0f;

    //Jump
    private bool isJumpPressed;
    private bool isJumping;
    private float currentMovementY;
    private bool needNewJumpInput;
    private float initialJumpVelocity;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private float maxJumpHeight;
    private float gravity;
    private float groundGrav = 0.05f;
    private readonly float jumpMult = 4;

    #region Getters/Setters

    public bool IsHurt
    {
        get => isHurt;
        set => isHurt = value;
    }

    public int Hp
    {
        get => hp;
        set => hp = value;
    }

    public bool CanTakeDamage
    {
        get => canTakeDamage;
        set => canTakeDamage = value;
    }

    public bool IsHitboxActive
    {
        get => isHitboxActive;
        set => isHitboxActive = value;
    }

    public float JumpMult => jumpMult;

    public float CurrentMovementY
    {
        get => currentMovementY;
        set => currentMovementY = value;
    }

    public bool IsJumping
    {
        get => isJumping;
        set => isJumping = value;
    }

    public AttackManager AttackManager
    {
        get => attackManager;
        set => attackManager = value;
    }

    public bool NeedNewJumpInput
    {
        get => needNewJumpInput;
        set => needNewJumpInput = value;
    }

    public float InitialJumpVelocity => initialJumpVelocity;

    public float Gravity => gravity;

    public float GroundGrav => groundGrav;

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

    public int DanceStartAnimationHash
    {
        get { return danceStartAnimationHash; }
    }

    public int JumpAnimationHash
    {
        get { return jumpAnimationHash; }
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

    public bool IsAttacking
    {
        get => isAttacking;
        set => isAttacking = value;
    }

    public Animator Animator
    {
        get { return animator; }
    }

    public bool IsMovementPressed
    {
        get { return isMovementPressed; }
    }

    public bool IsJumpPressed
    {
        get { return isJumpPressed; }
    }

    public float RunMultiplier
    {
        get { return runMultiplier; }
    }

    public bool IsRunPressed
    {
        get { return isRunPressed; }
    }

    public bool IsMovementStopped
    {
        get => isMovementStopped;
        set => isMovementStopped = value;
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

    public float AppliedMovementY
    {
        get { return appliedMovement.y; }
        set { appliedMovement.y = value; }
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
        currentState = stateFactory.CreateGroundedState();
        currentState.EnterState();
        CalcJumpVariables();
    }

    private void CalcJumpVariables()
    {
        float timeToApex = 0.5f * maxJumpTime;
        gravity = (-2 * maxJumpTime) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    #region Events

    private void OnEnable()
    {
        inputSystem.Enable();

        inputSystem.Player.Move.started += OnMovement;
        inputSystem.Player.Move.canceled += OnMovement;
        inputSystem.Player.Move.performed += OnMovement;
        inputSystem.Player.Sprint.started += OnSprint;
        inputSystem.Player.Sprint.canceled += OnSprint;
        inputSystem.Player.Jump.started += OnJump;
        inputSystem.Player.Jump.canceled += OnJump;
        inputSystem.Player.Dance.started += OnDance;
        inputSystem.Player.Dance.canceled += OnDance;
        inputSystem.Player.Attack.started += OnAttack;
        inputSystem.Player.Attack.canceled += OnAttack;
        GameEvents.OnPlayerHurt += OnHurt;
    }

    private void OnDisable()
    {
        inputSystem.Disable();

        inputSystem.Player.Move.started -= OnMovement;
        inputSystem.Player.Move.canceled -= OnMovement;
        inputSystem.Player.Move.performed -= OnMovement;
        inputSystem.Player.Sprint.started -= OnSprint;
        inputSystem.Player.Sprint.canceled -= OnSprint;
        inputSystem.Player.Jump.started -= OnJump;
        inputSystem.Player.Jump.canceled -= OnJump;
        inputSystem.Player.Dance.started -= OnDance;
        inputSystem.Player.Dance.canceled -= OnDance;
        GameEvents.OnPlayerHurt -= OnHurt;
    }


    private void OnMovement(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    private void OnDance(InputAction.CallbackContext context)
    {
        isDancePressed = context.ReadValueAsButton();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        needNewJumpInput = false;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        isAttacking = context.ReadValueAsButton();
    }

    public void OnAttackDone()
    {
        isAttacking = false;
        attackManager.SphereCollider.enabled = false;
    }

    public void OnDealDamage()
    {
        attackManager.SphereCollider.enabled = true;
    }

    private void OnHurt(int dmg)
    {
        if (canTakeDamage) isHurt = true;
    }

    #endregion

    #region Other Methods

    private void Update()
    {
        if (!IsMovementStopped) HandleRotation();
        currentState.UpdateStates();
        if (!isMovementStopped) characterController.Move((Time.deltaTime * movementSpeed * appliedMovement));
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
        if (isTransitioning) return;

        isTransitioning = true;
        currentState?.ExitStates();
        newState.EnterState();
        if (newState.IsRootState)
        {
            currentState = newState;
        }
        else if (currentState != null)
        {
            currentState.SubState = newState;
        }

        isTransitioning = false;
    }

    public void SendStateDebug(string message)
    {
        if (sendStateChangeDebug)
            Debug.Log(message);
    }

    #endregion
}