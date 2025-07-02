using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject painelMenuPrincipal;
    [SerializeField] private GameObject painelCredito;
    [SerializeField] private GameObject painelConfiguracao;
    public int clicou;
    public volume slider, slider2;
    public GameObject transicao;
    public GameObject[] borboleta;
    private Animator anim;
    private AudioSource som;

    void Start()
    {
        som = GetComponent<AudioSource>();
        anim = transicao.GetComponent<Animator>();
        clicou = PlayerPrefs.GetInt("Clicou", 0);
    }

    public void Jogar()
    {
        som.Play(); 
        clicou = 1;
        float volMusica = slider.valor;
        float volEfeitos = slider2.valor;
        PlayerPrefs.DeleteAll(); 
        PlayerPrefs.SetFloat("VolumeMusica", volMusica);
        PlayerPrefs.SetFloat("VolumeEfeitos", volEfeitos);
        PlayerPrefs.SetInt("Clicou", clicou);
        transicao.SetActive(true);
        apagarBorboletas();
        StartCoroutine(TransicaoCena());  
    }

    public void ContinuarJogo()
    {
        if (clicou == 1)
        {
            som.Play();
            transicao.SetActive(true);
            StartCoroutine(TransicaoCenaContinuar());
        }
    }

    public void AbrirCreditos()
    { 
        som.Play(); 
        painelMenuPrincipal.SetActive(false);
        painelCredito.SetActive(true);
        apagarBorboletas();
    }

    public void FecharCreditos()
    {
        som.Play(); 
        painelCredito.SetActive(false);
        painelMenuPrincipal.SetActive(true);
        mostarBorboletas();
    }

    public void AbrirConfig()
    { 
        som.Play(); 
        painelMenuPrincipal.SetActive(false);
        painelConfiguracao.SetActive(true);
        apagarBorboletas();
    }

    public void FecharConfig()
    {
        som.Play(); 
        painelConfiguracao.SetActive(false);
        painelMenuPrincipal.SetActive(true);
        mostarBorboletas();
    }

    public void SairJogo()
    { 
        Application.Quit();
    }

    private void apagarBorboletas()
    {
        for (int i = 0; i < borboleta.Length; i++)
        {
            borboleta[i].SetActive(false);
        }
    }

    private void mostarBorboletas()
    {
        for (int i = 0; i < borboleta.Length; i++)
        {
            borboleta[i].SetActive(true);
        }
    }

    IEnumerator TransicaoCena()
    {
        anim.SetBool("mudou", true); 
        yield return new WaitForSeconds(1.1f); 
        SceneManager.LoadScene("Cutscene");
    }

    IEnumerator TransicaoCenaContinuar()
    {
        anim.SetBool("mudou", true);
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("Fase1");
    }
}
