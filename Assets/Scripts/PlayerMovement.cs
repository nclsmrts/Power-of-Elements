using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public Transform orientation;
    public float moveSpeed;
    public float groundDrag;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Jump")]
    private Vector3 jump;
    public float jumpForce = 4f;

    [Header("Health")]
    [SerializeField] private int currenthealth;
    private int maxhealth = 3;
    public bool isDead = false;

    [Header("Cristais")]
    [SerializeField] private int cristaisColetados;

    [Header("Attack")]
    [SerializeField] private GameObject atack;
    [SerializeField] private Transform camera;

    [Header("PowerUp")]
    private bool powerUpSpeed = false;
    public GameObject powerSpeed;

    private float horizontalInput;
    private float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        currenthealth = maxhealth;

        jump = new Vector3(0f, 2f, 0f);
    }

    void Update()
    {

        //input do movimento
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //ground Check
        //Physics.RAycast(<origem do raio>, <Direção do raio>, <comprimento do raio> <Objeto que será verificado>);
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }


        //attack

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

        //controlar a velocidade
        SpeedControl();

    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;

        Cristal cristal = collision.gameObject.GetComponent<Cristal>();

        //detectar cristais
        if (cristal)
        {
            cristaisColetados++;

            Destroy(cristal);
        }



        //powerUp speed
        if (collision.gameObject.CompareTag("speed"))
        {
            powerUpSpeed = true;

            if (powerUpSpeed)
            {
                //pegar metade do valor e somar
                moveSpeed += 5;
            }

            Destroy(powerSpeed);
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    void MovePlayer()
    {
        if (!isDead)
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
    }

    void Attack()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

        Rigidbody rb = Instantiate(atack, spawnPos, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(camera.forward * 30f, ForceMode.Impulse);
        rb.AddForce(transform.up, ForceMode.Impulse);

    }


    public void TakeDamage(int damage)
    {
        currenthealth -= damage;

        if (currenthealth <= 0)
        {
            isDead = true;
        }

    }

    private void SpeedControl()
    {
        //pega a velocidade atual
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //limita a velocidade caso passe da moveSpeed do player
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;

            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }

    }
}
