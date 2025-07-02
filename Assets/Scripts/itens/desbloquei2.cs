using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desbloquei2 : MonoBehaviour
{
    public moveAkari conversou;

    void Update()
    {
        if (conversou.primeiraVista == 1)
        {
            gameObject.SetActive(false);
        }
    }
}
