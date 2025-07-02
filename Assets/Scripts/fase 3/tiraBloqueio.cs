using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiraBloqueio : MonoBehaviour
{
    public tutorial2 passou;

    void Update()
    {
        if(passou.primeiraVista == 1)
        {
            gameObject.SetActive(false);
        }
    }
}
