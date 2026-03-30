using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
  public PlayerAttackState(PlayerStateManager stateManager, PlayerStateFactory stateFactory) 
    : base(stateManager, stateFactory)
  {
    
        
  }
  public override void EnterState()
  {
    stateManager.Animator.SetTrigger("Attack");
    
    
  }

  public override void ExitState()
  {
    
  }

  public override void UpdateState()
  {
    if (stateManager.IsAttackFinished)
    {
      
    }
  }
  
}
