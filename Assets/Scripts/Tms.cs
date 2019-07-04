using System;
using Application;
using System.Collections.Generic;

namespace Application
{
  public class Tms : MedicalEquipment {
    public TmsStimulator stimulator;
    public Tms(UnitMeasure unitMeasure, double intensity,
      Pulse pulse, HashSet<BrainZone> brainZones, TmsStimulator stimulator) 
        : base(unitMeasure, intensity, pulse, brainZones) {
      this.stimulator = stimulator;
    }
  }
}