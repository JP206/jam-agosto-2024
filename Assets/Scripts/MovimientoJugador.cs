using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float velocidad;
    Vector2 movimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        movimiento = new Vector2(moveInput * velocidad, rb.velocity.y);
    }

    void FixedUpdate()
    {
        rb.velocity = movimiento;
    }
}
