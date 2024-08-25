using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiedraCae : MonoBehaviour
{
    public AudioClip sonidoCaida;
    public AudioSource audioSource;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("piso"))
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            audioSource.PlayOneShot(sonidoCaida);
        }
    }
}
