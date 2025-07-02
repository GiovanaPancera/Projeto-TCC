using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falha : MonoBehaviour
{
    public GameObject grid, splash, personagem, morteKerolyne, morteKerocat;
    private GameObject personagemAtual, morte;
    private transforma indice;    
    private Quaternion rotacao;
    public bool estaMorrendo = false;
    public List<Vector3> posicoes = new List<Vector3>(); // Lista dinâmica de posições
    private Animator anim;

    void Start()
    {
        anim = splash.GetComponent<Animator>();
        indice = personagem.GetComponent<transforma>();

        // Carrega a última posição salva, se existir
        Vector3 posSalva = CarregarUltimaPosicao();
        personagem.transform.position = posSalva;

        // Também adiciona a posição carregada na lista para manter consistência
        posicoes.Add(posSalva);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SafePoint"))
        {  
            Vector3 novaPosicao = personagem.transform.position;

            // Verifica se a nova posição já está na lista
            if (!posicoes.Contains(novaPosicao))
            {
                posicoes.Add(novaPosicao);
                SalvarUltimaPosicao(novaPosicao);  // Salva sempre que adiciona uma nova posição
            }
        }

        if (other.CompareTag("VitoriaRegia"))
        {
            StartCoroutine(PlaySplash());
        }

        if(other.CompareTag("BolaFogo") && !estaMorrendo)
        {
            Debug.Log("Colisão com BolaFogo detectada!");
            StartCoroutine(Queimado());
        }
    }

    private IEnumerator PlaySplash()
    {
        personagemAtual = GameObject.FindWithTag("Player"); // Aqui antes de desativar
        grid.SetActive(false);
        splash.SetActive(true); 
        splash.transform.position = personagem.transform.position;
        personagemAtual.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        anim.Play("agua");

        yield return new WaitForSeconds(1.4f);

        splash.SetActive(false);
        
        personagem.transform.position = posicoes[posicoes.Count - 1]; // Última posição registrada
        personagemAtual.transform.position = posicoes[posicoes.Count - 1]; 
        
        grid.SetActive(true);
        personagemAtual.transform.localScale = new Vector3(1f,1f,1f);
    }

    private IEnumerator Queimado()
    {
        estaMorrendo = true;
        personagemAtual = GameObject.FindWithTag("Player"); // Aqui antes de desativar
        rotacao = personagemAtual.transform.rotation;
        personagemAtual.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        
        if (indice.indiceAtual % 2 == 0)
        {
            morte = Instantiate(morteKerolyne, personagemAtual.transform.position, rotacao );
        }
        else
        {
            morte = Instantiate(morteKerocat, personagemAtual.transform.position, rotacao );
        }
       
        yield return new WaitForSeconds(1f);
        Destroy(morte, 1f);
        personagem.transform.position = posicoes[posicoes.Count - 1]; // Última posição registrada
        personagemAtual.transform.position = posicoes[posicoes.Count - 1]; 
        personagemAtual.transform.localScale = new Vector3(1f,1f,1f);
        estaMorrendo = false; // Libera novamente para futuras mortes
    }   

    // Salva a última posição no PlayerPrefs
    private void SalvarUltimaPosicao(Vector3 pos)
    {
        PlayerPrefs.SetFloat("ultimaPosX", pos.x);
        PlayerPrefs.SetFloat("ultimaPosY", pos.y);
        PlayerPrefs.SetFloat("ultimaPosZ", pos.z);
        PlayerPrefs.Save();
    }

    // Carrega a última posição salva ou retorna a posição atual do personagem (padrão)
    private Vector3 CarregarUltimaPosicao()
    {
        float x = PlayerPrefs.GetFloat("ultimaPosX", personagem.transform.position.x);
        float y = PlayerPrefs.GetFloat("ultimaPosY", personagem.transform.position.y);
        float z = PlayerPrefs.GetFloat("ultimaPosZ", personagem.transform.position.z);

        return new Vector3(x, y, z);
    }
}
