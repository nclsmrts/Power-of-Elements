using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Sprites")]
    public GameObject[] vidaPlayer;

    private PlayerMovement player;
    

    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();


    }

    public void UpdateUIPlayer()
    {
        //PlayerMovement player = GetComponent<PlayerMovement>();


        if (player.currenthealth == 2)
        {
            vidaPlayer[0].SetActive(false);
        }
        else if (player.currenthealth == 1)
        {
            vidaPlayer[1].SetActive(false);
        }
        else if (player.currenthealth <= 1);
        {
            vidaPlayer[2].SetActive(false);
        }

    }
}
