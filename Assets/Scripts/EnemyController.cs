using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerScript;
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent agent;

    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Health")]
    private int maxHealth = 4;
    public int currentHealth;

    [Header("Patroling")]
    public Vector3 walkpoint;
    private bool walkPointSet;
    public float walkPointRange;

    [Header("Attack")]
    [SerializeField] private GameObject attack;
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    //public GameObject cristal5;

    private Rigidbody rb;

    private void Awake()
    {
        //rb = GetComponent<Rigidbody>();

        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth;
    }

    void Start()
    {
        if (gameObject.CompareTag("enemyCristal"))
        {
            currentHealth += 5;
        }

    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();


        //inimigos voltarem dps de pegar 4 cristais
        //PlayerMovement player = GetComponent<PlayerMovement>();

        //if (playerScript.cristaisColetados == 4)
        //{
        //    gameObject.SetActive(true);
        //}

    }


    void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkpoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkpoint;

        if (distanceToWalkPoint.magnitude < 3f)
        {
            walkPointSet = false;
        }

    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    void AttackPlayer()
    {
        transform.LookAt(player);


        if (!alreadyAttacked)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

            Rigidbody rb = Instantiate(attack, spawnPos, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 100f, ForceMode.Impulse);
            rb.AddForce(transform.up, ForceMode.Impulse);

            alreadyAttacked = true;

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {

            gameObject.SetActive(false);
            //Destroy(gameObject);
        }

    }
}
