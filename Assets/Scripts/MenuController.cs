using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Constants;

// Quits the player when the user hits escape

public class MenuController : MonoBehaviour
{
    public void quitButtonPressed() => Application.Quit();

    public void onPlayButton()
    {
        SceneManager.LoadScene(GAME_SELECTION, LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync(MENU);     
    }                                                
    public void onBackButton()                       
    {   
        SceneManager.LoadScene(MENU, LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync(GAME_SELECTION);
    }

    public void onSelectButton()
    {
        SceneManager.LoadSceneAsync(MENU, LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync(GAME_SELECTION);
    }

    public void onRandomButton()
    {
        SceneManager.LoadSceneAsync(GAME, LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync(GAME_SELECTION);
    }
}
