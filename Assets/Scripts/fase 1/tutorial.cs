using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public GameObject borboleta, persona;   
    private Animator anim;
    private CircleCollider2D colisor;
    public TextoParaTextPro dialogo;
    public string arquivo;
    public bool trocaPersona = false;
    public bool passou = false;
    public bool triggerOn;
    private string chavePlayerPrefs;
    private string chaveTrigger;

    void Start()
    {
        anim = borboleta.GetComponent<Animator>();
        colisor = GetComponent<CircleCollider2D>();

        // Chaves para salvar valores
        chavePlayerPrefs = "tutorial_passou_" + arquivo;
        chaveTrigger = "tutorial_trigger_" + arquivo;

        // Carregar valores salvos
        passou = PlayerPrefs.GetInt(chavePlayerPrefs, 0) == 1;
        triggerOn = PlayerPrefs.GetInt(chaveTrigger, 0) == 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Borboleta"))
        {
            triggerOn = true;
            PlayerPrefs.SetInt(chaveTrigger, 1); // salva triggerOn

            dialogo.nomeDoArquivo = arquivo;
            dialogo.comecar = true;
            anim.Play("borboleta");
            anim.SetInteger("horizontal", 1);
            trocaPersona = true;

            if (!passou)
            {
                passou = true;
                PlayerPrefs.SetInt(chavePlayerPrefs, 1); // salva passou
            }

            if (persona != null)
            {
                persona.SetActive(true);
            }

            PlayerPrefs.Save();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Borboleta"))
        {
            triggerOn = false;
            PlayerPrefs.SetInt(chaveTrigger, 0); // salva triggerOff
            PlayerPrefs.Save();
        }
    }       

    void Update()
    {
        if (persona != null && trocaPersona == true)
        {
            if (dialogo.i == 3)
            {
                persona.SetActive(false);
                trocaPersona = false;
            }
        }
        else
        {
            trocaPersona = false;
        }
    }
}
