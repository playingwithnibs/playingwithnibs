using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

namespace Application
{
    public class MedicalReportController : MonoBehaviour

    {
        private MedicalReport mr;

        private SpriteRenderer memoji;
        private Text
            name,
            sex,
            age,
            description,
            pathology;

        // Start is called before the first frame update
        void Start()
        {
            mr = PlayerManager.getInstance().medicalReport;
            initMedicalReportUI();
        }

        // Update is called once per frame
        void Update()
        {

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
