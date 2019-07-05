using System;
using Application;
using System.Collections.Generic;

namespace Application
{
  public class BrainZonesArray {
    public static BrainZone[] brainZones = {
      new BrainZone(BrainZoneNames.DLPFC, Position.UPPER, ElectrodeType.NO),
      new BrainZone(BrainZoneNames.M1, Position.UPPER, ElectrodeType.NO),
      new BrainZone(BrainZoneNames.SO, Position.UPPER, ElectrodeType.NO),
      new BrainZone(BrainZoneNames.O, Position.UPPER, ElectrodeType.NO),
      new BrainZone(BrainZoneNames.CP5, Position.LEFT, ElectrodeType.NO),
      new BrainZone(BrainZoneNames.CP6, Position.RIGHT, ElectrodeType.NO),
    };

    public static int countActiveZones = 0;

    public static void activateZone(BrainZoneNames brainZoneName, 
      ElectrodeType electrodeType) {
        brainZones[(int)brainZoneName].electrodeType = electrodeType;
        
        countActiveZones++;
    }

    public static void deactivateZone(BrainZoneNames brainZoneName) {
      brainZones[(int)brainZoneName].electrodeType = electrodeType.NO;

      countActiveZones--;
    }
  }
}