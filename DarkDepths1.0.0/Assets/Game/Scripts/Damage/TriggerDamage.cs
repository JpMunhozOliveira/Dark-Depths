using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage();
        }
    }
}