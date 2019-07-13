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

        private MedicalReport mr;

        private SpriteRenderer memoji;
        private Text
            name,
            sex,
            age,
            description,
            pathology;

        private void Start()
        {
            PlayerManager.getInstance().medicalReport = new MedicalReport();
            mr = PlayerManager.getInstance().medicalReport;
            Debug.Log(PlayerManager.getInstance().medicalReport.pathology.name + " " + PlayerManager.getInstance().medicalReport.pathology.position);
            PlayerManager.getInstance().startTime = PlayerManager.getInstance().getCurrentTimestampInSeconds();

            initMedicalReportUI();
            
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

        private void initMedicalReportUI()
        {
            name = GameObject.Find("Medical report text/name").GetComponent<Text>();
            sex = GameObject.Find("Medical report text/sex").GetComponent<Text>();
            age = GameObject.Find("Medical report text/age").GetComponent<Text>();
            description = GameObject.Find("Medical report text/description").GetComponent<Text>();
            pathology = GameObject.Find("Medical report text/pathology").GetComponent<Text>();
            memoji = GameObject.Find("memoji").GetComponent<SpriteRenderer>();

            memoji.sprite = Resources.Load(mr.animojiPath, typeof(Sprite)) as Sprite;
            memoji.transform.localScale -= new Vector3(0.72381F, 0.72381F, 0);
            name.text = mr.name + " " + mr.surname;
            sex.text = "Sex: " + mr.gender;
            age.text = "Birth: " + mr.dateOfBirth.ToShortDateString();
            pathology.text = "Pathology:\n" + mr.pathology.getName();
        }
    }
}
