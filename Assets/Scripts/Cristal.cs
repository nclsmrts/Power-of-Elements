using UnityEngine;

public class Cristal : MonoBehaviour
{
    

    void Start()
    {
        
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
