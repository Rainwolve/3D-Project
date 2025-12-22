using UnityEngine;

public abstract class EnemyBaseState{
    protected EnemyStateManager stateManager;
    protected EnemyStateFactory  stateFactory;

    public EnemyBaseState(EnemyStateManager enemyStateManager, EnemyStateFactory enemyStateFactory)
    {
        stateManager = enemyStateManager;
        stateFactory = enemyStateFactory;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public abstract void EnterState();
    public abstract void UpdateState();

    // Update is called once per frame
    void Update()
    {
        
    }
}
