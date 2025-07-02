using UnityEngine;

public class pegada : MonoBehaviour
{
    public GameObject img, fase;
    public float intervalo = 0.5f;
    public float tempoDeVida = 120f;
    public movePipo labirinto;

    private Vector3 ultimaPosicao;

    void Start()
    {
        ultimaPosicao = transform.position;
    }

    void Update()
    {
       if(fase.tag == "TopDown" &&  labirinto.primeiraVista == 2)
       {
            if (Vector3.Distance(transform.position, ultimaPosicao) > intervalo)
            {
                GameObject rastro = Instantiate(img, transform.position, Quaternion.identity);
                Destroy(rastro, tempoDeVida); // aqui ele será destruído após o tempo definido
                ultimaPosicao = transform.position;
            }
       }
    }
}
