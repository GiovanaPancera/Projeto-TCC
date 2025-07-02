using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desbloqueio3 : MonoBehaviour
{
    public movePipo conversou;

    void Update()
    {
        if (conversou.primeiraVista == 2)
        {
            gameObject.SetActive(false);
        }
    }
}
