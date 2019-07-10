using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using static Constants;

namespace Application {
    public class GameController : MonoBehaviour
    {
        //private string macchinario;
        //private string voltaggio;
        public GameObject macchina1;
        public GameObject macchina2;
        public GameObject parametro1;
        public GameObject parametro2;
        public GameObject sliderA;
        public GameObject sliderP;

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
            macchina1.SetActive(false);
            macchina2.SetActive(false);
            parametro1.SetActive(true);
            parametro2.SetActive(true);
        }

        public void scegliVoltaggioA()
        {
            PlayerManager.getInstance().medicalEquipment.unitMeasure = 
                UnitMeasure.MILLIAMPERE;
            sliderA.SetActive(true);
            sliderP.SetActive(false);     
        }

        public void scegliVoltaggioP()
        {
            PlayerManager.getInstance().medicalEquipment.unitMeasure =
                UnitMeasure.PERCENTAGE_OF_MT;
            sliderA.SetActive(false);
            sliderP.SetActive(true);
        }

        public void exitSession()
        {
            SceneManager.LoadScene(GAME_SELECTION, LoadSceneMode.Single);
            Debug.Log("exit");
        }

        private void Awake()
        {
            Debug.Log(PlayerManager.getInstance().pathology.name);
        }

    }
}
