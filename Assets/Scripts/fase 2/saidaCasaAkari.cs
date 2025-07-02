using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saidaCasaAkari : MonoBehaviour
{
    public GameObject personagem, transicao, load;
    private GameObject personagemAtual;
    public moveAkari akari;
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
            if (akari.primeiraVista == 1)
            {
                personagemAtual = GameObject.FindWithTag("Player");
                transicao.SetActive(true);
                StartCoroutine(TransicaoCena());
            }
            
            if (akari.primeiraVista >= 3)
            {
                personagemAtual = GameObject.FindWithTag("Player");
                transicao.SetActive(true);
                StartCoroutine(TransicaoCena());
            }
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
        personagem.transform.position = new Vector2(29.66f, -69.29f);
        personagemAtual.transform.position = new Vector2(29.66f, -69.29f);

        yield return new WaitForSeconds(0.5f); // Pequena pausa antes de desativar a animação
        load.SetActive(false);
        anim.SetBool("mudou", false);
        anim2.SetBool("mudou", false);
        transicao.SetActive(false);
        yield return new WaitForSeconds(1f);
    }

}
