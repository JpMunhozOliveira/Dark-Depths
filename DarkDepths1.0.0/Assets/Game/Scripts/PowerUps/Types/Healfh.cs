using UnityEngine;

public class Healfh : PowerUps, IPowerUp
{
    [SerializeField]
    bool Debuff = false;
    [SerializeField]
    bool Gold = false;

    public void SetUpgrade(PlayerController player)
    {
        AudioController.Instance.PlaySound(collectClip);

        if (Debuff){
            player.TakeDamage();
        }
        else if (Gold){
            for (int i = 5; i > 0; i--){
                if (player.Health < 10)
                    player.Health++;
            }
        }
        else{
            if (player.Health < 10)
                player.Health++;
        }
    }
}
