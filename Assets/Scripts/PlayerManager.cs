using UnityEngine;
using Application;
using System;
using System.Collections.Generic;

public class PlayerManager
{
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

    public double getCurrentTimestampInSeconds() 
    {
        return (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1))
            .TotalSeconds;
    } 

    public double computeScore()
    {
        ScoreCalculator sc = new ScoreCalculator();

        return sc.computeTimeBonus(startTime, endTime) + 
            sc.computeOutcomeScore(outcome) + 
            sc.computeMedicalEquipmentScore(outcome);
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
