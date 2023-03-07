using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterAnimationKeys
{
    public const string Horizontal = "Horizontal";
    public const string Vertical = "Vertical";
    public const string Dead = "Dead";
}
public class CharacterAnimator : MonoBehaviour
{
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected CharacterController controller;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        if (controller.Health <= 0)
        {
            animator.SetTrigger(CharacterAnimationKeys.Dead);
        }
    }

    protected void FlipSprite(float movement)
    {
        if (movement > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
