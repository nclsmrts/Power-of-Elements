using UnityEngine;

public class Cristal : MonoBehaviour
{
    //PlayerMovement player;

    void Start()
    {
        //player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player)
        {
            Destroy(gameObject);
        }
    }
}
