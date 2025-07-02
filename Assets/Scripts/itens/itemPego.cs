using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class itemPego : MonoBehaviour
{
    public GameObject item, canvas;
    private itemColetavel foiPego;
    private Image img;

    void Start()
    {
        foiPego = item.GetComponent<itemColetavel>(); 
        img = canvas.GetComponent<Image>();
    }

    void Update()
    {
        if (foiPego.coletado == true)
        {
            canvas.SetActive(true);
            img.enabled = true;
        }
        else
        {
           img.enabled = false;
        }
    }
}
