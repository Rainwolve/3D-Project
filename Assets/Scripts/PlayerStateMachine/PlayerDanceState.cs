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
    }

    public override void UpdateState()
    {
        CheckSwitchState();
        //stateManager.AppliedMovementX = 0f;
        //stateManager.AppliedMovementZ = 0f;
    }
    
    private void CheckSwitchState()
    {
        if (!stateManager.IsMovementPressed)
        {
            stateManager.Animator.SetBool(stateManager.DanceAnimationHash, true);
            stateManager.SwitchState(stateFactory.CreateIdleState());
        }
       
    }
}


