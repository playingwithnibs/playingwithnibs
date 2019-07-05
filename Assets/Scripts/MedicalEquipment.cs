using System;
using Application;
using System.Collections.Generic;

namespace Application
{
  public class MedicalEquipment {
    public UnitMeasure unitMeasure; 
    public double intensity;
    public Pulse pulse; 
    public BrainZonesArray[] brainZones;

    public StimulationType stimulationType;

    public MedicalEquipment(UnitMeasure unitMeasure, double intensity, 
      Pulse pulse, BrainZonesArray[] brainZones, 
      StimulationType stimulationType) {
        this.unitMeasure = unitMeasure;
        this.intensity = intensity;
        this.pulse = pulse;
        this.brainZones = brainZones;
        this.stimulationType = stimulationType;
    }
  }
}