using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 4f;
    private Vector3 jump;
    private bool grounded;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        jump = new Vector3(0f,3f,0f);
    }

    void Update()
    {
        //fazer o 2 pulo

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            grounded = false;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }
}
