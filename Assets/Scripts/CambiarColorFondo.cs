using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarColorFondo : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CambiarFondoVioleta()
    {
        animator.SetTrigger("violeta");
    }

    public void CambiarFondoBlanco()
    {
        animator.SetTrigger("blanco");
    }
}
