using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SceneManagement : MonoBehaviour
{
    PlayerActionsScript playerActions;

    private void Awake()
    {
        playerActions = new PlayerActionsScript();

        playerActions.MainMenu.Enable();

        playerActions.MainMenu.Play.performed += StartGame;
        playerActions.MainMenu.Quit.performed += QuitGame;
    }

    private void StartGame(InputAction.CallbackContext context)
    {
        playerActions.MainMenu.Disable();
        SceneManager.LoadScene(1);
    }

    private void QuitGame(InputAction.CallbackContext context)
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

}
