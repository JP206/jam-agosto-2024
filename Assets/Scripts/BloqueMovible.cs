using UnityEngine;

public class BloqueMovible : MonoBehaviour
{
    Rigidbody2D rb;
    bool estaEnElSuelo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("Rigidbody2D no se encontró en el objeto.");
        else Debug.Log("Rigidbody2D encontrado y asignado correctamente.");    
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Verificola colision con el suelo por Tag o por Layer
        if (col.gameObject.CompareTag("Suelo") || col.gameObject.CompareTag("piso") || col.gameObject.layer == LayerMask.NameToLayer("Piso"))
        {
            // Verifico ambos angulos de rotacion si el "Arbol" esta caido
            float rotacionZ = transform.rotation.eulerAngles.z;

            // Ajusto el rango a tener en cuenta para cambiar el body type del rigid body
            if ((rotacionZ > 80 && rotacionZ < 100) || (rotacionZ > 260 && rotacionZ < 280))
            {
                estaEnElSuelo = true;
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static; 
            }
        }
    }

    void Update()
    {
        if (estaEnElSuelo)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}
