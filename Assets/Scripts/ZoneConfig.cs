using System.Collections;
using System.Collections.Generic;
using Application;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ZoneConfig
{
    public BrainZone brainZone;
    public int stimulator;
    public int stimulatorType;

    public ZoneConfig(BrainZone brainZone, int stimulator, int stimulatorType)
    {
        this.brainZone = brainZone;
        this.stimulator = stimulator;
        this.stimulatorType = stimulatorType;
    }

}