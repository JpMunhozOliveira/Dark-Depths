using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FireRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
        animator.enabled = true;    
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        animator.enabled = false;
    }
}
