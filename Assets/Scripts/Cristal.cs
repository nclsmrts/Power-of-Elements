using UnityEngine;

public class Cristal : MonoBehaviour
{
    [SerializeField]private int vida = 2;

    void Start()
    {
        
    }

    void Update()
    {
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player)
        {
            Destroy(gameObject);
        }

        PlayerAttack playerAttack = collision.gameObject.GetComponent<PlayerAttack>();


        if (playerAttack)
        {
            vida--;
        }
    }
}
