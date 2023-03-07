using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class AudioControllerKeys
{
    public const string Menu = "StartMenu";
    public const string Lobby = "LobbyGame";
    public const string Dungeon = "Dungeon";
    public const string Treasure = "TreasureLevel";
}

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource soundSource;
    [SerializeField]
    private AudioClip[] musics;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        SwitchMusic();
    }
    private void OnLevelWasLoaded(int level)
    {
        SwitchMusic();
    }

    private void SwitchMusic()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case AudioControllerKeys.Menu:
                musicSource.clip = musics[0];
                goto default;
            case AudioControllerKeys.Lobby:
                musicSource.clip = musics[1];
                goto default;
            case AudioControllerKeys.Treasure:
                musicSource.clip = musics[2];
                goto default;
            case AudioControllerKeys.Dungeon:
                musicSource.clip = musics[3];
                goto default;
            default:
                musicSource.loop = true;
                musicSource.Play();
                break;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }
}
