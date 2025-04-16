using UnityEngine;

public class enemyCristal : MonoBehaviour
{
    public GameObject cristal5;


    void Start()
    {
        
    }

    void Update()
    {
        EnemyController enemy = gameObject.GetComponent<EnemyController>();

        if (enemy.currentHealth <= 0)
        {
            Instantiate(cristal5, transform.position, Quaternion.identity);
        }

        //PlayerMovement player = gameObject.GetComponent<PlayerMovement>();

        //if (player.cristaisColetados == 4)
        //{
        //    gameObject.SetActive(true);
        //}

    }
}
