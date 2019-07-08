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
            PlayerManager.Instance.pathology = new Pathology((PathologyName)pathology);
            SceneManager.LoadScene(GAME_1);
        }
    }
}