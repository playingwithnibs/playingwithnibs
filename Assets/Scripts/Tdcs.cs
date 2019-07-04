using System;
using Application;

namespace Application
{
  public class Tdcs : MedicalEquipment {
    public TdcsStimulator stimulator;
    public Tdcs(UnitMeasure unitMeasure, double intensity,
      Pulse pulse, BrainZone brainZone, TdcsStimulator stimulator) 
        : base(unitMeasure, intensity, pulse, brainZone) {
      this.stimulator = stimulator;
    }
  }
}