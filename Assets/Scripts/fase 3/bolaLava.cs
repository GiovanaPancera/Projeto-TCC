using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolaLava : MonoBehaviour
{
    public float moveHeight = 2f; // Altura máxima que o objeto vai subir a partir da posição inicial
    public float moveSpeed = 0.8f; // Velocidade com que o objeto se move  
    private Vector3 posicaoInicial; // Posição inicial do objeto
    private Vector3 posicaoAtual;    
    private Vector3 posicaoFinal; // Posição de destino (acima da posição inicial)    
    private bool subindo; // indica se o objeto está subindo ou descendo

    private void Start()
    {
        posicaoInicial = transform.position;     
        posicaoFinal = posicaoInicial + Vector3.up * moveHeight; // Calcula a posição alvo para onde ele deve subir
        subindo = true;
    }

    private void Update()
    {
        if(subindo == true)
        {
            posicaoFinal = posicaoInicial + Vector3.up * moveHeight; 
            posicaoAtual = transform.position;

            if(posicaoAtual.y < posicaoFinal.y)
            {
                posicaoAtual.y +=  moveSpeed * Time.deltaTime;
                transform.position = posicaoAtual;
            }
            else
            {
                subindo = false;
            }
        }
        else
        {
            posicaoAtual = transform.position;

            if(posicaoAtual.y > posicaoInicial.y)
            {
                posicaoAtual.y -=  moveSpeed * Time.deltaTime;
                transform.position = posicaoAtual;
            }
            else
            {
                subindo = true;
            }
        }
    }
}

