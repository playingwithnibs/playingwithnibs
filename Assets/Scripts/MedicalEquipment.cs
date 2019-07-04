using System;
using Application;
using System.Collections.Generic;

namespace Application
{
  public class MedicalEquipment {
    public UnitMeasure unitMeasure; 
    public double intensity;
    public Pulse pulse; 
    public HashSet<BrainZone> brainZones;

    public MedicalEquipment(UnitMeasure unitMeasure, double intensity, 
      Pulse pulse, HashSet<BrainZone> brainZones) {
      this.unitMeasure = unitMeasure;
      this.intensity = intensity;
      this.pulse = pulse;
      this.brainZones = brainZones;
    }
  }
}