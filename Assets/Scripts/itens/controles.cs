using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controles : MonoBehaviour
{
    public tutorial item;
    public tutorial2 item2;
    public ativarDescongelar item3;
    public GameObject canvas;

    void Update()
    {
        if(item != null)
        {
            if (item.passou == true)
            {
                canvas.SetActive(true);            
            }
            else
            {
                canvas.SetActive(false);
            }
        }
        else if(item2 != null)
        {
            if (item2.primeiraVista == 1)
            {
                canvas.SetActive(true);            
            }
            else
            {
                canvas.SetActive(false);
            }
        }
        else if(item3 != null)
        {
            if (item3.passou == true)
            {
                canvas.SetActive(true);            
            }
            else
            {
                canvas.SetActive(false);
            }
        }
    }
}
