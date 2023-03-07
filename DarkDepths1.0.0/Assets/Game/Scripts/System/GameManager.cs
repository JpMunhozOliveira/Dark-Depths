using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Level { get => level; set => level = value; }
    public int World { get => world; set => world = value; }

    [SerializeField]
    PlayerSettings playerSettings;

    [SerializeField]
    private int level = 0;
    [SerializeField]
    private int world = 0;

    void Awake()
    {
        playerSettings = new PlayerSettings(5, 200, 0, 1, 1);

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }

    public void PassLevel()
    {
        level++;

        if(level >= 3)
        {
            world++;
            level = 1;
        }
    }

    public void ResetInfo()
    {
        level = 0;
        world = 0;
    }

    public void GetPlayerSettings(PlayerController player)
    {
        playerSettings.Health = player.Health;
        playerSettings.Speed = player.Speed;
        playerSettings.TypeBomb = player.TypeBomb;
        playerSettings.BombAllowed = player.BombsAllowed;
        playerSettings.FireLenght = player.FireLength;
    }
    public void SetPlayerSettings(PlayerController player)
    {
        player.Health = playerSettings.Health;
        player.Speed = playerSettings.Speed;
        player.TypeBomb = playerSettings.TypeBomb;
        player.BombsAllowed = playerSettings.BombAllowed;
        player.FireLength = playerSettings.FireLenght;
    }
}