using UnityEngine;
using System.Collections;

public class Cazador : MonoBehaviour
{
    public float tiempoParaSalir = 3f;
    public float velocidadTransicion = 2f;

    private bool jugadorDetectado = false;
    private bool enTransicion = false;

    AudioSource audioSource;
    public AudioClip disparoEscopeta;
    float posicionInicialX, posicionInicialY, posicionInicialZ;

    void Start()
    {
        posicionInicialX = transform.position.x;
        posicionInicialY = transform.position.y;
        posicionInicialZ = transform.position.z;

        audioSource = GetComponent<AudioSource>();
        // Iniciar la coroutine para hacer que el cazador se vaya después de un tiempo
        StartCoroutine(EsperarYSalir());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDetectado = true;
            Debug.Log("Game Over: El jugador ha muerto.");
            audioSource.PlayOneShot(disparoEscopeta);
        }
    }

    IEnumerator EsperarYSalir()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2.8f, 3.5f));
            while (Mathf.Abs(transform.position.y - posicionInicialY) < 16)
            {
                yield return null;
                transform.position -= new Vector3(0, 0.1f, 0);
            }
            yield return new WaitForSeconds(4);
            while (transform.position.y < posicionInicialY)
            {
                yield return null;
                transform.position += new Vector3(0, 0.1f, 0);
                if (transform.position.y > posicionInicialY)
                {
                    transform.position = new Vector3(posicionInicialX, posicionInicialY, posicionInicialZ);
                    break;
                }
            }
        }
    }
}