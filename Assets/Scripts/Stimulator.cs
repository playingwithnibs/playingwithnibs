namespace Application
{
  public class Stimulator {   
    public ElectrodeType electrodeType;

    public ElectrodeName electrodeName;
    
    public int tapCounter;

    public Stimulator() {
      electrodeName = ElectrodeName.NO;
      electrodeType = ElectrodeType.NO;
      tapCounter = 0;
    }

    public Stimulator(ElectrodeType electrodeType) {
      this.electrodeType = electrodeType;
      tapCounter = (int)electrodeType;
    }

    public Stimulator(ElectrodeType electrodeType, 
      ElectrodeName electrodeName) {
        this.electrodeType = electrodeType;
        this.electrodeName = electrodeName;
    }

        public ElectrodeType tap(int states)
        {
            tapCounter = ((int) ++electrodeType % states);
            return electrodeType;
        }

    public override string ToString() {
      return electrodeName.ToString() + " " + electrodeType.ToString();
    }
  } 
}
