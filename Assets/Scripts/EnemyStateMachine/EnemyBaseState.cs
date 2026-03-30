using UnityEngine;
using UnityEngine.AI;


public abstract class EnemyBaseState{
    
    protected EnemyStateManager stateManager;
    protected EnemyStateFactory  stateFactory;

    public EnemyBaseState(EnemyStateManager enemyStateManager, EnemyStateFactory enemyStateFactory)
    {
        stateManager = enemyStateManager;
        stateFactory = enemyStateFactory;
    }
    public abstract void EnterState();
    public abstract void UpdateState();

    
}
