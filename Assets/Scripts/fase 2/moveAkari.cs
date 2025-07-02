using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class moveAkari : MonoBehaviour
{
    private Animator anim;
    private CircleCollider2D colisor;
    public TextoParaTextPro dialogo;
    public itemColetavel foiPego1, foiPego2;
    public GameObject persona1, persona2;
    public int primeiraVista;   
    public bool pare, trocaPersona;

    private string chavePrimeiraVista;

    void Start()
    {
        anim = GetComponent<Animator>();
        colisor = GetComponent<CircleCollider2D>();
        dialogo = FindFirstObjectByType<TextoParaTextPro>();
        pare = false;
        trocaPersona = false;

        // Define a chave para PlayerPrefs com base no nome do objeto
        chavePrimeiraVista = "moveAkari_primeiraVista_" + gameObject.name;

        // Carrega o valor salvo, padrão 0 se não existir
        primeiraVista = PlayerPrefs.GetInt(chavePrimeiraVista, 0);
    }

    void Update()
    {
        if(trocaPersona == true && primeiraVista == 1)
        {
            if (dialogo.i == 3 || dialogo.i == 15 || dialogo.i == 24 || dialogo.i == 36 || dialogo.i == 42)
            {
                persona1.SetActive(true);
                persona2.SetActive(false);
            }
            else if(dialogo.i == 0 || dialogo.i == 6 || dialogo.i == 18 || dialogo.i == 27 || dialogo.i == 39 || dialogo.i == 45)
            {
                persona1.SetActive(false);
                persona2.SetActive(true);
            }

            if (dialogo.i == 45) 
            {
                trocaPersona = false;
            }
        } 
        else if(trocaPersona == true && primeiraVista == 2)
        {
            if (dialogo.i == 3 || dialogo.i == 12)
            {
                persona1.SetActive(true);
                persona2.SetActive(false);
            }
            else if(dialogo.i == 0 || dialogo.i == 6 || dialogo.i == 15)
            {
                persona1.SetActive(false);
                persona2.SetActive(true);
            }

            if (dialogo.i == 18) 
            {
                trocaPersona = false;
            }
        } 
        else if(trocaPersona == true && primeiraVista == 3)
        {
            if (dialogo.i == 3 || dialogo.i == 9 || dialogo.i == 18)
            {
                persona1.SetActive(true);
                persona2.SetActive(false);
            }
            else if(dialogo.i == 0 || dialogo.i == 6 || dialogo.i == 12)
            {
                persona1.SetActive(false);
                persona2.SetActive(true);
            }

            if (dialogo.i == 18) 
            {
                trocaPersona = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(primeiraVista == 0)
            {  
                parado();
                dialogo.comecar = true;
                trocaPersona = true;
                dialogo.nomeDoArquivo = "Akira1";
                primeiraVista += 1;
                PlayerPrefs.SetInt(chavePrimeiraVista, primeiraVista);
            } 
            else if(primeiraVista == 1 && foiPego1.coletado == true)
            {  
                parado();
                dialogo.comecar = true;
                trocaPersona = true;
                dialogo.nomeDoArquivo = "Akira2";
                primeiraVista += 1;
                PlayerPrefs.SetInt(chavePrimeiraVista, primeiraVista);
            } 
            else if(foiPego2.coletado == true)
            {  
                parado();
                dialogo.comecar = true;
                trocaPersona = true;
                dialogo.nomeDoArquivo = "Akira3";
                primeiraVista += 1;
                PlayerPrefs.SetInt(chavePrimeiraVista, primeiraVista);
            } 
            else 
            {
                pare = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pare = true;
            parado();   
        }
    }

    private void parado()
    {
        anim.Play("deboas");
    }
}
