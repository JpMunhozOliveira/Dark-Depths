using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MenuController
{
    [SerializeField]
    private GameObject mainMenuContainer = null;
    [SerializeField]
    private GameObject gameDataExist = null;
    [SerializeField]
    private Button loadButton = null;

    public void StartNewGame()
    {
        GoToScene("LobbyGame");
    }

    public void NewGameDialogue()
    {

        mainMenuContainer.SetActive(false);
        gameDataExist.SetActive(true);

        StartNewGame();
        
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
