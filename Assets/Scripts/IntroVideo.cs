using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Referencia al componente VideoPlayer

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // Suscribirse al evento loopPointReached
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // M�todo que se ejecuta cuando el video termina
    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("Juego");
    }
}
