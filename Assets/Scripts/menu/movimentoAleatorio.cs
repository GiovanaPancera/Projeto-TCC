using UnityEngine;

public class movimentoAleatorio : MonoBehaviour
{
    public Transform alvo; // O objeto a ser seguido (ex: flor)
    public float velocidade = 2f;
    public float distanciaParaCircular = 1.5f;
    public float velocidadeOrbital = 50f; // velocidade de rotação ao redor do alvo
    public float raioOrbita = 1.2f; // raio da órbita ao redor do objeto
    public float variacaoAltura = 0.2f; // para subir/descer levemente durante a órbita

    private bool chegouNoAlvo = false;
    private float anguloAtual;

    void Update()
    {
        if (alvo == null) return;

        Vector2 direcao = (alvo.position - transform.position);
        float distancia = direcao.magnitude;

        if (!chegouNoAlvo)
        {
            // Vai em direção ao alvo
            transform.position += (Vector3)direcao.normalized * velocidade * Time.deltaTime;

            // Chegou perto o suficiente?
            if (distancia <= distanciaParaCircular)
            {
                chegouNoAlvo = true;
                anguloAtual = Random.Range(0f, 360f); // ângulo inicial aleatório
            }
        }
        else
        {
            // Circula ao redor do alvo
            anguloAtual += velocidadeOrbital * Time.deltaTime;
            float rad = anguloAtual * Mathf.Deg2Rad;

            // Cria um movimento em volta com leve variação vertical
            Vector3 pos = alvo.position;
            pos.x += Mathf.Cos(rad) * raioOrbita;
            pos.y += Mathf.Sin(rad) * (raioOrbita + Mathf.Sin(Time.time * 2f) * variacaoAltura);

            transform.position = pos;
        }
    }
}
