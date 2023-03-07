using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorLevel : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string sceneToLoad;

    public void Interaction(PlayerController player)
    {
        GameManager.Instance.GetPlayerSettings(player);
        player.hud.GoToScene(sceneToLoad);
        Debug.Log("Passou nivel");
        GameManager.Instance.PassLevel();
    }
}
