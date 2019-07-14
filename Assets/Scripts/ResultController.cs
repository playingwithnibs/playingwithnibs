using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
    private PlayerManager pm;

    private Text
        deviceConfigurationText,
        theoryApplicationText,
        timeMalusText,
        totalText,
        outcomeText,
        qualitativeDeviceText,
        qualitativeTheoryText,
        qualitativeTimeText;

    private Color green;
    private Color red;
    private Color grey;

    // Start is called before the first frame update
    void Start()
    {
        pm = PlayerManager.getInstance();
        initUI();

        green = new Color(0.5647059f, 1f, 0.48f, 1f);
        red = new Color(1f, 0.13f, 0f, 1f);
        grey = new Color(0.3396f, 0.3396f, 0.3396f, 1f);

        deviceConfigurationText.text = pm.computeMedicalEquipmentScore().ToString("f0");
        theoryApplicationText.text = pm.computeOutcomeScore().ToString("f0");
        timeMalusText.text = pm.computeTimeBonus().ToString("f0");
        totalText.text = pm.getTotalScore().ToString("f0");
        outcomeText.text = pm.outcome.ToString().Replace("_", " ");

        qualitativeDeviceText.text = pm.getQualitativeScore();
        qualitativeTheoryText.text = pm.getQualitativeScore();
        qualitativeTimeText.text = pm.getQualitativeTimeScore();

        qualitativeDeviceText.color = computeColor(qualitativeDeviceText.text);
        qualitativeTheoryText.color = computeColor(qualitativeTheoryText.text);
        qualitativeTimeText.color = computeColor(qualitativeTimeText.text);
    }

    private Color computeColor(string outcome)
    {
        if (outcome.Equals("Very good") || outcome.Equals("Good")) return green;
        if (outcome.Equals("Neutral")) return grey;
        return red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initUI()
    {
        deviceConfigurationText = GameObject.Find("device-configuration").GetComponent<Text>();
        theoryApplicationText = GameObject.Find("theory-application").GetComponent<Text>();
        timeMalusText = GameObject.Find("time-malus").GetComponent<Text>();
        totalText = GameObject.Find("total").GetComponent<Text>();
        outcomeText = GameObject.Find("outcome").GetComponent<Text>();

        qualitativeDeviceText = GameObject.Find("qd").GetComponent<Text>();
        qualitativeTheoryText = GameObject.Find("qt").GetComponent<Text>();
        qualitativeTimeText = GameObject.Find("qb").GetComponent<Text>();
    }
}
