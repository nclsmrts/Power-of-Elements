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

        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (gameObject.CompareTag("player") && enemy)
        {

            enemy.TakeDamage(damage);

        }

        if (gameObject.CompareTag("enemy") && player)
        {
            player.TakeDamage(damage);


        }

    }
}
