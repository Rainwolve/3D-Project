using UnityEngine;

public class EnemyStateFactory
{
    private EnemyStateManager stateManager;

    public EnemyStateFactory(EnemyStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public EnemyBaseState IdleState()
    {
        return new IdleStateEnemy(stateManager, this);
    }

    public EnemyBaseState PatrolState()
    {
        return new PatrolStateEnemy(stateManager, this);
    }

    public EnemyBaseState AttackCDState()
    {
        return new AttackCDStateEnemy(stateManager, this);
    }
    public EnemyBaseState HuntState()
    {
        return new HuntStateEnemy(stateManager, this);
    }
    public EnemyBaseState HurtState()
    {
        return new HurtStateEnemy(stateManager, this);
    }
    public EnemyBaseState AttackState()
    {
        return new AttackStateEnemy(stateManager, this);
    }
}