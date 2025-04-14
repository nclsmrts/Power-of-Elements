using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool plane = false;


    [Header("Health")]
    public float currenthealth;
    private int maxhealth = 3;
    public bool isDead = false;
    //public Sprite[] spriteVida;


    [Header("Cristais")]
    public int cristaisColetados;

    [Header("Attack")]
    [SerializeField] private GameObject atack;
    [SerializeField] private Transform camera;

    [Header("PowerUp")]
    public GameObject powerSpeed;
    private bool powerUpSpeed = false;

    private bool powerupInvencible;
    public GameObject powerInvencible;


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

        //Planar
        if (plane && Input.GetKeyDown(KeyCode.C))
        {
            rb.mass = 0.2f;
        }

        //attack

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

        //controlar a velocidade
        SpeedControl();


        if (isDead)
        {
            SceneManager.LoadScene(0);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;

        Cristal cristal = collision.gameObject.GetComponent<Cristal>();

        //detectar cristais
        if (cristal)
        {
            cristaisColetados++;

            FindAnyObjectByType<UIManager>().UpdateCristais();

            Destroy(cristal);
        }



        //powerUp speed
        if (collision.gameObject.CompareTag("speed"))
        {
            powerUpSpeed = true;

            if (powerUpSpeed)
            {
                //pegar metade do valor e somar
                float buffSpeed = moveSpeed / 2 + 1;
                moveSpeed += buffSpeed;
            }

            Destroy(powerSpeed);
        }

        //powerup invencibilidade
        if (collision.gameObject.CompareTag("invencible"))
        {
            powerupInvencible = true;

            if (powerInvencible)
            {
                float buffVida = currenthealth * 2000 + 2;
                currenthealth += buffVida;
            }
            Destroy(powerInvencible);
        }


        ////dano fogo

        //if (collision.gameObject.CompareTag("fire") && !isFire)
        //{
        //    queimando += Time.time;

        //    currenthealth -= 1;

        //    isFire = true;

        //    while (queimando <= fogoTempo)
        //    {
        //        currenthealth -= 0.5f;
        //    }

        //    if (queimando >= fogoTempo)
        //    {
        //        isFire = false;
        //    }
        //}
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
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        Rigidbody rb = Instantiate(atack, spawnPos, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(camera.forward * 30f, ForceMode.Impulse);
        rb.AddForce(transform.up, ForceMode.Impulse);

    }


    public void TakeDamage(int damage)
    {
        currenthealth -= damage;

        FindAnyObjectByType<UIManager>().UpdateUIPlayer();

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


    //void Redirecionar(GameObject pedra)
    //{
    //    Rigidbody rbPedra = pedra.GetComponent<Rigidbody>();


    //    Vector3 direcao = rbPedra.linearVelocity.normalized;
    //    float intensidade = 5f;

    //    rb.linearVelocity = direcao * intensidade;

    //}
}
