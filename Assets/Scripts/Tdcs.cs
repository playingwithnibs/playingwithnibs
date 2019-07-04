using System;
using Application;
using System.Collections.Generic;

namespace Application
{
  public class Tdcs : MedicalEquipment {
    public TdcsStimulator stimulator;
    public Tdcs(UnitMeasure unitMeasure, double intensity,
      Pulse pulse, HashSet<BrainZone> brainZones, TdcsStimulator stimulator) 
        : base(unitMeasure, intensity, pulse, brainZones) {
      this.stimulator = stimulator;
    }
  }
}