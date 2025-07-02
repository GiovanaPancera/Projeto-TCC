using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ativarDescongelar : MonoBehaviour
{ 
    public TextoParaTextPro dialogo;
    public moveAkari akari;
    private descongelar script;
    public string arquivo;
    public bool passou;
    private string chavePassou;

    void Start()
    {
        script = GetComponent<descongelar>();
        chavePassou = "passou_" + gameObject.name; // Cria uma chave única baseada no nome do GameObject
        passou = PlayerPrefs.GetInt(chavePassou, 0) == 1;

        if (passou)
        {
            script.enabled = true;
        }
        else
        {
            script.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            if (akari.primeiraVista == 2 && !passou)
            {
                dialogo.nomeDoArquivo = arquivo;
                dialogo.comecar = true;
                script.enabled = true;
                passou = true;
                PlayerPrefs.SetInt(chavePassou, 1);
            }
        }
    }
}
