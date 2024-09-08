using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salto : MonoBehaviour
{
    SpacialDetector detector;
    Animator animator;

    Rigidbody2D rb;
    [SerializeField] float fuerzaSalto = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void RealizarSalto()
    {
        if(detector.EstaEnElSuelo(0.1f, 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
            animator.SetBool("isJumping", true);
        }
    }
}
