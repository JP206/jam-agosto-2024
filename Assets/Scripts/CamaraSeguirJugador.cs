using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSeguirJugador : MonoBehaviour
{
    GameObject jugador;

    void Start()
    {
        jugador = GameObject.FindWithTag("Player");        
    }

    void LateUpdate()
    {
        transform.position = new Vector3(jugador.transform.position.x, jugador.transform.position.y, -10);
    }
}
