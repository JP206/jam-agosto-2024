using System.Collections;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoLoba : MonoBehaviour
{
    //Objectos & Componentes

    [SerializeField] SpriteRenderer spriteRenderer;
    SpacialDetector detector;
    Animator animator;

    //Variables de entorno
    [SerializeField] float velocidadCaminar = 5f;
    [SerializeField] float velocidadCorrer = 10f;
    [SerializeField] float fuerzaEmpuje = 10f;
    [SerializeField] float fuerzaGravitatoria;

    float velocidadX;
    float velocidadY;

    //Propiedaes para escalar el movimiento
    float deltaX => velocidadX * Time.fixedDeltaTime;
    float deltaY => velocidadY * Time.fixedDeltaTime;
    bool saltando => velocidadY > 0;

    void FixedUpdate()
    {
        MovmientoY();

        //Movimiento del personaje multiplico las velocidades por fixedDeltaTime
        var movementVector = new Vector3(deltaX, deltaY);
        transform.Translate(movementVector);
    }

    public void InitializeReferences(SpacialDetector _spacialDetector, Animator _anim)
    {
        detector = _spacialDetector;
        animator = _anim;
    }

    public void MovimientoX(float valorX)
    {
        velocidadX = valorX * velocidadCaminar;

        //Setteo la velocidad de la variable del Animator
        animator.SetFloat("VelocidadX", Mathf.Abs(velocidadX));

        //Manejo la orientacion del personaje
        if (deltaX > 0f) spriteRenderer.flipX = true;
        else if (deltaX < 0) spriteRenderer.flipX = false;
    }

    public void MovmientoY()
    {
        if (!detector.EstaEnElSuelo(0.2f, deltaY))
        {
            //Controlo la fuerza de gravedad
            velocidadY -= fuerzaGravitatoria;
            Debug.Log("velocidadY" + velocidadY);
        }

        //Detecto si el personaje llego al piso aplicando a mi fuerza vertical 0 para que no se mueva
        if (!saltando && detector.EstaEnElSuelo(0.1f, deltaY))
        {
            velocidadY = 0;

            //Seteo posicion de Y
            Vector2 vector2 = new Vector2(transform.position.x, detector.PosicionYEnPiso());
            transform.position = vector2;

            //Setteo el boolean del animator para animacion de salto
            animator.SetBool("isJumping", false);
        }
    }

    public void AplicarSalto(float velocidadSalto)
    {
        velocidadY += velocidadSalto;
    }
}
