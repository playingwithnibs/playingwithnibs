using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

namespace Application {
    public class GameController : MonoBehaviour
    {

        private void Start()
        {
            Debug.Log(PlayerManager.getInstance().medicalReport.pathology.name + " " + PlayerManager.getInstance().medicalReport.pathology.position);
            PlayerManager.getInstance().startTime = PlayerManager.getInstance().getCurrentTimestampInSeconds();            
        }

        public void onTdcsSelected()
        {
            PlayerManager.getInstance().medicalEquipment = new Tdcs();
            iniziaFaseDue();
        }

        public void onTmsSelected()
        {
            PlayerManager.getInstance().medicalEquipment = new Tms();
            iniziaFaseDue();
        }

        public void iniziaFaseDue()
        {
            SceneManager.LoadScene(GAME_2, LoadSceneMode.Single);
        }

        public void exitSession()
        {
            // TODO: void the simulation session
            SceneManager.LoadScene(GAME_SELECTION, LoadSceneMode.Single);
        }
    }
}
