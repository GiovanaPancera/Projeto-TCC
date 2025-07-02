using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventario : MonoBehaviour
{
    public GameObject mochila, fechamento;
    public AudioSource som;

    public void abrir()
    {
        som.Play(); 
        mochila.SetActive(true);
        fechamento.SetActive(true);
    }

    public void fechar()
    {
        som.Play(); 
        mochila.SetActive(false);        
        fechamento.SetActive(false);
    }
}
