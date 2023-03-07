using UnityEngine;

public class SetTypeBomb : PowerUps, IPowerUp
{
    [SerializeField]
    int TypeBomb;
    public void SetUpgrade(PlayerController player)
    {
        AudioController.Instance.PlaySound(collectClip);

        player.TypeBomb = TypeBomb;
    }
}
