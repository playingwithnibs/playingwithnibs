using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Constants;

public class PathologyController : MonoBehaviour
{
    public void onBack()
    {
        SceneManager.LoadScene(GAME_SELECTION, LoadSceneMode.Single);
    }

    public void setPathology() {
        
    }
}
