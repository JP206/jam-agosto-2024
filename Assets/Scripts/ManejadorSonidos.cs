using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorSonidos : MonoBehaviour
{
    public AudioClip grunido, aullidoCachorros, clipMusicaTranquila, clipMusicaTensa, arbolCayendo, sonidoPasos;
    AudioSource sfx, musica, pasos;
    bool musicaTranqui = true;

    int cantidadAullidosCachorros = 0;

    CambiarColorFondo[] fondos;
   
    void Start()
    {
        sfx = transform.Find("sfx").GetComponent<AudioSource>();
        musica = transform.Find("musica").GetComponent<AudioSource>();
        pasos = transform.Find("sonido pasos").GetComponent<AudioSource>();

        fondos = FindObjectsOfType<CambiarColorFondo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cazador"))
        {
            sfx.PlayOneShot(grunido);
        }

        if (collision.CompareTag("trigger cachorros"))
        {
            StartCoroutine(AullidosCachorros());
        }

        if (collision.CompareTag("trigger musica"))
        {
            StartCoroutine(CambiarMusica());
        }
    }

    /// <summary>
    /// Corrutina que ajusta el volumen del Audio Source cuando aullan los cachorros.
    /// </summary>
    /// <returns></returns>
    IEnumerator AullidosCachorros()
    {
        if (cantidadAullidosCachorros == 0)
        {
            sfx.volume = 0.5f;
        }
        else if (cantidadAullidosCachorros == 1)
        {
            sfx.volume = 0.75f;
        }

        sfx.PlayOneShot(aullidoCachorros);
        yield return new WaitForSeconds(6.1f);
        sfx.volume = 1;
        cantidadAullidosCachorros++;
    }

    /// <summary>
    /// Hace fade out y empieza el otro clip. Alterna entre musica tranquila y tensa.
    /// </summary>
    /// <returns></returns>
    IEnumerator CambiarMusica()
    {
        musicaTranqui = !musicaTranqui;
        while (musica.volume > 0)
        {
            musica.volume -= 0.05f;
            yield return new WaitForSeconds(0.01f);
        }
        if (musicaTranqui)
        {
            musica.clip = clipMusicaTranquila;
            FondoBlanco();
        }
        else
        {
            musica.clip = clipMusicaTensa;
            FondoVioleta();
        }
        musica.volume = 1;
        musica.Play();
    }

    public void ArbolCayendo()
    {
        sfx.PlayOneShot(arbolCayendo);
    }

    public void PlaySonidoPasos()
    {
        pasos.Play();
    }

    public void StopSonidoPasos()
    {
        pasos.Pause();
    }

    void FondoBlanco()
    {
        foreach (CambiarColorFondo c in fondos)
        {
            c.CambiarFondoBlanco();
        }
    }

    void FondoVioleta()
    {
        foreach (CambiarColorFondo c in fondos)
        {
            c.CambiarFondoVioleta();
        }
    }
}
