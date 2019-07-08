using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Application;
using System;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public Application.Pathology pathology;
    public double startTime; 

    public double endTime;

    public Outcome outcome;

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


    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
