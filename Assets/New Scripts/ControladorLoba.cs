using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorLoba : MonoBehaviour
{
    [SerializeField] private MovimientoLoba movimientoLoba;
    [SerializeField] private Salto salto;
    [SerializeField] private SpacialDetector detector;
    [SerializeField] private Animator animator;


    void Start()
    {
        // Configuración de referencias en Jump y Movement
        movimientoLoba.InitializeReferences(detector, animator);
        salto.InitializeReferences(detector, animator, movimientoLoba);
    }

    public void InitializeReferences(MovimientoLoba _movimientoLoba, Salto _salto)
    {
        movimientoLoba = _movimientoLoba;
        salto = _salto;
    }

    public void MovimientoX(float valorX)
    {
        movimientoLoba.MovimientoX(valorX);
    }

    public void EnSalto()
    {
        salto.RealizarSalto();
    }
}
