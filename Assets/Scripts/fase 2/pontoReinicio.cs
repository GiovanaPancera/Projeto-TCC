using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pontoReinicio : MonoBehaviour
{
    public GameObject personagem;
    public falha posicao;
    // Start is called before the first frame update
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {  
            Vector3 novaPosicao = personagem.transform.position;
            
             // Verifica se a nova posição já está na lista
            if (!posicao.posicoes.Contains(novaPosicao))
            {
                posicao.posicoes.Add(novaPosicao);
            }
        }
    }
}
