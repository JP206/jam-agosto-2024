using UnityEngine;
using System.Collections;

public class Cazador : MonoBehaviour
{
    public float tiempoParaSalir = 3f;
    public float velocidadTransicion = 2f; 

    private bool jugadorDetectado = false;
    private bool enTransicion = false;

    void Start()
    {
        // Iniciar la coroutine para hacer que el cazador se vaya después de un tiempo
        StartCoroutine(EsperarYSalir());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorDetectado = true;
            Debug.Log("Game Over: El jugador ha muerto.");
        }
    }

    IEnumerator EsperarYSalir()
    {
        yield return new WaitForSeconds(tiempoParaSalir);

        if (!jugadorDetectado)
        {
            enTransicion = true;
            Debug.Log("El cazador no encontró nada y se va.");
        }

        // Iniciar la transición de salida
        while (enTransicion)
        {
            // Cambiar la dirección si necesitas que se vaya en otra dirección
            transform.position += Vector3.left * velocidadTransicion * Time.deltaTime; 
            yield return null;
        }
    }
}
