using System;
using Application;
using System.Collections.Generic;

namespace Application
{
  public class Tdcs : MedicalEquipment {
    public TdcsStimulator stimulator;

    public double min = 0;

    public double max = 2;
    
    public Tdcs(UnitMeasure unitMeasure, double intensity, Pulse pulse,
      BrainZonesArray brainZones, TdcsStimulator stimulator,
      StimulationType stimulationType) 
        : base(unitMeasure, intensity, pulse, brainZones, stimulationType) {
      this.stimulator = stimulator;
    }

    public Tdcs() : base() {}

    public override string ToString() {
      return "tDCS";
    }
  }
}