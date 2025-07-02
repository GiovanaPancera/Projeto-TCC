using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vinha : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D box;
    public runa[] runas;  
    public bool desbloqueada;
    private int[] identifica;
    private int[] senha1;
    private int[] senha2;

    private string chaveDesbloqueada;
    private string chaveCollider;

    void Start()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();

        // Define chaves únicas
        chaveDesbloqueada = "Vinha_desbloqueada_" + gameObject.name;
        chaveCollider = "Vinha_collider_" + gameObject.name;

        // Carrega os valores salvos
        desbloqueada = PlayerPrefs.GetInt(chaveDesbloqueada, 0) == 1;
        bool colliderAtivo = PlayerPrefs.GetInt(chaveCollider, 1) == 1;
        box.enabled = colliderAtivo;

        // Se já estava desbloqueada, toca a animação
        if (desbloqueada)
        {
            anim.SetBool("desbloqueou", true);
        }

        identifica = new int[runas.Length];
        senha1 = new int[] { 0, 1, 0, 1, 0};
        senha2 = new int[] { 1, 0, 2, 2, 2, 1, 0, 0, 2, 0, 1 };
    }

    void Update()
    {
        for (int i = 0; i < runas.Length; i++)
        {
            identifica[i] = runas[i].id;
        }

        if (!desbloqueada && Desbloqueio())
        {
            anim.SetBool("desbloqueou", true); 
            box.enabled = false;
            desbloqueada = true;

            // Salva o estado
            PlayerPrefs.SetInt(chaveDesbloqueada, 1);
            PlayerPrefs.SetInt(chaveCollider, 0); // false
        }
    }

    private bool Desbloqueio()
    {
        if (runas.Length == 5)
        {
            for (int i = 0; i < runas.Length; i++)
            {
                if (identifica[i] != senha1[i])
                    return false;
            }
            return true;
        }
        else if (runas.Length == 11)
        {
            for (int i = 0; i < runas.Length; i++)
            {
                if (identifica[i] != senha2[i])
                    return false;
            }
            return true;
        }

        return false; 
    }
}
