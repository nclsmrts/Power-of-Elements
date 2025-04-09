using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 4f;
    private Vector3 jump;
    private bool grounded;

    [Header("Pulo")]
    [SerializeField]private bool pulo = false;
    public GameObject powerUp;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        jump = new Vector3(0f, 3f, 0f);
    }

    void Update()
    {
        //fazer o 2 pulo
        if (pulo)
        {
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                grounded = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;

        if (collision.gameObject.CompareTag("Pulo"))
        {
            pulo = true;

            Destroy(powerUp);
        }

    }
}
