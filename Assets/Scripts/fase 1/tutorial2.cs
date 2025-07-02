using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class tutorial2 : MonoBehaviour
{
    private CircleCollider2D colisor;
    public TextoParaTextPro dialogo;
    public string arquivo;
    public int primeiraVista = 0;
    private string chavePlayerPrefs;

    void Start()
    {
        colisor = GetComponent<CircleCollider2D>();

        // Criar uma chave única para salvar, pode usar o nome do arquivo por exemplo
        chavePlayerPrefs = "tutorial2_primeiraVista_" + arquivo;

        // Carregar valor salvo, ou padrão 0 se não existir
        primeiraVista = PlayerPrefs.GetInt(chavePlayerPrefs, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (primeiraVista < 1)
            {
                dialogo.nomeDoArquivo = arquivo;
                dialogo.comecar = true;
                primeiraVista += 1;

                // Salvar no PlayerPrefs
                PlayerPrefs.SetInt(chavePlayerPrefs, primeiraVista);
                PlayerPrefs.Save();
            }
        }
    }
}
