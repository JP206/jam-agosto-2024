using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCazador : MonoBehaviour
{
    bool mirandoIzquierda = false, disparo = false;
    [SerializeField] float distanciaRaycast, offsetRaycast, velocidad;
    AnimacionesCazador animacionesCazador;

    void Start()
    {
        animacionesCazador = GetComponent<AnimacionesCazador>();
        StartCoroutine(MovimientoCazador());
    }

    void Update()
    {
        Detectar();
    }

    void Detectar()
    {
        Vector3 posicion = new(transform.position.x, transform.position.y - offsetRaycast, transform.position.z);
        Vector2 lado = mirandoIzquierda ? Vector2.left : Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(posicion, lado, distanciaRaycast);
        Debug.DrawRay(posicion, lado * distanciaRaycast, Color.black);
        if (hit.collider != null && hit.collider.name.Equals("jugador"))
        {
            if (!disparo)
            {
                StopAllCoroutines();
                animacionesCazador.Disparar();
                disparo = true;
            }
        }
    }

    IEnumerator MovimientoCazador()
    {
        float tiempoCaminar = 0;
        while (true)
        {
            animacionesCazador.Caminar();
            // Camina por 5 segundos.
            while (tiempoCaminar < 5)
            {
                tiempoCaminar += Time.deltaTime;
                // Como se rota 180 grados el objeto, siempre debe caminar hacia la derecha.
                transform.Translate(Time.deltaTime * velocidad * Vector2.right);
                yield return null;
            }
            tiempoCaminar = 0;

            /// Se detiene por tiempo random.
            animacionesCazador.Idle();
            yield return new WaitForSeconds(Random.Range(2.5f, 4));

            // Rota al lado contrario.
            if (mirandoIzquierda)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                mirandoIzquierda = false;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                mirandoIzquierda = true;
            }
        }
    }
}
