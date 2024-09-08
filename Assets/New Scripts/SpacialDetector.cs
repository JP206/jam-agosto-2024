using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacialDetector : MonoBehaviour
{
    [SerializeField] float altoPersonaje;
    [SerializeField] float anchoPersonaje;
    [SerializeField] LayerMask layerDeColision;

    public bool EstaEnElSuelo(float minLookAhead, float lookAhead)
    {
        var totalLookAhead = Mathf.Abs(lookAhead) + minLookAhead;

        Debug.DrawRay(transform.position + new Vector3(0, altoPersonaje / 2), Vector2.down * (altoPersonaje + totalLookAhead), Color.black, 0.1f);

        return Physics2D.Raycast(transform.position + new Vector3(0, altoPersonaje / 2), Vector2.down, altoPersonaje + totalLookAhead, layerDeColision);
    }

    public bool HayTecho(float minLookAhead, float lookAhead)
    {
        var totalLookAhead = Mathf.Abs(lookAhead) + minLookAhead;

        Debug.DrawRay(transform.position, Vector2.up * (altoPersonaje / 2f + totalLookAhead), Color.red, 0.1f);

        return Physics2D.Raycast(transform.position, Vector2.up, altoPersonaje / 2f + totalLookAhead, layerDeColision);
    }

    public float PosicionYEnPiso()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 100f, layerDeColision).point.y + (altoPersonaje / 2f);
    }

}
