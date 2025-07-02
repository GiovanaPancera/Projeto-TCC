using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicaFase : MonoBehaviour
{
    public AudioSource som, som2;
    public GameObject fase;
    private bool tocando = false;

    void Start()
    {
        // Carrega o valor salvo em PlayerPrefs (1 = true, 0 = false)
        tocando = PlayerPrefs.GetInt("tocandoMusica", 0) == 1;

        if (tocando)
        {
            som.Play();
            som2.Pause();
        }
        else
        {
            som2.Play();
            som.Pause();
        }
    }

    void Update()
    {
        if (fase.tag == "TopDown")
        {
            if (!tocando)
            {
                som.Play();
                som2.Pause();
                tocando = true;
                PlayerPrefs.SetInt("tocandoMusica", 1); // Salva como true
            }
        }
        else
        {
            if (tocando)
            {
                som2.Play();
                som.Pause();
                tocando = false;
                PlayerPrefs.SetInt("tocandoMusica", 0); // Salva como false
            }
        }
    }
}
