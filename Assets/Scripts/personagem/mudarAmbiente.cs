using System.Collections;
using UnityEngine;

public class mudarAmbiente : MonoBehaviour
{
    public GameObject transicao, personagem, load;
    public Vector3 local;
    private GameObject personagemAtual;
    private Animator anim, anim2;

    void Start()
    {
        anim = transicao.GetComponent<Animator>();
        anim2 = load.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            personagemAtual = GameObject.FindWithTag("Player");
            transicao.SetActive(true);
            StartCoroutine(TransicaoCena());
        }
    }

    IEnumerator TransicaoCena()
    {
        anim.SetBool("mudou", true); // Ativa a animação de transição
        yield return new WaitForSeconds(1f);
        load.SetActive(true);
        anim2.SetBool("mudou", true);
        yield return new WaitForSeconds(1f); // Espera a animação acontecer

        // Move os personagens para a nova posição
        personagem.transform.position = local;
        personagemAtual.transform.position = local; //new Vector2(116.076f, -75.847f);

        yield return new WaitForSeconds(0.5f); // Pequena pausa antes de desativar a animação
        load.SetActive(false);
        anim.SetBool("mudou", false);
        anim2.SetBool("mudou", false);
        transicao.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }
}
