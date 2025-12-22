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
      return new IdleStateEnemy(stateManager,this);
   }
   // implement other States
}
