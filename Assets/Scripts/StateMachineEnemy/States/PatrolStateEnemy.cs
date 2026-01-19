using UnityEngine;

public class PatrolStateEnemy : EnemyBaseState
{
    private float moveTimer = 0.0f;
    private float breakTimer;
    private float distanceToPlayer;

    public PatrolStateEnemy(EnemyStateManager enemyStateManager, EnemyStateFactory stateFactory) : base(
        enemyStateManager, stateFactory)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Patrol state");
        breakTimer = Random.Range(20f, 30f);
        stateManager.NavMeshAgent.isStopped = false;
        stateManager.NavMeshAgent.SetDestination(stateManager.PatrolDestinations[stateManager.DestinationIndex]);
    }

    public override void UpdateState()
    {
        breakTimer -= Time.deltaTime;
        moveTimer -= Time.deltaTime;

        UpdateMovementSpeed();
        UpdateAnimationSpeed();
        UpdateDistanceToPlayer();
        CheckSwitchState();
        UpdateWaypoints();
    }

    private void UpdateMovementSpeed()
    {
        if (moveTimer <= 0.0f)
        {
            moveTimer = Random.Range(3f, 10f);
            stateManager.NavMeshAgent.speed = Random.Range(0.7f, 4f);
        }
    }

    private void UpdateDistanceToPlayer()
    {
        distanceToPlayer = Vector3.Distance(stateManager.EnemyTransform.position,stateManager.PlayerTransform.position);
    }

    private void UpdateAnimationSpeed()
    {
        stateManager.Animator.SetFloat(stateManager.SpeedAnimationHash,
            stateManager.NavMeshAgent.velocity.magnitude / stateManager.NavMeshAgent.speed);
    }

    private void UpdateWaypoints()
    {
        if (stateManager.NavMeshAgent.remainingDistance <= stateManager.NavMeshAgent.stoppingDistance)
        {
            stateManager.DestinationIndex = (stateManager.DestinationIndex + 1) % stateManager.PatrolDestinations.Count;
            stateManager.NavMeshAgent.SetDestination(stateManager.PatrolDestinations[stateManager.DestinationIndex]);
        }
    }

    private void CheckSwitchState()
    {
        if (breakTimer <= 0.0f)
        {
            stateManager.SwitchState(stateFactory.IdleState());
        }
        else if (distanceToPlayer < stateManager.HuntingDistance)
        {
            stateManager.SwitchState(stateFactory.HuntState());
        }
    }
}