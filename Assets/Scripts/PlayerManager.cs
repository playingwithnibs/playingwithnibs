using UnityEngine;
using Application;
using System;
using System.Collections.Generic;

public class PlayerManager { 

    private const int TIME_BONUS = 20;
    private const int SAVED_TIME_BONUS_MOLTIPLICATOR = 5;
    private const int DEVICE_CONFIG_SCORE_PERCENTAGE = 40;

    public static PlayerManager Instance;

    public Pathology pathology;
    
    public double startTime; 

    public double endTime;

    public Outcome outcome;

    public UnitMeasure unitMeasure;

    public double intensity;

    public Pulse pulse;

    public Brain brain;

    public MedicalEquipment medicalEquipment;

    public StimulationType stimulationType;

    public MedicalReport medicalReport;

    public ScoreCalculator sc;

    public double getCurrentTimestampInSeconds() 
    {
        return (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1))
            .TotalSeconds;
    }

    public double computeMedicalEquipmentScore()
    {
        return (int)outcome * DEVICE_CONFIG_SCORE_PERCENTAGE / 100;
    }

    public double computeOutcomeScore()
    {
        return (int)outcome;
    }

    public double computeTimeBonus()
    {
        return TIME_BONUS +
            (endTime - startTime);
    }

    public string computePatientFace()
    {
        return "Sprites/" + ((int)medicalReport.name).ToString() + "_" +
          outcome.ToString().ToLower();
    }

    public double getTotalScore()
    {
        return computeMedicalEquipmentScore()+ computeOutcomeScore() + computeTimeBonus();
    }

    public static PlayerManager getInstance()
    {
        if (Instance == null)
        {
            Instance = new PlayerManager();
            Instance.medicalReport = new MedicalReport();
        }
        return Instance;
    }

    public MedicalEquipment buildMedicalEquipment() {
        medicalEquipment.unitMeasure = unitMeasure;

        medicalEquipment.intensity = intensity;

        medicalEquipment.pulse = pulse;

        return medicalEquipment;
    }
}
