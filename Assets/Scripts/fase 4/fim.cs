using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fim : MonoBehaviour
{
    public itemColetavel[] foiPego;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (itensColetados())
        {
            SceneManager.LoadScene("Fim");
        }
        
    }

    private bool itensColetados()
    {
        for (int i = 0; i < foiPego.Length; i++) 
        {
            if (!foiPego[i].coletado) 
            {
                return false;
            }   
        }
        return true;
    }
}
