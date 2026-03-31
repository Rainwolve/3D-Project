using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) 
        : base(stateManager, stateFactory)
    {
    }

    public override void EnterState()
    {
        stateManager.SendStateDebug("IdleState Entered");
        stateManager.Animator.SetBool(stateManager.WalkAnimationHash, false);
        stateManager.Animator.SetBool(stateManager.RunAnimationHash, false);
        stateManager.Animator.SetBool(stateManager.DanceAnimationHash, false);
        stateManager.AppliedMovementX = 0;
        stateManager.AppliedMovementZ = 0;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    
    public override void ExitState()
    {
        
    }
    
    private void CheckSwitchStates()
    {
        if (stateManager.IsMovementPressed && stateManager.IsRunPressed)
        {
            stateManager.SwitchState(stateFactory.CreateRunState());
        }
        else if (stateManager.IsMovementPressed)
        {
            stateManager.SwitchState(stateFactory.CreateWalkState());
        }
        else if (stateManager.IsDancePressed)
        {
            stateManager.SwitchState(stateFactory.CreateDanceState());
        }

        if (stateManager.IsAttacking)
        {
            stateManager.SwitchState(stateFactory.CreateAttackState());
        }
    }
}