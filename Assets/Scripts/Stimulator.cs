namespace Application
{
  public class Stimulator {   
    public ElectrodeType electrodeType;

    public int tapCounter;

    public Stimulator() {
      electrodeType = ElectrodeType.NO;
      tapCounter = 0;
    }

    public Stimulator(ElectrodeType electrodeType) {
      this.electrodeType = electrodeType;
      tapCounter = (int)electrodeType;
    }

        public ElectrodeType tap(int states)
        {
            tapCounter = ((int) ++electrodeType % states);
            return electrodeType;
        }
  } 
}
