using System;
using Application;
using System.Collections.Generic;

namespace Application
{
  public class Tms : MedicalEquipment {
    public TmsStimulator stimulator;

    public double min = 0;

    public double max = 180;

    public Tms(UnitMeasure unitMeasure, double intensity, Pulse pulse,
      BrainZonesArray brainZones, TmsStimulator stimulator, 
      StimulationType stimulationType) 
        : base(unitMeasure, intensity, pulse, brainZones, stimulationType) {
      this.stimulator = stimulator;
    }

    public Tms() : base() { }

    public override string ToString() {
      return "TMS";
    }
  }
}