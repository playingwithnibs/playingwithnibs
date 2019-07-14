namespace Application
{
  public class Tms : MedicalEquipment {
    public TmsStimulator stimulator;

    public static float min = 70;

    public static float max = 120;

    // public Tms(UnitMeasure unitMeasure, double intensity, Pulse pulse,
    //   BrainZonesArray brainZones, TmsStimulator stimulator, 
    //   StimulationType stimulationType) 
    //     : base(unitMeasure, intensity, pulse, brainZones, stimulationType) {
    //   this.stimulator = stimulator;
    // }

    public Tms() : base() {
        name = "TMS";
    }

    public override string ToString() {
      return "TMS" + "\nUnit measure: " + unitMeasure +
        "\nIntensity value: " + intensity +
        "\nPulse: " + pulse + "\nStimulator" + stimulator;
    }
  }
}