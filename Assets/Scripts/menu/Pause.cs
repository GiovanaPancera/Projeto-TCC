using UnityEngine;
using UnityEngine.SceneManagement; 

public class Pause : MonoBehaviour
{
    public GameObject configuracao, fechar, controles, fechar2, voltar, sair;
    public AudioSource som;

    public void Config()
    {
       som.Play(); 
       configuracao.SetActive(true);
       fechar.SetActive(true);       
       Time.timeScale = 0;
    }

    public void VoltarJogo()
    { 
        som.Play(); 
        configuracao.SetActive(false);
        fechar.SetActive(false);     
        Time.timeScale = 1;
    }

    public void Controles()
    { 
        som.Play(); 
        controles.SetActive(true);
        fechar2.SetActive(true); 
        voltar.SetActive(false);
        fechar.SetActive(false); 
        sair.SetActive(false);    
    }

    public void VoltarConfig()
    { 
        som.Play(); 
        controles.SetActive(false);
        fechar2.SetActive(false); 
        voltar.SetActive(true);
        fechar.SetActive(true); 
        sair.SetActive(true);     
    }

    public void Reiniciar()
    {
      
    }

    public void VoltarMenu()
    {
        som.Play(); 
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void SairJogo()
    { 
        som.Play(); 
        Application.Quit();
    }
}