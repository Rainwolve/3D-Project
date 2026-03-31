using UnityEngine;

public class PlayerRunState: PlayerBaseState
{
    public PlayerRunState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) 
        : base(stateManager, stateFactory)
    {
    }

    public override void EnterState()
    {
        stateManager.SendStateDebug("RunState Entered");
        stateManager.Animator.SetBool(stateManager.WalkAnimationHash, true);
        stateManager.Animator.SetBool(stateManager.RunAnimationHash, true);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        stateManager.AppliedMovementX = stateManager.CurrentMovementInput.x * stateManager.RunMultiplier;
        stateManager.AppliedMovementZ = stateManager.CurrentMovementInput.y * stateManager.RunMultiplier;
    }
    
    public override void ExitState()
    {
        
    }
    
    private void CheckSwitchState()
    {
        if (!stateManager.IsMovementPressed)
        {
            stateManager.SwitchState(stateFactory.CreateIdleState());
        }
        else if (stateManager.IsMovementPressed && !stateManager.IsRunPressed)
        {
            stateManager.SwitchState(stateFactory.CreateWalkState());
        } 
        if (stateManager.IsAttacking)
        {
            stateManager.SwitchState(stateFactory.CreateAttackState());
        }
    }
}

