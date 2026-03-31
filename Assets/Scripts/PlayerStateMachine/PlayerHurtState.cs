using UnityEngine;

public class PlayerHurtState : PlayerBaseState
{
    public PlayerHurtState(PlayerStateManager stateManager, PlayerStateFactory stateFactory)
        : base(stateManager, stateFactory)
    {
    }

    public override void EnterState()
    {
        stateManager.SendStateDebug("HurtState Entered");
        stateManager.Animator.SetTrigger("Hurt");
        stateManager.IsMovementStopped = true;
        stateManager.AppliedMovementX = 0f;
        stateManager.AppliedMovementZ = 0f;

        stateManager.IsHurt = false;
        stateManager.CanTakeDamage = false;
        stateManager.Hp -= 1;
        if (stateManager.Hp <= 0) 
            KillPlayer();
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public void KillPlayer()
    {
        
    }

    private void CheckSwitchState()
    {
        if (!stateManager.IsAttacking)
        {
            stateManager.CanTakeDamage = true;
            stateManager.IsMovementStopped = false;
            stateManager.SwitchState(stateFactory.CreateIdleState());
        }
    }
}