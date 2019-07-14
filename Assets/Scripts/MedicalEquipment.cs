namespace Application {
  public class MedicalEquipment {
    public string name;

    public UnitMeasure unitMeasure;

    public double intensity;

    public Pulse pulse; 

    public StimulationType stimulationType;

    public MedicalEquipment(UnitMeasure unitMeasure, double intensity, 
      Pulse pulse, BrainZonesArray brainZones,
      StimulationType stimulationType) {
        this.unitMeasure = unitMeasure;
        this.intensity = intensity;
        this.pulse = pulse;
        this.stimulationType = stimulationType;
    }

    public bool usesMt() {
      return unitMeasure == UnitMeasure.PERCENTAGE_OF_MT;
    }

    public bool usesMa() {
      return unitMeasure == UnitMeasure.MILLIAMPERE;
    }

    public bool hasUnitMeasure() {
      return unitMeasure != UnitMeasure.NO;
    }

    public bool isHighPulse() {
      return pulse == Pulse.HIGH;
    }

    public bool isLowPulse() {
      return pulse == Pulse.LOW;
    }

    public bool isSinglePulse() {
      return pulse == Pulse.SINGLE;
    }

    public bool hasPulse() {
      return pulse != Pulse.NO;
    }



    public MedicalEquipment() {}

    public override string ToString(){
      return "Unit measure: " + unitMeasure +
        "\nIntensity value: " + intensity + 
        "\nPulse: " + pulse;
    }
  }
}