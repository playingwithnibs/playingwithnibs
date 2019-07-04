using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


// Quits the player when the user hits escape

public class MenuController : MonoBehaviour
{
    public void quitButtonPressed() => Application.Quit();


    public void onPlayButton()
    {
        Debug.Log("Hello");
        Console.Write("ciao");
        SceneManager.LoadScene("GameSelection", LoadSceneMode.Single);
        //SceneManager.UnloadSceneAsync("Menu");     
    }                                                
    public void onBackButton()                       
    {   
    Debug.Log("Hello");
        Console.Write("ciao");
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        //SceneManager.UnloadSceneAsync("GameSelection");
    }

    public void onSelectButton()
    {
        SceneManager.LoadSceneAsync("Patologie", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("GameSelection");

    }

    public void onRandomButton()
    {
        SceneManager.LoadSceneAsync("Gioco", LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync("GameSelection");

    }
}
