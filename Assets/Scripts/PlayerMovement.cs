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


    private float horizontalInput;
    private float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

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


        //controlar a velocidade
        SpeedControl();


        ////Jump
        //if (Input.GetKeyDown(KeyCode.Space) && grounded)
        //{
        //    rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        //    grounded = false;
        //}


    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
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
