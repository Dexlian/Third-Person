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
    public ZombieHealth zombieHealth;
    public float zombieDamageMultiplierHead = 1f;
    public float zombieDamageMultiplierTorso = 1f;
    public float zombieDamageMultiplierArm = 1f;
    public float zombieDamageMultiplierLeg = 1f;
    public float decayTimer = 5f;
    public bool isDead = false;

    [Header("Stagger")]
    public float zombieStaggerMultiplierHead = 1f;
    public float zombieStaggerMultiplierTorso = 1f;
    public float zombieStaggerMultiplierArm = 1f;
    public float zombieStaggerMultiplierLeg = 1f;

    [Header("Item Drops")]
    public float ammoDropChance = 0.25f;
    public GameObject ammo;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        zombieNavMeshAgent = GetComponentInChildren<NavMeshAgent>();
        zombieRigidbody = GetComponent<Rigidbody>();
        zombieHealth = GetComponent<ZombieHealth>();
    }

    private void Start()
    {
        currentState = startingState;
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

        if (isDead)
        {
            decayTimer -= Time.deltaTime;

            if (decayTimer <= 0f)
            {
                ZombieCleanup();
            }
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

    private void ZombieCleanup()
    {
        float ammoDrop;
        ammoDrop = Random.Range(0f, 1f);

        if (ammoDrop <= ammoDropChance)
        {
            Instantiate(ammo, transform.position, transform.rotation);

        }

        Destroy(gameObject);
    }
}
