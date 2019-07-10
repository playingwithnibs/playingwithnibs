using System;
using Application;
using System.Collections.Generic;

namespace Application
{
  public class Tms : MedicalEquipment {
    public TmsStimulator stimulator;
    public Tms(UnitMeasure unitMeasure, double intensity, Pulse pulse,
      Application.BrainZonesArray brainZones, TmsStimulator stimulator, 
      StimulationType stimulationType) 
        : base(unitMeasure, intensity, pulse, brainZones, stimulationType) {
      this.stimulator = stimulator;
    }

    public Tms() : base () {
            name = Constants.TMS;
        }
  }
}