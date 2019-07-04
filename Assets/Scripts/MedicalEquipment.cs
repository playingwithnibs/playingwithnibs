using System;
using Application;

namespace Application
{
  public class MedicalEquipment {
    public UnitMeasure unitMeasure; 
    public double intensity;
    public Pulse pulse; 
    public BrainZone brainZone;

    public MedicalEquipment(UnitMeasure unitMeasure, double intensity, 
      Pulse pulse, BrainZone brainZone) {
      this.unitMeasure = unitMeasure;
      this.intensity = intensity;
      this.pulse = pulse;
      this.brainZone = brainZone;
    }
  }
}