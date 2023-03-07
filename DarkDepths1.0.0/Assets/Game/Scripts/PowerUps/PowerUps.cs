using UnityEngine;

public class PowerUps : MonoBehaviour, IDamageable
{
    private float invincibilityTime = 5f;
    public AudioClip collectClip;
    public AudioClip destroyClip;

    private void Update()
    {
        if (invincibilityTime > 0)
            invincibilityTime -= Time.deltaTime;
    }

    public void TakeDamage()
    {
        if (invincibilityTime < 0) {
            AudioController.Instance.PlaySound(destroyClip);
            Destroy(gameObject);
        }
    }

    public void PlaySound()
    {
        AudioController.Instance.PlaySound(collectClip);
    }
}