using System;

namespace Application
{
  public class BrainZone {
    public BrainZoneNames brainZoneName;
    public Position position;
    public Stimulator stimulator;

    // public BrainZone(BrainZoneNames brainZoneName, Position position, Stimulator stimulator) : this(brainZoneName, position) {
    //     this.stimulator = stimulator;
    // }

    public BrainZone(BrainZoneNames brainZoneName, Position position)
    {
        this.brainZoneName = brainZoneName;
        this.position = position; 
        stimulator = new Stimulator();
    }

    public override int GetHashCode() { 
      return (int)brainZoneName + (int)position;
    }

    public bool Equals(BrainZoneNames name, Position pos) {
      return Equals(new BrainZone(name, pos));
    }
    
    public override bool Equals(object obj) { 
      if (!(obj is BrainZone) || obj == null) 
        return false;
      
      BrainZone bz = (BrainZone)obj;

      return bz.brainZoneName == brainZoneName && bz.position == position;
    }

    public override string ToString()
    {
        return brainZoneName + " [" + position + "]: (stimulator: " + 
          stimulator.electrodeType + "), (type: " + 
          stimulator.electrodeName + ")";
    }

    public bool isActive() {
      return stimulator.electrodeName != ElectrodeName.NO;
    }

    public bool isActiveTmsEightCoil() {
      return stimulator.electrodeName == ElectrodeName.EIGHT;
    }

    public bool isActiveTmsCircularCoil() {
      return stimulator.electrodeName == ElectrodeName.CIRCULAR;
    }
  }
}