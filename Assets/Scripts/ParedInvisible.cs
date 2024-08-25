using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedInvisible : MonoBehaviour
{
    Transform cameraTransform;
    float maxLeftPositionX;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        maxLeftPositionX = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        transform.position = new Vector3(maxLeftPositionX, transform.position.y, transform.position.z);
    }

    void LateUpdate()
    {
        // Obtener la posición actual del borde izquierdo de la cámara
        float currentLeftPositionX = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;

        // Si la cámara ha avanzado hacia la derecha, actualizar la posición de la pared
        if (currentLeftPositionX > maxLeftPositionX)
        {
            maxLeftPositionX = currentLeftPositionX;
            transform.position = new Vector3(maxLeftPositionX, transform.position.y, transform.position.z);
        }
    }
}
