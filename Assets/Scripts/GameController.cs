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

        PlayerManager pm = PlayerManager.getInstance();

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
            
            pm.time -= Time.deltaTime;

            if (pm.time <= 0)
            {
                GameObject
                    .Find("BonusText")
                    .GetComponent<Text>()
                    .text 
                        = "Malus:\n" + 
                            ((int)pm.time).ToString() +
                            " pts.";

                GameObject
                    .Find("BonusText")
                    .GetComponent<Text>()
                    .color = new Color(1f, 0.13f, 0f, 1f);
            }
            else
            {
                GameObject
                    .Find("BonusText")
                    .GetComponent<Text>()
                    .text 
                        = "Bonus\n" + 
                            ((int)pm.time).ToString() + 
                            " pts. ";
                
                GameObject
                    .Find("BonusText")
                    .GetComponent<Text>()
                    .color = new Color(0.5647059f, 1f, 0.48f, 1f);
            }
        
        }

        private void Update() {
        pm.time -= Time.deltaTime;

        if (pm.time <= 0)
        {
            GameObject
                .Find("BonusText")
                .GetComponent<Text>()
                .text
                    = "Malus:\n" +
                        ((int)pm.time).ToString() +
                        " pts.";

            GameObject
                .Find("BonusText")
                .GetComponent<Text>()
                .color = new Color(1f, 0.13f, 0f, 1f);
        }
        else
        {
            GameObject
                .Find("BonusText")
                .GetComponent<Text>()
                .text
                    = "Bonus\n" +
                        ((int)pm.time).ToString() +
                        " pts. ";

            GameObject
                .Find("BonusText")
                .GetComponent<Text>()
                .color = new Color(0.5647059f, 1f, 0.48f, 1f);
        }
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
