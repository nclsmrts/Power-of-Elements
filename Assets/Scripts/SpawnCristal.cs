using UnityEngine;

public class SpawnCristal : MonoBehaviour
{
    public GameObject cristal3vari1;
    public GameObject cristal3vari2;
    public GameObject cristal4vari1;
    public GameObject cristal4vari2;

    private int num;

    void Start()
    {
        num = Random.Range(0,2);

        print(num);

      

        if (num == 0)
        {
            cristal3vari1.SetActive(true);
            cristal4vari2.SetActive(true);
        }
        else
        {
            cristal3vari2.SetActive(true);
            cristal4vari1.SetActive(true);
        }

    }

    void Update()
    {
        
    }
}
