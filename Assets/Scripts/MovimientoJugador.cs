using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float velocidad, fuerzaSalto;
    Vector2 movimiento;
    bool puedeSaltar = true;
    float bufferCheckDistance = 0.3f, groundCheckDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheckDistance = GetComponent<BoxCollider2D>().size.y / 2 + bufferCheckDistance;
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        movimiento = new Vector2(moveInput * velocidad, rb.velocity.y);

        if (moveInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && puedeSaltar)
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            puedeSaltar = false;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance);
        if (hit.collider)
        {
            puedeSaltar = true;
        }
        else
        {
            puedeSaltar = false;
        }
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red);
    }

    void FixedUpdate()
    {
        rb.velocity = movimiento;
    }
}
