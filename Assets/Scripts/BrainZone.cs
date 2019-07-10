using System;

namespace Application
{
  public class BrainZone {
    public BrainZoneNames brainZoneName;
    public Position position;
    public ElectrodeType electrodeType;
    public BrainZone(BrainZoneNames brainZoneName, Position position, 
      ElectrodeType electrodeType) {
      this.brainZoneName = brainZoneName;
      this.position = position;
      this.electrodeType = electrodeType;
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

    public Boolean isActive() {
      return electrodeType != ElectrodeType.NO;
    }
  }

}
