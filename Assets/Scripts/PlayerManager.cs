﻿using UnityEngine;
using Application;
using System;
using System.Collections.Generic;
using System.Timers;

public class PlayerManager { 

    private const int TIME_BONUS = 20;
    private const int SAVED_TIME_BONUS_MOLTIPLICATOR = 5;
    private const int DEVICE_CONFIG_SCORE_PERCENTAGE = 40;

    public float time = 121;

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
        return endTime - startTime - TIME_BONUS;
    }

    public string computePatientFace()
    {
        return "Sprites/" + ((int)medicalReport.name).ToString() + "_" +
          outcome.ToString().ToLower();
    }

    public double getTotalScore()
    {
        return computeMedicalEquipmentScore() + computeOutcomeScore() - computeTimeBonus();
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

    public string getQualitativeScore() {
        switch(outcome) {
            case (Outcome.VERY_BAD):
                return "Very bad";
            case (Outcome.BAD):
                return "Bad";
            case (Outcome.EXPLOSION):
              return "EXPLOSION!";
            case (Outcome.GOOD):
              return "Good";
            case (Outcome.UNCHANGED):
              return "Neutral";
            case (Outcome.VERY_GOOD):
              return "Very good";
            default:
                return "Neutral";
        }
    }

    public string getQualitativeTimeScore() {
        double elapsedTime = computeTimeBonus();

        if (elapsedTime <= 0)
            return "Very good";
        else if (elapsedTime > 0 && elapsedTime < 20)
            return "Good";
        else if (elapsedTime >= 20 && elapsedTime < 40)
            return "Neutral";
        else if (elapsedTime >= 40 && elapsedTime < 60)
            return "Bad";
        else if (elapsedTime >= 60 && elapsedTime < 80)
            return "Very bad";
        else
            return "A TRAGEDY!";
    }
}
