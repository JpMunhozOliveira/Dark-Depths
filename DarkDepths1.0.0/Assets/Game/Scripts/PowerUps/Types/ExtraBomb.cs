using UnityEngine;

public class ExtraBomb : PowerUps, IPowerUp
{
    [SerializeField]
    bool Debuff = false;
    [SerializeField]
    bool Gold = false;

    public void SetUpgrade(PlayerController player)
    {
        AudioController.Instance.PlaySound(collectClip);

        if (Debuff && player.BombsAllowed > 1)
        {
            player.BombsAllowed--;
        }
        else if (Gold)
        {
            for (int i = 5; i > 0; i--)
            {
                if (player.BombsAllowed < 9)
                    player.BombsAllowed++;
            }
        }
        else
        {
            if (!Debuff && player.BombsAllowed < 9)
                player.BombsAllowed++;
        }
    }
}
