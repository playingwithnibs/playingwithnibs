using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Constants;

namespace Application {
    public class PathologyController : MonoBehaviour
    {
        public void onBack()
        {
            SceneManager.LoadScene(GAME_SELECTION, LoadSceneMode.Single);
        }

         public void setPathology(int pathology) {
            //PlayerManager.getInstance().pathology = new Pathology((PathologyName)pathology);
            Debug.Log("setPathology, pathologyName = " 
                + pathology + ", " + (PathologyName)pathology);
            
            PlayerManager.getInstance((PathologyName)pathology);
            
            SceneManager.LoadScene(GAME_1);
        }
    }
}
