using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pipo : MonoBehaviour
{
    public int primeiraVista;
    private CircleCollider2D colisor;
    public TextoParaTextPro dialogo;
    public GameObject persona1, persona2;
    public bool trocaPersona = false; 

    private string chavePlayerPrefs = "pipo_primeiraVista";

    void Start()
    {
        // Carregar valor salvo, default 0
        primeiraVista = PlayerPrefs.GetInt(chavePlayerPrefs, 0);

        colisor = GetComponent<CircleCollider2D>();
        dialogo = FindFirstObjectByType<TextoParaTextPro>();
    }

    void Update()
    {
        if(primeiraVista == 1 && trocaPersona == true)
        {
            if ( dialogo.i == 3 || dialogo.i == 15 || dialogo.i == 24 || dialogo.i == 36 || dialogo.i == 48)
            {
                persona1.SetActive(true);
                persona2.SetActive(false);
            }
            else
            {
                persona1.SetActive(false);
                persona2.SetActive(true);
            }

            if(dialogo.i == 51)
            {
                trocaPersona = false;
            }
        }        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {  
            if(primeiraVista < 1)
            {
                primeiraVista += 1;

                // Salvar no PlayerPrefs
                PlayerPrefs.SetInt(chavePlayerPrefs, primeiraVista);
                PlayerPrefs.Save();

                dialogo.comecar = true;
                trocaPersona = true; 
                dialogo.nomeDoArquivo = "PipoCasa";
            }           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {  
            if(primeiraVista == 1)
            {
                primeiraVista += 1;

                // Salvar no PlayerPrefs
                PlayerPrefs.SetInt(chavePlayerPrefs, primeiraVista);
                PlayerPrefs.Save();
            }           
        }
    }
}
