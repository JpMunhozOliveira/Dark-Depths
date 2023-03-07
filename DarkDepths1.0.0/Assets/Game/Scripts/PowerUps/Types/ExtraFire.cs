using UnityEngine;

public class ExtraFire : PowerUps, IPowerUp
{
    [SerializeField]
    bool Debuff = false;
    [SerializeField]
    bool Gold = false;

    public void SetUpgrade(PlayerController player)
    {
        AudioController.Instance.PlaySound(collectClip);

        if (Debuff && player.FireLength > 1)
        {
            player.FireLength--;
        }
        else if (Gold)
        {
            for (int i = 5; i > 0; i--)
            {
                if (player.FireLength < 9)
                    player.FireLength++;
            }
        }
        else
        {
            if (!Debuff && player.FireLength < 9)
                player.FireLength++;
        }
    }
}
