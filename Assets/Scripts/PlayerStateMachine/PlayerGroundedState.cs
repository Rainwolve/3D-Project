using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) 
        : base(stateManager, stateFactory)
    {
        IsRootState = true;
        InitializeSubState();
        
    }
    public override void EnterState()
    {
        stateManager.SendStateDebug("GroundedSuperState Entered");
        stateManager.AppliedMovementY = stateManager.GroundGrav;
        stateManager.CurrentMovementY = stateManager.GroundGrav;


    }

    public override void UpdateState()
    {
       CheckSwitchState();
    }
    
    private void CheckSwitchState()
    {
        if (stateManager.IsJumpPressed && !stateManager.NeedNewJumpInput)
        {
            stateManager.SwitchState(stateFactory.CreateJumpState());
        }
       
    }

    public override void ExitState()
    {
    }

    public void InitializeSubState()
    {
        if (stateManager.IsMovementPressed && stateManager.IsRunPressed)
        {
            SubState = stateFactory.CreateRunState();
        }
        else if (stateManager.IsMovementPressed&& !stateManager.IsRunPressed)
        {
            SubState = stateFactory.CreateWalkState();
        }
        else
        {
            SubState = stateFactory.CreateIdleState();
        }
    }
}
