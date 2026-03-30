using UnityEngine;

public class PlayerDanceState: PlayerBaseState
{
    public PlayerDanceState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) 
        : base(stateManager, stateFactory)
    {
    }

    public override void EnterState()
    {
        stateManager.SendStateDebug("DanceState Entered");
        stateManager.Animator.SetBool(stateManager.WalkAnimationHash, false);
        stateManager.Animator.SetBool(stateManager.RunAnimationHash, false);
        stateManager.Animator.SetBool(stateManager.DanceAnimationHash, true);
        stateManager.Animator.SetTrigger(stateManager.DanceStartAnimationHash);
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        stateManager.AppliedMovementX = 0f;
        stateManager.AppliedMovementZ = 0f;
    }
    public override void ExitState()
    {
        
    }
    
    private void CheckSwitchState()
    {
        if (!stateManager.IsDancePressed)
        {
            stateManager.Animator.SetBool(stateManager.DanceAnimationHash, false);
            stateManager.SwitchState(stateFactory.CreateIdleState());
        }
       
    }
}


