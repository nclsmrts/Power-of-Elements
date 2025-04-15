using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaInicial : MonoBehaviour
{
    public float rotacao;
    private Vector3 sla;

    void Start()
    {
        
    }

    void Update()
    {
        //rodar o cenário
        sla = new Vector3(0,rotacao,0);

        //transform.position += sla;
       
        transform.Rotate(sla);
    }

    public void Jogar()
    {
        SceneManager.LoadScene(0);
    }
}
