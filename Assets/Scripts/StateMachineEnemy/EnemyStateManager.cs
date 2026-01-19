using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStateManager : MonoBehaviour
{
    //state Manager
    private EnemyStateFactory stateFactory;
    private EnemyBaseState currentState;
    private bool isTransitioning;
    public bool IsHurt;

    #region Context and Access

    // context
    [SerializeField] private Transform enemyPatrolDestinationParent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float huntingDistance;
    [SerializeField] private float attackDistance;

    private Animator animator;
    private int speedAnimationHash = Animator.StringToHash("Speed");

    private NavMeshAgent navMeshAgent;
    private List<Vector3> patrolDestinations;
    private int destinationIndex = 0;

    //access
    public Animator Animator
    {
        get { return animator; }
    }

    public int SpeedAnimationHash
    {
        get { return speedAnimationHash; }
    }

    public NavMeshAgent NavMeshAgent
    {
        get { return navMeshAgent; }
    }

    public Transform EnemyTransform
    {
        get { return transform; }
    }

    public List<Vector3> PatrolDestinations
    {
        get { return patrolDestinations; }
    }

    public int DestinationIndex
    {
        get { return destinationIndex; }
        set { destinationIndex = value; }
    }

    public Transform PlayerTransform
    {
        get { return playerTransform; }
    }

    public float HuntingDistance
    {
        get { return huntingDistance; }
    }

    public float AttackDistance
    {
        get { return attackDistance; }
    }

    #endregion

    private void Awake()
    {
        stateFactory = new EnemyStateFactory(this);
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        ExtractDestinations();
    }

    private void Start()
    {
        SwitchState(stateFactory.PatrolState());
    }

    void Update()
    {
        currentState?.UpdateState();
    }

    public void SwitchState(EnemyBaseState newState)
    {
        if (isTransitioning) return;

        isTransitioning = true;
        currentState = newState;
        currentState.EnterState();
        isTransitioning = false;
    }

    public void ExtractDestinations()
    {
        patrolDestinations = new List<Vector3>();
        foreach (Transform child in enemyPatrolDestinationParent)
        {
            patrolDestinations.Add(child.position);
        }
    }
}