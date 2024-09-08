using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salto : MonoBehaviour
{
    SpacialDetector detector;
    MovimientoLoba movmiento;
    Animator animator;

    [SerializeField] float fuerzaSalto;

    public void InitializeReferences(SpacialDetector _spacialDetector, Animator _anim, MovimientoLoba _movmiento)
    {
        detector = _spacialDetector;
        animator = _anim;
        movmiento = _movmiento;
    }

    public void RealizarSalto()
    {
        if(detector.EstaEnElSuelo(0.1f, 0))
        {
            movmiento.AplicarSalto(fuerzaSalto);

            animator.SetBool("isJumping", true);

        }
    }
}
