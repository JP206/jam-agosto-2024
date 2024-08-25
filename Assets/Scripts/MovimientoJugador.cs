using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoJugador : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float velocidadCaminar = 5f;
    [SerializeField] float velocidadCorrer = 10f;
    [SerializeField] float fuerzaSalto = 10f;
    [SerializeField] float fuerzaEmpuje = 10f;

    Vector2 movimiento;
    [SerializeField] Animator animator;
    float bufferCheckDistance = 0.3f, groundCheckDistance;
    float velocidadActual;
    float moveInput;
    bool quiereSaltar = false;
    bool puedeSaltar = true;
    bool estaEmpujando = false;

    private GameObject arbol;
    private GameObject jaula;

    ManejadorSonidos manejadorSonidos;

    bool flagSonidoPasos = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheckDistance = GetComponent<BoxCollider2D>().size.y / 2 + bufferCheckDistance;
        velocidadActual = velocidadCaminar;

        manejadorSonidos = GetComponent<ManejadorSonidos>();
    }

    void Update()
    {
        if (!estaEmpujando)
        {
            moveInput = Input.GetAxis("Horizontal");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                velocidadActual = velocidadCorrer;
                animator.SetBool("isDashing", true);
                animator.SetBool("IsRunning", false);
            }
            else
            {
                velocidadActual = velocidadCaminar;
                animator.SetBool("isDashing", false);
                animator.SetBool("IsRunning", moveInput != 0);
            }

            // Controlar la rotación del personaje
            RotacionHandler();

            // Chequear si está en el suelo
            PisoChecker();

            // Detectar si el jugador quiere saltar
            if (puedeSaltar && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                quiereSaltar = true;
                animator.SetBool("isJumping", true);
            }

            // Si no hay movimiento y no se está saltando, está en Idle
            if (moveInput == 0 && puedeSaltar)
            {
                animator.SetBool("IsRunning", false);
                animator.SetBool("isJumping", false);
            }
        }
    }

    void FixedUpdate()
    {
        if (!estaEmpujando)
        {
            MovimientoHandler();

            // Aplicar el salto si es necesario
            if (quiereSaltar)
            {
                AplicarSalto(fuerzaSalto);
                quiereSaltar = false;
            }
        }
    }

    void MovimientoHandler()
    {
        movimiento = new Vector2(moveInput * velocidadActual, rb.velocity.y);
        rb.velocity = movimiento;

        if (rb.velocity.x != 0 && puedeSaltar && flagSonidoPasos)
        {
            manejadorSonidos.PlaySonidoPasos();
            flagSonidoPasos = false;
        }
        else
        {
            flagSonidoPasos = true;
            manejadorSonidos.StopSonidoPasos();
        }
    }

    void RotacionHandler()
    {
        if (movimiento.x < 0) transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (movimiento.x > 0) transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    void PisoChecker()
    {
        Vector2 posicion = transform.position;
        Vector2 esquinaIzquierda = posicion - new Vector2(GetComponent<BoxCollider2D>().size.x / 2, 0);
        Vector2 esquinaDerecha = posicion + new Vector2(GetComponent<BoxCollider2D>().size.x / 2, 0);

        RaycastHit2D hitCentro = Physics2D.Raycast(posicion, Vector2.down, groundCheckDistance);
        RaycastHit2D hitIzquierda = Physics2D.Raycast(esquinaIzquierda, Vector2.down, groundCheckDistance);
        RaycastHit2D hitDerecha = Physics2D.Raycast(esquinaDerecha, Vector2.down, groundCheckDistance);

        puedeSaltar = hitCentro.collider || hitIzquierda.collider || hitDerecha.collider;
        animator.SetBool("isJumping", !puedeSaltar);
    }

    void AplicarSalto(float fuerzaSalto)
    {
        rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
        animator.SetBool("isJumping", true);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Arbol") && !estaEmpujando)
        {
            arbol = col.gameObject;

            BloqueMovible bloqueMovible = arbol.GetComponent<BloqueMovible>();
            if (bloqueMovible != null && !bloqueMovible.EstaEnElSuelo())
            {
                estaEmpujando = true;
                rb.velocity = Vector2.zero;

                // Detenemos cualquier animación en curso
                animator.SetBool("isDashing", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("IsRunning", false);

                StartCoroutine(EsperarUnFrameYEmpujar());
            }
        }

        // Aquí se detecta la colisión con el objeto que tiene el tag "Lobitos"
        if (col.gameObject.CompareTag("Lobitos"))
        {
            StartCoroutine(PonerEnIdleYCargarGameOver());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("caida piedra"))
        {
            // un poco de acoplamiento de codigo
            GameObject.Find("piedra que cae").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void EjecutarCorutina()
    {
        StartCoroutine(TransitionToGameOver());
    }

    IEnumerator TransitionToGameOver()
    {
        animator.SetBool("isHitted", true);
        // Esperar la duración de la animación de golpeo
        yield return new WaitForSeconds(1f);

        // Cargar la escena de Game Over
        SceneManager.LoadScene("GameOver");
    }

    IEnumerator EsperarUnFrameYEmpujar()
    {
        yield return new WaitForEndOfFrame();
        IniciarEmpuje();
    }

    void IniciarEmpuje()
    {
        animator.SetBool("isInteracting", true);
        Invoke("ComenzarEmpuje", 1f);
    }

    void ComenzarEmpuje()
    {
        animator.SetTrigger("Empujar");

        if (arbol != null)
        {
            Rigidbody2D arbolRb = arbol.GetComponent<Rigidbody2D>();
            if (arbolRb != null)
            {
                arbolRb.bodyType = RigidbodyType2D.Dynamic;
                arbolRb.AddForce(new Vector2(fuerzaEmpuje * transform.localScale.x, 0), ForceMode2D.Impulse);
                manejadorSonidos.ArbolCayendo();
            }
        }

        StartCoroutine(EsperarArbolCaido());
    }

    IEnumerator EsperarArbolCaido()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        if (arbol != null)
        {
            animator.SetBool("isInteracting", false);
            estaEmpujando = false;

            // Restablecer cualquier estado que pueda haberse alterado durante el empuje
            ResetearEstados();
        }
    }

    void ResetearEstados()
    {
        // Asegurar que se puedan realizar otras acciones (como saltar) después de empujar
        animator.SetBool("isJumping", false);
        animator.SetBool("isDashing", false);
        animator.SetBool("IsRunning", false);
        animator.SetBool("isInteracting", false);

        puedeSaltar = true;
        quiereSaltar = false;
        estaEmpujando = false;
    }

    // Nueva corutina para poner al personaje en idle antes de cargar la escena de Game Over
    IEnumerator PonerEnIdleYCargarGameOver()
    {
        // Poner al personaje en idle
        animator.SetBool("isRunning", false);
        animator.SetBool("isDashing", false);
        animator.SetBool("isInteracting", true);

        // Esperar 1 segundo
        yield return new WaitForSeconds(1f);

        // Cargar la escena de Game Over
        SceneManager.LoadScene("Ending");
    }
}
