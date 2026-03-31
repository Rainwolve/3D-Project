using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateManager stateManager, PlayerStateFactory stateFactory)
        : base(stateManager, stateFactory)
    {
    }

    public override void EnterState()
    {
        stateManager.SendStateDebug("AttackState Entered");
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
        DealDamage();
        CheckSwitchState();
    }

    private void CheckSwitchState()
    {
        if (stateManager.IsHurt)
        {
            stateManager.IsMovementStopped = false;
            stateManager.SwitchState(stateFactory.CreateHurtState());
        }
        if (!stateManager.IsAttacking)
        {
            stateManager.IsMovementStopped = false;
            stateManager.SwitchState(stateFactory.CreateIdleState());
        }
    }
    private void DealDamage()
    {
        foreach (IHurtable enemy in stateManager.AttackManager.Hurtables)
        {
            enemy.TakeDamage(1);
        } 
    }
}