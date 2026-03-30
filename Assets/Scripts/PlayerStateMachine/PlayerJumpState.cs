using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateManager stateManager, PlayerStateFactory stateFactory)
        : base(stateManager, stateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        stateManager.SendStateDebug("JumpSuperState Entered");
        HandleJump();
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        HandleMovement();
        HandleGravity();
    }

    private void CheckSwitchState()
    {
        if (stateManager.CharacterController.isGrounded)
        {
            stateManager.Animator.SetBool(stateManager.JumpAnimationHash, false);
            if (stateManager.IsJumpPressed)
            {
                stateManager.NeedNewJumpInput = true;
            }
            stateManager.SwitchState(stateFactory.CreateGroundedState());
        }
    }

    public override void ExitState()
    {
        stateManager.Animator.SetBool("isJumping", false);
        if (stateManager.IsJumpPressed)
        {
            stateManager.NeedNewJumpInput = true;
        }
    }
    private void HandleJump()
    {
        stateManager.Animator.SetBool(stateManager.JumpAnimationHash, true);
        stateManager.NeedNewJumpInput = true;
        stateManager.IsJumping = true;
        stateManager.CurrentMovementY = stateManager.InitialJumpVelocity;
        stateManager.AppliedMovementY = stateManager.InitialJumpVelocity;
    }

    private void HandleMovement()
    {
        stateManager.AppliedMovementX = stateManager.JumpMult * stateManager.CurrentMovementInput.x;
        stateManager.AppliedMovementZ = stateManager.JumpMult * stateManager.CurrentMovementInput.y;
    }
    private void HandleGravity()
    {
        bool isFalling = stateManager.CurrentMovementY <= 0.0f || !stateManager.IsJumpPressed;
        float fallMultiplier = 2.0f;
        if (isFalling)
        {
            float previousYVelocity = stateManager.CurrentMovementY;
            stateManager.CurrentMovementY += stateManager.Gravity * fallMultiplier * Time.deltaTime;
            stateManager.AppliedMovementY = Mathf.Max((previousYVelocity + stateManager.CurrentMovementY * 0.5f), -20.0f);
        }
        else
        {
            float previousYVelocity = stateManager.CurrentMovementY;
            stateManager.CurrentMovementY += stateManager.Gravity * Time.deltaTime;
            stateManager.AppliedMovementY = (previousYVelocity + stateManager.CurrentMovementY) * 0.5f;
        }
    }
   
}