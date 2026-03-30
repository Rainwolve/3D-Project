using UnityEngine;


public class HuntStateEnemy : EnemyBaseState
{
    public HuntStateEnemy(EnemyStateManager enemyStateManager, EnemyStateFactory stateFactory) : base(
        enemyStateManager, stateFactory)
    {
    }

    public override void EnterState()
    {
        stateManager.NavMeshAgent.isStopped = false;
        stateManager.NavMeshAgent.speed = 4;
        stateManager.Animator.SetFloat(stateManager.SpeedAnimationHash,
            stateManager.NavMeshAgent.velocity.magnitude);
    }

    public override void UpdateState()
    {
        UpdateDestination();
        CheckSwitchState();
    }

    private void UpdateDestination()
    {
        stateManager.NavMeshAgent.SetDestination(stateManager.PlayerTransform.position);
    }

    private void CheckSwitchState()
    {
        if (stateManager.IsHurt)
        {
            stateManager.SwitchState(stateFactory.HurtState());
        }
        else if (stateManager.NavMeshAgent.remainingDistance <= stateManager.AttackDistance &&
                 Vector3.Distance(stateManager.PlayerTransform.position, stateManager.EnemyTransform.position) <=
                 stateManager.AttackDistance)
        {
            stateManager.SwitchState(stateFactory.AttackState());
        }
        else if (stateManager.NavMeshAgent.remainingDistance >= stateManager.HuntingDistance &&
                 Vector3.Distance(stateManager.PlayerTransform.position, stateManager.EnemyTransform.position) >=
                 stateManager.HuntingDistance)
        {
            stateManager.SwitchState(stateFactory.PatrolState());
        }
    }
}