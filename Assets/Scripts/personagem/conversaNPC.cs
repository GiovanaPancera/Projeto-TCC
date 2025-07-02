using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class conversaNPC : MonoBehaviour
{
    public GameObject persona1, persona2;   
    private CircleCollider2D colisor;
    public TextoParaTextPro dialogo;
    public string arquivo;
    public bool conversou = false;
    public bool trocaPersona = false; 

    void Start()
    {
        colisor = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {          
            dialogo.nomeDoArquivo = arquivo;
            dialogo.comecar = true;
            conversou = true;

            if (persona1 != null)
            {   
                persona1.SetActive(true);
                trocaPersona = true;
            } 
        }
    }

    void Update()
    {
        if (trocaPersona == true )
        {    
            if (persona2 != null && dialogo.i == 3) 
            {
                persona2.SetActive(true);
                persona1.SetActive(false);
            }
            else if(persona2 != null && dialogo.i > 3)
            {
                persona2.SetActive(false);
                persona1.SetActive(true);
            } 
            else if (dialogo.acabou == true)
            {
                trocaPersona = false;
            }
        }        
    }
}
