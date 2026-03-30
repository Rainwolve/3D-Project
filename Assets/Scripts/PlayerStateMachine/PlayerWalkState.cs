using UnityEngine;

public class PlayerWalkState: PlayerBaseState
{
    public PlayerWalkState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) 
        : base(stateManager, stateFactory)
    {
    }

    public override void EnterState()
    {
        stateManager.SendStateDebug("WalkState Entered");
        stateManager.Animator.SetBool(stateManager.WalkAnimationHash, true);
        stateManager.Animator.SetBool(stateManager.RunAnimationHash, false);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        stateManager.AppliedMovementX = stateManager.CurrentMovementInput.x;
        stateManager.AppliedMovementZ = stateManager.CurrentMovementInput.y;
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
        else if (stateManager.IsMovementPressed && stateManager.IsRunPressed)
        {
            stateManager.SwitchState(stateFactory.CreateRunState());
        } 
    }
}

