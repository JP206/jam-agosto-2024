using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [SerializeField] ControladorLoba controladorLoba;
    // Update is called once per frame
    void Update()
    {
        DetectarSalto();
        controladorLoba.MovimientoX(DetectarMovimientoHorizontal());
    }
    float DetectarMovimientoHorizontal()
    {
        //GetAxisRaw() Genera el "Smooth" en el movimiento del personaje 
        return Input.GetAxisRaw("Horizontal");
    }

    void DetectarSalto()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            controladorLoba.EnSalto();
        }
    }
}
