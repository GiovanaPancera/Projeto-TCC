using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sairCasaPipo : MonoBehaviour
{
    public GameObject item, item2, personagem, transicao, load;
    private GameObject personagemAtual;
    private itemColetavel foiPego, foiPego2;
    private Animator anim,anim2;

    void Start()
    {
        anim = transicao.GetComponent<Animator>();
        anim2 = load.GetComponent<Animator>();
    }

    void Update()
    {
        foiPego = item.GetComponent<itemColetavel>(); 
        foiPego2 = item2.GetComponent<itemColetavel>(); 
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            if (foiPego.coletado == true && foiPego2.coletado == true)
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
        personagem.transform.position = new Vector2(101.3421f, -75f);
        personagemAtual.transform.position = new Vector2(101.3421f, -75f);

        yield return new WaitForSeconds(0.5f); // Pequena pausa antes de desativar a animação
        load.SetActive(false);
        anim.SetBool("mudou", false);
        anim2.SetBool("mudou", false);
        transicao.SetActive(false);
    }

}
