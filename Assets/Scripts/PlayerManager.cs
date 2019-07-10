﻿using UnityEngine;
using Application;
using System;

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

    public BrainZonesArray brainZones;

    public MedicalEquipment medicalEquipment;

    public StimulationType stimulationType;

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
        }
        return Instance;
    }
}
