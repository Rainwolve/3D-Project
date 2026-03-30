using UnityEngine;

public class AttackStateEnemy : EnemyBaseState
{
    public AttackStateEnemy(EnemyStateManager enemyStateManager, EnemyStateFactory stateFactory) : base(
        enemyStateManager, stateFactory)
    {
    }

    public override void EnterState()
    {
        stateManager.NavMeshAgent.isStopped = true;
        stateManager.Animator.SetBool(stateManager.CoolDownAnimationHash, true);
        stateManager.Animator.SetTrigger(stateManager.AttackAnimationHash);
        stateManager.IsAnimationFinished = false;
    }

    public override void UpdateState()
    {
        CheckToDealDamage();
        CheckSwitchState();
    }

    private void CheckToDealDamage()
    {
        if (stateManager.CheckToDealDamage)
        {
            if (stateManager.AttackArea.IsPlayerInAttackArea)
            {
                GameEvents.OnPlayerHurt?.Invoke(stateManager.AttackDamage);
            }
        }
    }

    private void CheckSwitchState()
    {
        if (stateManager.IsAnimationFinished)
        {
            stateManager.IsAnimationFinished = false;
            stateManager.SwitchState(stateFactory.AttackCDState());
        }
    }
}