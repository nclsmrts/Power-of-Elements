using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Sprites")]
    public GameObject[] vidaPlayer;

    private PlayerMovement player;

    public Image[] cristaisPlayer;
    public Sprite cristaisPlayerSprites;

    [Header("Timer")]
    private float timer;
    private float minutos;
    private float segundos;
    [SerializeField] private TextMeshProUGUI textoTimer;

    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();

    }

    private void Update()
    {
        timer += Time.deltaTime;
        minutos = Mathf.FloorToInt(timer / 60);
        segundos = Mathf.FloorToInt(timer - minutos * 60);

        textoTimer.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void UpdateUIPlayer()
    {
        //PlayerMovement player = GetComponent<PlayerMovement>();


        if (player.currenthealth >= 2)
        {
            vidaPlayer[0].SetActive(false);
        }
        else if (player.currenthealth == 1)
        {
            vidaPlayer[1].SetActive(false);
        }
        else
        {
            vidaPlayer[2].SetActive(false);
        }

    }

    public void UpdateCristais()
    {
        if (player.cristaisColetados == 1)
        {
            cristaisPlayer[0].sprite = cristaisPlayerSprites;
        }
        else if (player.cristaisColetados == 2)
        {
            cristaisPlayer[1].sprite = cristaisPlayerSprites;
        }
        else if (player.cristaisColetados == 3)
        {
            cristaisPlayer[2].sprite = cristaisPlayerSprites;

        }
        else if (player.cristaisColetados == 4)
        {
            cristaisPlayer[3].sprite = cristaisPlayerSprites;

        }
        else if (player.cristaisColetados == 5)
        {
            cristaisPlayer[4].sprite = cristaisPlayerSprites;

        }

    }
}
