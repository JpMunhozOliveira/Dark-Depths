using UnityEngine;

public class TriggerDamageEnemy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null && collision.tag != "Bomb" && collision.tag != "Enemy")
        {
            damageable.TakeDamage();
        }
    }
}