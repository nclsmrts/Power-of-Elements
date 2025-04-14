using System;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField]
    private bool isFire;
    [SerializeField]
    private float queimando;
    private float fogoTempo = 1.5f;
    private bool fogos;

    void Start()
    {

    }

    void Update()
    {
        print(queimando);

        PlayerMovement player = gameObject.GetComponent<PlayerMovement>();

        //if (fogos)
        //{
        //    queimando += Time.deltaTime;

        //    player.currenthealth -= 1;



        //    //while (queimando <= fogoTempo)
        //    //{
        //    //    player.currenthealth -= 0.5f;
        //    //}

        //    if (queimando >= fogoTempo)
        //    {
        //        isFire = false;
        //    }
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        //dano fogo

        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player && !isFire)
        {
            isFire = true;

            player.currenthealth -= 1;
        }

        if (!player && isFire)
        {
            fogos = true;
            queimando = Time.deltaTime;
        }
    }
  
}
