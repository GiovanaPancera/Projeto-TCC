using System.Collections;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;

public class TextoParaTextPro : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public GameObject[] canvas;
    public string nomeDoArquivo;
    public int i; //linha lida
    public bool comecar = false;
    public bool acabou = false;
    private string[] linhas;
    private bool pulandoTexto = false;
    private bool esperandoEnter = false;
    private bool exibindoTexto = false;

    void Update()
    {
        
        if (comecar)
        {
            canvas[0].SetActive(true);
            acabou = false;
            EventSystem.current.SetSelectedGameObject(null); // <- limpar qualquer seleção de botão ou UI
            LerArquivo();
            StartCoroutine(ExibirLinhas());
            comecar = false;
        }        

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (exibindoTexto) 
            {
                // Se apertar ENTER enquanto as linhas estão sendo escritas, exibe todas de uma vez
                pulandoTexto = true;
            }
            else
            {
                // Se apertar ENTER depois que tudo apareceu, avança para a próxima parte
                esperandoEnter = true;
            }
        }

        if (acabou == true)
        {
            for (int index = 0; index < canvas.Length ; index ++)
            {
                canvas[index].SetActive(false);
            }
        }
    }

    void LerArquivo()
    {
        TextAsset arquivo = Resources.Load<TextAsset>(nomeDoArquivo);
        if (arquivo != null)
        {
            linhas = arquivo.text.Split('\n');
        }
        else
        {
            Debug.LogError("Arquivo não encontrado: " + nomeDoArquivo);
        }
    }

    IEnumerator ExibirLinhas()
    {
        if (linhas != null && linhas.Length > 0)
        {
            Time.timeScale = 0;

            for ( i = 0; i < linhas.Length; i += 3)
            {
                textMeshPro.text = "";
                pulandoTexto = false;
                exibindoTexto = true;

                // Exibir até 3 linhas por vez
                for (int j = 0; j < 3 && (i + j) < linhas.Length; j++)
                {
                    yield return StartCoroutine(EscreverTexto(linhas[i + j].Trim()));
                    textMeshPro.text += "\n";
                }

                exibindoTexto = false;

                // Espera o jogador apertar Enter para continuar
                esperandoEnter = false;
                yield return new WaitUntil(() => esperandoEnter);
            }

            textMeshPro.text = "";           
            Time.timeScale = 1;
            acabou = true;
            i = 0; 
        }
    }

    IEnumerator EscreverTexto(string frase)
    {
        if (pulandoTexto)
        {
            textMeshPro.text += frase; // Mostra tudo de uma vez
            yield break;
        }

        for (int i = 0; i < frase.Length; i++)
        {
            if (pulandoTexto)
            {
                textMeshPro.text += frase.Substring(i);
                yield break;
            }

            textMeshPro.text += frase[i];
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}
