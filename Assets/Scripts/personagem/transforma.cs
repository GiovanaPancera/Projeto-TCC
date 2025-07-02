using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transforma : MonoBehaviour
{
    public GameObject[] personagens; 
    public GameObject morte; 
    public GameObject personagemAtual;
    public int indiceAtual = 0;    
    private Vector3 posicao;
    private Quaternion rotacao;
    public ParticleSystem efeito;

    void Start() 
    {
        indiceAtual = PlayerPrefs.GetInt("PersonagemIndice", 0);
        float x = PlayerPrefs.GetFloat("PersonagemX", transform.position.x);
        float y = PlayerPrefs.GetFloat("PersonagemY", transform.position.y);
        float z = PlayerPrefs.GetFloat("PersonagemZ", transform.position.z);
        Vector3 posicaoSalva = new Vector3(x, y, z);
        personagemAtual = Instantiate(personagens[indiceAtual], posicaoSalva, transform.rotation);
    }

    void Update() 
    {           
        posicao = personagemAtual.transform.position;
        transform.position = posicao;
        efeito.transform.position = posicao - new Vector3(0, 0.15f, 0);

        if (Input.GetKeyDown(KeyCode.X)) 
        {  
            efeito.Play();
            Invoke("TrocarPersonagem", 1.2f);
        }

        SalvarEstado(); 
    }

    void TrocarPersonagem() 
    {   
        rotacao = personagemAtual.transform.rotation;
        Destroy(personagemAtual);
        indiceAtual = (indiceAtual + 1) % personagens.Length;
        personagemAtual = Instantiate(personagens[indiceAtual], posicao, rotacao);
        SalvarEstado();
    }

    void SalvarEstado()
    {
        PlayerPrefs.SetInt("PersonagemIndice", indiceAtual);
        PlayerPrefs.SetFloat("PersonagemX", personagemAtual.transform.position.x);
        PlayerPrefs.SetFloat("PersonagemY", personagemAtual.transform.position.y);
        PlayerPrefs.SetFloat("PersonagemZ", personagemAtual.transform.position.z);
    }
}
