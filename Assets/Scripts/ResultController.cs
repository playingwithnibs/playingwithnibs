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
        outcomeText;

    // Start is called before the first frame update
    void Start()
    {
        pm = PlayerManager.getInstance();
        initUI();

        deviceConfigurationText.text = pm.computeMedicalEquipmentScore().ToString("f0");
        theoryApplicationText.text = pm.computeOutcomeScore().ToString("f0");
        timeMalusText.text = pm.computeTimeBonus().ToString("f0");
        totalText.text = pm.getTotalScore().ToString("f0");
        outcomeText.text = pm.outcome.ToString().Replace("_", " ");
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
    }
}
