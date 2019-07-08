using System;
using Application;
using System.Collections.Generic;

namespace Application
{
  public class Tdcs : MedicalEquipment {
    public TdcsStimulator stimulator;
    public Tdcs(UnitMeasure unitMeasure, double intensity, Pulse pulse, 
      BrainZonesArray brainZones, TdcsStimulator stimulator,
      StimulationType stimulationType) 
        : base(unitMeasure, intensity, pulse, brainZones, stimulationType) {
      this.stimulator = stimulator;
    }
  }
}