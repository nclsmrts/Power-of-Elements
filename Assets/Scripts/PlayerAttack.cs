using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("chao"))
        {
            Destroy(gameObject);
        }
    }
}
