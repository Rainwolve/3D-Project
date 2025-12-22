using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]private float moveTimer = 0.0f;
    private static readonly int speedHash = Animator.StringToHash("Speed");
    private Animator animator;
    private NavMeshAgent agent;

    [SerializeField] private Transform[] waypoints;

    private int navIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (waypoints.Length == 0) return;
        agent.destination = waypoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimationSpeed();
        UpdateWaypoints();
        UpdateMovementSpeed();
    }

    private void UpdateMovementSpeed()
    {
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0.0f)
        {
            moveTimer = Random.Range(3f, 10f);
            agent.speed = Random.Range(0.7f, 4f);
        }
    }

    private void UpdateAnimationSpeed()
    {
        animator.SetFloat(speedHash, agent.velocity.magnitude);
    }

    private void UpdateWaypoints()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            navIndex = (navIndex + 1) % waypoints.Length;
            agent.destination = waypoints[navIndex].position;
        }
    }
}