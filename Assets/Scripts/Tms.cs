using System;
using Application;

namespace Application
{
  public class Tms : MedicalEquipment {
    public TmsStimulator stimulator;
    public Tms(UnitMeasure unitMeasure, double intensity,
      Pulse pulse, BrainZone brainZone, TmsStimulator stimulator) 
        : base(unitMeasure, intensity, pulse, brainZone) {
      this.stimulator = stimulator;
    }
  }
}