﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Constants;

public class MenuController : MonoBehaviour
{
    public void quitButtonPressed() => Application.Quit();

    public void onPlayButton()
    {
        SceneManager.LoadScene(GAME_SELECTION, LoadSceneMode.Single); 
    }       
    
    public void openMenu()                       
    {
        SceneManager.LoadScene(MENU, LoadSceneMode.Single);
    }

    public void openPathologySelection()
    {
        SceneManager.LoadScene(PATHOLOGIES, LoadSceneMode.Single);
    }

    public void onRandomButton()
    {
        SceneManager.LoadScene(GAME_1, LoadSceneMode.Single);
    }
}