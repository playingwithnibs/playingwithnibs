using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using static Constants;

namespace Application {
    public class GameController : MonoBehaviour
    {

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

        public void scegliVoltaggioA()
        {
            PlayerManager.getInstance().medicalEquipment.unitMeasure = 
                UnitMeasure.MILLIAMPERE;
        }

        public void scegliVoltaggioP()
        {
            PlayerManager.getInstance().medicalEquipment.unitMeasure =
                UnitMeasure.PERCENTAGE_OF_MT;
        }

        public void exitSession()
        {
            SceneManager.LoadScene(GAME_SELECTION, LoadSceneMode.Single);
            Debug.Log("exit");
        }

        private void Awake()
        {
            //Debug.Log(PlayerManager.getInstance().pathology.name);
        }

    }
}
