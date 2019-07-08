using System;
using Application;
using System.Collections.Generic;

namespace Application
{
  public class BrainZonesArray {
    public BrainZone[] brainZones;

    public  int countActiveZones;

    public BrainZonesArray() {
      brainZones = new BrainZone[6];
      this.brainZones[0] = 
        new BrainZone(BrainZoneNames.DLPFC, Position.UPPER, ElectrodeType.NO);
      this.brainZones[1] = 
        new BrainZone(BrainZoneNames.M1, Position.UPPER, ElectrodeType.NO);
      this.brainZones[2] = 
        new BrainZone(BrainZoneNames.SO, Position.UPPER, ElectrodeType.NO);
      this.brainZones[3] = 
        new BrainZone(BrainZoneNames.O, Position.UPPER, ElectrodeType.NO);
      this.brainZones[4] = 
        new BrainZone(BrainZoneNames.CP5, Position.LEFT, ElectrodeType.NO);
      this.brainZones[5] = 
        new BrainZone(BrainZoneNames.CP6, Position.RIGHT, ElectrodeType.NO);

      this.countActiveZones = 0;
    }

    public void activateZone(BrainZoneNames brainZoneName, 
      ElectrodeType electrodeType) {
        brainZones[(int)brainZoneName].electrodeType = electrodeType;
        
        countActiveZones++;
    }

    public void deactivateZone(BrainZoneNames brainZoneName) {
      brainZones[(int)brainZoneName].electrodeType = ElectrodeType.NO;

      countActiveZones--;
    }
  }
}