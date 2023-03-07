using UnityEngine;

public class Dead : PowerUps, IPowerUp
{
    public void SetUpgrade(PlayerController player)
    {
        AudioController.Instance.PlaySound(collectClip);
        player.Health = 0;
    }
}
