using UnityEngine;

public class IdleStateEnemy :EnemyBaseState
{
  private float timer;
  
  public IdleStateEnemy(EnemyStateManager enemyStateManager, EnemyStateFactory stateFactory) : base(
    enemyStateManager, stateFactory)
  {
  }

  public override void EnterState()
  {
    Debug.Log("idle state");
    stateManager.NavMeshAgent.isStopped = true;
    stateManager.Animator.SetFloat(stateManager.SpeedAnimationHash, 0.0f);
    timer = 5.0f;
  }

  public override void UpdateState()
  {
    timer -= Time.deltaTime;
    if (timer <= 0.0f)
    {
      stateManager.SwitchState(stateFactory.PatrolState());
    }
  }
}
