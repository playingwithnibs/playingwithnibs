using System;
using Application;
using System.Collections.Generic;

namespace Application
{
  public class Tms : MedicalEquipment {
    public TmsStimulator stimulator;

    public static float min = 70;
    public static float max = 120;

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