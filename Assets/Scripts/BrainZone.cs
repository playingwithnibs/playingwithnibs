using System;

namespace Application
{
  public class BrainZone {
    public BrainZoneNames brainZoneName;
    public Position position;
    public Stimulator stimulator;
    public BrainZone(BrainZoneNames brainZoneName, Position position, 
      Stimulator stimulator) {
      this.brainZoneName = brainZoneName;
      this.position = position;
      this.stimulator = stimulator;
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

    public bool isActive() {
      return stimulator.electrodeType != ElectrodeType.NO;
    }

    public void applicate(Stimulator stimulator, Position position)
        {
            this.stimulator = stimulator;
            this.position = position;
            this.stimulator.tapCounter++;
        }
  }

}
