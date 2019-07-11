namespace Application
{
  public class BrainZonesArray {
    public BrainZone[] brainZones;

    public  int countActiveZones;

    public BrainZonesArray() {
      brainZones = new BrainZone[6];
      
      brainZones[0] = 
        new BrainZone(BrainZoneNames.DLPFC, Position.UPPER, ElectrodeType.NO);
      brainZones[1] = 
        new BrainZone(BrainZoneNames.M1, Position.UPPER, ElectrodeType.NO);
      brainZones[2] = 
        new BrainZone(BrainZoneNames.SO, Position.UPPER, ElectrodeType.NO);
      brainZones[3] = 
        new BrainZone(BrainZoneNames.O, Position.UPPER, ElectrodeType.NO);
      brainZones[4] = 
        new BrainZone(BrainZoneNames.CP5, Position.LEFT, ElectrodeType.NO);
      brainZones[5] = 
        new BrainZone(BrainZoneNames.CP6, Position.RIGHT, ElectrodeType.NO);

      countActiveZones = 0;
    }

    // tested
    public void activateZone(BrainZoneNames brainZoneName, 
      ElectrodeType electrodeType) {
        if (electrodeType != ElectrodeType.NO) {
          brainZones[(int)brainZoneName].electrodeType = electrodeType;

          countActiveZones++;
        }
    }

    // tested
    public void deactivateZone(BrainZoneNames brainZoneName) {
      brainZones[(int)brainZoneName].electrodeType = ElectrodeType.NO;

      countActiveZones--;
    }
  }
}