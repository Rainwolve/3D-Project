using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    private EnemyStateFactory stateFactory;
    private EnemyBaseState  currentState;
    private bool isTransitioning;
    private void Awake()
    {
        stateFactory = new EnemyStateFactory(this);
    }

    void Update()
    {
        currentState?.UpdateState();
    }

    public void SwitchState(EnemyBaseState newState)
    {
        if(isTransitioning)return;
        
        isTransitioning = true;
        currentState = newState;
        currentState.EnterState();
        isTransitioning = false;
    }
}
