using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Video : MonoBehaviour
{
    private VideoPlayer videoPlayer; // Referência ao Video Player

    void Start()
    {
        // Vincula o evento de término do vídeo
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Método chamado quando o vídeo termina
    void OnVideoEnd(VideoPlayer vp)
    {
        // Carrega a próxima cena
        SceneManager.LoadScene("Fase1");
    }
}
