namespace Application
{
  public class Tdcs : MedicalEquipment {
    public TdcsStimulator stimulator;

    public static float min = 0;

    public static float max = 10;
    
    // public Tdcs(UnitMeasure unitMeasure, double intensity, Pulse pulse,
    //   BrainZonesArray brainZones, TdcsStimulator stimulator,
    //   StimulationType stimulationType) 
    //     : base(unitMeasure, intensity, pulse, brainZones, stimulationType) {
    //   this.stimulator = stimulator;
    // }

    public Tdcs() : base() {}

    public override string ToString() {
      return "tDCS" + "\nUnit measure: " + unitMeasure +
        "\nIntensity value: " + intensity +
        "\nPulse: " + pulse + "\nStimulator" + stimulator; 
    }
  }
}