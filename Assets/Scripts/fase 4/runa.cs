using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runa : MonoBehaviour
{
    public transforma personagem;   
    public Sprite[] runas;  
    public int id, indice;
    private SpriteRenderer sp;

    private string chaveIndice;
    private string chaveID;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();

        // Define chaves únicas para cada runa
        chaveIndice = "Runa_indice_" + gameObject.name;
        chaveID = "Runa_id_" + gameObject.name;

        // Carrega valores salvos ou usa valores padrão
        indice = PlayerPrefs.GetInt(chaveIndice, -1);
        id = PlayerPrefs.GetInt(chaveID, -1);

        // Atualiza o sprite com base no índice carregado
        if (id >= 0 && runas.Length > 0 && indice >= 0 && indice < runas.Length)
        {
            sp.sprite = runas[indice];
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            if (personagem.indiceAtual % 2 == 0)
            {
                indice = (indice + 1) % runas.Length;
                id = (id + 1) % runas.Length;

                sp.sprite = runas[indice];

                // Salva os novos valores
                PlayerPrefs.SetInt(chaveIndice, indice);
                PlayerPrefs.SetInt(chaveID, id);
            }
        }
    }
}
