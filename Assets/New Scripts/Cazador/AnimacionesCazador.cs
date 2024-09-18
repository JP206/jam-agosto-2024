using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesCazador : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Sprite idleSprite;
    [SerializeField] SpriteRenderer rendererCazador;
    
    public void Caminar()
    {
        animator.SetTrigger("caminar");
    }

    public void Disparar()
    {
        animator.SetTrigger("disparar");
    }

    public void Idle()
    {
        animator.SetTrigger("idle");
        rendererCazador.sprite = idleSprite;
    }
}
