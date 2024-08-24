using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float velocidad, fuerzaSalto;
    Vector2 movimiento;
    bool puedeSaltar = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
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
    }

    void FixedUpdate()
    {
        rb.velocity = movimiento;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("piso"))
        {
            puedeSaltar = true;
        }
    }
}
