using UnityEngine;

public class BloqueMovible : MonoBehaviour
{
    Rigidbody2D rb;
    bool estaEnElSuelo = false;
    AudioSource audioSource;
    public AudioClip sonidoCaida;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("Rigidbody2D no se encontró en el objeto.");
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Suelo") || col.gameObject.layer == LayerMask.NameToLayer("Piso"))
        {
            float rotacionZ = transform.rotation.eulerAngles.z;

            if ((rotacionZ > 80 && rotacionZ < 100) || (rotacionZ > 260 && rotacionZ < 280))
            {
                estaEnElSuelo = true;
                rb.velocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
                audioSource.PlayOneShot(sonidoCaida);
            }
        }
    }

    void Update()
    {
        if (estaEnElSuelo)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    public bool EstaEnElSuelo()
    {
        return estaEnElSuelo;
    }
}
