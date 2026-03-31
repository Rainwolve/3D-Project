using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateManager stateManager, PlayerStateFactory stateFactory)
        : base(stateManager, stateFactory)
    {
    }

    public override void EnterState()
    {
        stateManager.Animator.SetTrigger("Attack");
        stateManager.IsMovementStopped = true;
        stateManager.AppliedMovementX = 0f;
        stateManager.AppliedMovementZ = 0f;
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {   
        CheckSwitchState();
    }

    private void CheckSwitchState()
    {
        if (!stateManager.IsAttacking)
        {
            stateManager.IsMovementStopped = false;
            stateManager.SwitchState(stateFactory.CreateIdleState());
        }
    }
}