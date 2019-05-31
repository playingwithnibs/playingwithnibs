using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Quits the player when the user hits escape

public class MenuController : MonoBehaviour
{
    public void quitButtonPressed() => Application.Quit();


    public void onPlayButton()
    {
        SceneManager.LoadScene("GameSelection", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Menu");     
    }                                                
    public void onBackButton()                       
    {                                                
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("GameSelection");
    }
}
