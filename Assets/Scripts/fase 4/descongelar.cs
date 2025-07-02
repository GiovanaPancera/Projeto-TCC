using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class descongelar : MonoBehaviour
{
    private Animator anim;
    public GameObject borboleta;
    public moveAkari akari; 
    private bool descongelada;
    private string chavePassou;

    void Start()
    {
        anim = GetComponent<Animator>();
        chavePassou = "descongelou_" + borboleta.name; // Cria uma chave única baseada no nome do GameObject
        descongelada = PlayerPrefs.GetInt(chavePassou, 0) == 1;
        
        if (descongelada)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && akari.primeiraVista == 2 && gameObject.activeSelf == true)
            {
                anim.SetBool("descongelou", true);
                descongelada = true;
                PlayerPrefs.SetInt(chavePassou, 1);
                StartCoroutine(Desativar());
            }
        }
    }

    IEnumerator Desativar()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        borboleta.SetActive(true);
    }

}
