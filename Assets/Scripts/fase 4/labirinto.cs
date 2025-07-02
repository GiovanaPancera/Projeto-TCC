using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class labirinto : MonoBehaviour
{
    
    public GameObject dica;
    public AudioSource som;
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            dica.SetActive(true);
        }
    }

    public void fechar()
    { 
        som.Play();
        dica.SetActive(false);  
    }
}
