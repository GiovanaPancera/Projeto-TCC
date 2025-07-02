using System.Collections;
using UnityEngine;

public class itemColetavel : MonoBehaviour
{
    public bool coletado = false;
    public bool jogadorDentro = false;
    public TextoParaTextPro dialogo;
    public GameObject persona1, persona2;
    public string arquivo;
    public string chaveColeta; // chave única para este item
    public AudioSource som;

    private void Start()
    {
        // Verifica se esse item já foi coletado em uma sessão anterior
        if (PlayerPrefs.GetInt(chaveColeta, 0) == 1)
        {
            coletado = true;
            gameObject.SetActive(false); // Desativa o objeto se já foi coletado
            transform.position = new Vector3(0,0,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorDentro = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorDentro = false;
        }
    }

    private void Update()
    {
        if (jogadorDentro && !coletado && Input.GetKeyDown(KeyCode.Z))
        {
            som.Play();
            coletado = true;
            PlayerPrefs.SetInt(chaveColeta, 1);
            PlayerPrefs.Save();
            persona2.SetActive(false);
            persona1.SetActive(true);
            dialogo.nomeDoArquivo = arquivo;
            dialogo.comecar = true;
            transform.position = new Vector3(0,0,0);
            gameObject.SetActive(false);
        }
    }
}
