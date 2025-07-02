using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raioLampiao : MonoBehaviour
{
    public bool raioDentro = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Borboleta"))
        {  
            raioDentro = true;
        }
    }
}
