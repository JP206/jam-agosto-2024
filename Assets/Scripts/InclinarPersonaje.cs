using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InclinarPersonaje : MonoBehaviour
{
    [SerializeField] private float rayLength = 1f; // Longitud del raycast
    [SerializeField] private LayerMask groundLayer; // Capa para detectar el suelo
    [SerializeField] private float rotacionSuavizada = 5f; // Velocidad de rotación del personaje
    [SerializeField] private float offsetRayos;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Crear dos rayos, uno desde el lado izquierdo y otro desde el lado derecho del personaje
        Vector2 posicionIzquierda = transform.position - transform.right * offsetRayos;
        Vector2 posicionDerecha = transform.position + transform.right * offsetRayos;

        // Lanzar rayos hacia abajo para detectar la pendiente
        RaycastHit2D hitIzquierda = Physics2D.Raycast(posicionIzquierda, Vector2.down, rayLength, groundLayer);
        RaycastHit2D hitDerecha = Physics2D.Raycast(posicionDerecha, Vector2.down, rayLength, groundLayer);

        // Visualización de rayos en la escena para depuración
        Debug.DrawRay(posicionIzquierda, Vector2.down * rayLength, Color.red);
        Debug.DrawRay(posicionDerecha, Vector2.down * rayLength, Color.red);

        if (hitIzquierda.collider != null && hitDerecha.collider != null)
        {
            // Calcular el ángulo de inclinación basado en la diferencia de altura de los dos puntos de impacto
            Vector2 puntoIzquierda = hitIzquierda.point;
            Vector2 puntoDerecha = hitDerecha.point;

            // Calcular la diferencia en posiciones de los puntos de impacto
            float anguloPendiente = Mathf.Atan2(puntoIzquierda.y - puntoDerecha.y, puntoIzquierda.x - puntoDerecha.x) * Mathf.Rad2Deg;

            // Ajustar la rotación del personaje
            Quaternion objetivoRotacion = Quaternion.Euler(0f, 0f, anguloPendiente);
            transform.rotation = objetivoRotacion;
        }
    }
}
