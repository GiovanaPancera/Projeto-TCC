using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dicas : MonoBehaviour
{
    public GameObject dica1, dica2, dica3, dica4, catnip;
    public pipo conversou;
    public conversaNPC jade;
    public vinha passou;

    // Update is called once per frame
    void Update()
    {
        if(conversou.primeiraVista >= 1)
        {
            dica1.SetActive(true);
        }

        if(jade.conversou == true)
        {
            dica2.SetActive(true);
        }

        if(passou.desbloqueada == true)
        {
            dica3.SetActive(true);
        }

        if(catnip.activeSelf == false)
        {
            dica4.SetActive(true);
        }
    }
}
