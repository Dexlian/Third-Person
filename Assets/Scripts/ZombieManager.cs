using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    //The state this character begins in
    public ZombieIdleState startingState;

    [Header("Flags")]
    public bool isPerformingAction;

    //The state this character is currently in
    [Header("Current State")]
    [SerializeField] private ZombieState currentState;

    [Header("Current Target")]
    public PlayerManager currentTarget;
    public float distanceFromCurrentTarget;

    [Header("Animator")]
    public Animator animator;

    [Header("Nav Mesh Agent")]
    public NavMeshAgent zombieNavMeshAgent;

    [Header("Rigidbody")]
    public Rigidbody zombieRigidbody;

    [Header("Locomotion")]
    public float rotationSpeed = 5f;
    public float zombieSpeed = 0.5f;
    public float zombieMaxSpeed = 2f;

    [Header("Attack")]
    public int damageHit = 10;
    public float minimumAttackDistance = 1f;
    public float attackRangeBuffer = 0.3f;
    public float attackCooldown = 1.5f;
    public float attackTimer = 0f;
    public bool canAttack;

    [Header("Health")]
    public bool isDead = false;

    private void Awake()
    {
        currentState = startingState;
        animator = GetComponent<Animator>();
        zombieNavMeshAgent = GetComponentInChildren<NavMeshAgent>();
        zombieRigidbody = GetComponent<Rigidbody>();
        canAttack = true;
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        //Debug.Log(attackTimer);

        zombieNavMeshAgent.transform.localPosition = Vector3.zero;

        if (currentTarget != null)
        {
            distanceFromCurrentTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        }
    }

    private void HandleStateMachine()
    {
        ZombieState nextState;

        if (currentState != null)
        {
            nextState = currentState.Tick(this);

            if (nextState != null)
            {
                currentState = nextState;
            }
        }
    }
}
