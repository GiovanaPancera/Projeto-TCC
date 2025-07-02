using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usarBorboleta : MonoBehaviour
{
    public ColorHighlightEffect cor; // Referência ao ColorHighlightEffect
    public GameObject[] borboletas;
    public GameObject lampiao;
    public GameObject[] botaoBorboleta;
    public itemColetavel[] itemBorboleta;
    private bool[] foiPego;
    private int usando = 0;
    public AudioSource som;

    void Start()
    {
        foiPego = new bool[itemBorboleta.Length];
    }

    void Update()
    {
        for(int i = 0; i < itemBorboleta.Length; i++)
        {
            foiPego[i] = itemBorboleta[i].coletado;

            if ( foiPego[i] == true)
            {
                botaoBorboleta[i].SetActive(true);
            }
            else
            {
                botaoBorboleta[i].SetActive(false);
            }
        }
    }

// Atualiza a cor no ColorHighlightEffect
   public void cristalVermelho()
    {
        som.Play();
        cor.highlightColors[0] = new Color(0.7f, 0f, 0f);
        cor.threshold = 0.46f;
        cor.radii[0] = 0.25f;
        borboletaUsando(5);
    }

    public void cristalLaranja()
    {
        som.Play();
        cor.highlightColors[0] = new Color(1f, 0.3f, 0f); 
        cor.threshold = 0.3f;        
        cor.radii[0] = 0.25f;
        borboletaUsando(0);
    }

    public void cristalAmarelo()
    {
        som.Play();
        cor.highlightColors[0] =  new Color(0.828f, 0.7f, 0f); 
        cor.threshold = 0.3f; 
        cor.radii[0] = 0.25f;
        borboletaUsando(1);
    }

    public void cristalVerde()
    {
        som.Play();
        cor.highlightColors[0] =  new Color(0f, 0.55f, 0f);
        cor.threshold = 0.4f;
        cor.radii[0] = 0.25f;
        borboletaUsando(3);   
    }

   public void cristalAzul()
    {
        som.Play();
        cor.highlightColors[0] =  new Color(0f, 0.2f, 1f);  
        cor.threshold = 0.41f; 
        cor.radii[0] = 0.25f;
        borboletaUsando(4);
    }

    public void cristalAnil()
    {
        som.Play();
        cor.highlightColors[0] =  new Color(0.16f, 0.0f, 0.75f);  
        cor.threshold = 0.4f; 
        cor.radii[0] = 0.25f;
        borboletaUsando(2);
    }

    public void cristalVioleta()
    {
        som.Play();
        cor.highlightColors[0] =  new Color(0.45f, 0f, 0.67f);
        cor.threshold = 0.4f;
        cor.radii[0] = 0.25f;
        borboletaUsando(6);   
    }

    public void usarLampiao()
    {
        usando += 1;
        som.Play();
        if(usando % 2 == 0)
        {
            lampiao.SetActive(false);
        }
        else
        {
            lampiao.SetActive(true);   
        }
    }

    private void borboletaUsando(int posicao)
    {
        for(int i = 0; i < 7; i++)
        {
            if(i == posicao)
            {
                borboletas[i].SetActive(true);
            }
            else
            {
                borboletas[i].SetActive(false);
            }
        }
    }
   
}
