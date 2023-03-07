using UnityEngine;

public class ExtraSpeed : PowerUps, IPowerUp
{
    [SerializeField]
    bool Debuff = false;
    [SerializeField]
    bool Gold = false;

    public void SetUpgrade(PlayerController player)
    {
        AudioController.Instance.PlaySound(collectClip);

        if (Debuff && player.Speed > 150)
        {
            player.Speed -= 5;
        }
        else if (Gold)
        {
            for (int i = 5; i > 0; i--)
            {
                if (player.Speed < 250)
                    player.Speed += 5;
            }
        }
        else
        {
            if (!Debuff && player.Speed < 250)
                player.Speed += 5;
        }
    }
}
