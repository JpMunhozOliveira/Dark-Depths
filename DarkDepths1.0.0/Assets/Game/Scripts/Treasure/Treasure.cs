using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour, IInteractable {

    public int ID;
    public new string name;
    [TextArea]
    public string description;

    public void Interaction(PlayerController player)
    {
        player.hud.CollectTreasure(GetComponent<SpriteRenderer>().sprite);
        AudioController.Instance.PlaySound(player.SfxVictory);
    }
}