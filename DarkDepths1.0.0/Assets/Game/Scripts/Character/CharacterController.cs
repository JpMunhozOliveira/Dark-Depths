using System;
using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField][Min(0)] 
    protected int health = 10;

    [Header("Invincibility")]
    [SerializeField][Range(0.01f, 5f)] 
    protected float timeInvincible = 1f;
    [SerializeField]
    protected bool invincible = false;

    protected SpriteRenderer spriteRenderer;

    [Header("Sound")]
    [SerializeField]
    private AudioClip HitSound;
    [SerializeField]
    private AudioClip DeadSound;

    public int Health { get => health; set => health = value; }

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage()
    {
        if (!invincible)
        {
            health--;
            if(health > 0)
            {
                StartCoroutine(InvincibilityTime());
            }
            else
            {
                AudioController.Instance.PlaySound(DeadSound);
            }
        }
    }
    IEnumerator InvincibilityTime()
    {
        invincible = true;
        AudioController.Instance.PlaySound(HitSound);
        StartCoroutine(Flash());
        yield return null;
    }
    IEnumerator Flash()
    {
        float forgetTargetTime = Time.time + timeInvincible;

        while (Time.time < forgetTargetTime)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        invincible = false;
        spriteRenderer.enabled = true;
        yield return null;
    }
}
