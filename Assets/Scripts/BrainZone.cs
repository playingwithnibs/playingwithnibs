using System;

namespace Application
{
  public class BrainZone {
    public BrainZoneNames brainZoneName;
    public Position position;
    public Stimulator stimulator;
    public int stimulatorType;

    public BrainZone(BrainZoneNames brainZoneName, Position position, Stimulator stimulator) : this(brainZoneName, position) {
        this.stimulator = stimulator;
    }

    public BrainZone(BrainZoneNames brainZoneName, Position position)
    {
        this.brainZoneName = brainZoneName;
        this.position = position; 
        stimulator = new Stimulator();
    }

        public override int GetHashCode() { 
      return (int)brainZoneName;
    }
    
    public override bool Equals(object obj) { 
      if (!(obj is BrainZone) || obj == null) 
        return false;
      
      BrainZone bz = (BrainZone)obj;

      return bz.brainZoneName.Equals(this.brainZoneName);
    }

    public override string ToString()
    {
        return brainZoneName + " [" + position + "]: (stimulator: " + stimulator.electrodeType + "), (type: " + stimulatorType + ")";
    }

        public bool isActive() {
      return stimulator.electrodeType != ElectrodeType.NO;
    }

    public void applicate(Stimulator stimulator, int stimulatorType)
        {
            this.stimulator = stimulator;
            this.stimulator.tapCounter++;
            this.stimulatorType = stimulatorType;
        }
  }

}