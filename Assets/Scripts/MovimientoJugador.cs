using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    // Objetos y animaciones
    Rigidbody2D rb;
    [SerializeField] float velocidadCaminar = 5f; // Velocidad al caminar
    [SerializeField] float velocidadCorrer = 10f; // Velocidad al correr
    [SerializeField] float fuerzaSalto = 10f;
    [SerializeField] float gravedad;

    // Vectores y variables de entorno
    Vector2 movimiento;
    float bufferCheckDistance = 0.3f, groundCheckDistance;
    float velocidadActual;
    float moveInput;
    bool quiereSaltar = false;
    bool puedeSaltar = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheckDistance = GetComponent<BoxCollider2D>().size.y / 2 + bufferCheckDistance;
        velocidadActual = velocidadCaminar;
    }

    void Update()
    {
        // Capturar la entrada del jugador
        moveInput = Input.GetAxis("Horizontal");

        // Cambiar entre caminar y correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidadActual = velocidadCorrer;
        }
        else
        {
            velocidadActual = velocidadCaminar;
        }

        // Rotación del personaje
        RotacionHandler();

        // Detección de suelo
        PisoChecker();

        // Detectar si el jugador quiere saltar
        if (puedeSaltar && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            quiereSaltar = true;  
        }
    }

    void FixedUpdate()
    {
        // Aplicar movimiento basado en la entrada
        MovimientoHandler();

        // Aplicar salto si el jugador quiere saltar
        if (quiereSaltar)
        {
            AplicarSalto(fuerzaSalto);
            quiereSaltar = false;  // Resetea el estado de salto
        }
    }

    // Handler para el manejo de movimiento
    void MovimientoHandler()
    {
        movimiento = new Vector2(moveInput * velocidadActual, rb.velocity.y);
        rb.velocity = movimiento;
    }

    // Handler para el manejo de la rotación
    void RotacionHandler()
    {
        if (movimiento.x < 0) transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (movimiento.x > 0) transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    // Detección de espacios en torno al lobo (Jugador)
    void PisoChecker()
    {
        Vector2 posicion = transform.position;
        Vector2 esquinaIzquierda = posicion - new Vector2(GetComponent<BoxCollider2D>().size.x / 2, 0);
        Vector2 esquinaDerecha = posicion + new Vector2(GetComponent<BoxCollider2D>().size.x / 2, 0);

        RaycastHit2D hitCentro = Physics2D.Raycast(posicion, Vector2.down, groundCheckDistance);
        RaycastHit2D hitIzquierda = Physics2D.Raycast(esquinaIzquierda, Vector2.down, groundCheckDistance);
        RaycastHit2D hitDerecha = Physics2D.Raycast(esquinaDerecha, Vector2.down, groundCheckDistance);

        if (hitCentro.collider || hitIzquierda.collider || hitDerecha.collider) puedeSaltar = true;
        else puedeSaltar = false;
    }

    // Aplico salto sobre el lobo (Jugador)
    void AplicarSalto(float fuerzaSalto)
    {
        rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
    }
}
