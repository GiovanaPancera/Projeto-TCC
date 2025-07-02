using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desbloqueio : MonoBehaviour
{
    public itemColetavel[] foiPego;

    void Update()
    {
        if (itensColetados())
        {
            gameObject.SetActive(false);
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
