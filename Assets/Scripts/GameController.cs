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
        private AudioSource audioSource;
        private float audioTimer;

        IEnumerator LoadLevelTimer(float audioTimer, string name)
        {
            yield return new WaitForSeconds(audioTimer);
            SceneManager.LoadScene(name, LoadSceneMode.Single);
        }

        private void Start()
        {
            audioSource = GameObject.Find("sound-effects").GetComponent<AudioSource>();
            audioTimer = audioSource.clip.length;
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
            StartCoroutine(LoadLevelTimer(audioTimer, GAME_2));
        }

        public void exitSession()
        {
            // TODO: void the simulation session
             StartCoroutine(LoadLevelTimer(audioTimer, GAME_SELECTION));
        }
        
    }
}
