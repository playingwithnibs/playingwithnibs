namespace Application
{
  public class BrainZonesArray {
    public BrainZone[] brainZones;

    public  int countActiveZones;

    public BrainZonesArray() {
      brainZones = new BrainZone[6];
      Stimulator stimulator = new Stimulator();
      
      brainZones[0] = 
        new BrainZone(BrainZoneNames.DLPFC, Position.UPPER, stimulator);
      brainZones[1] = 
        new BrainZone(BrainZoneNames.M1, Position.UPPER, stimulator);
      brainZones[2] = 
        new BrainZone(BrainZoneNames.SO, Position.UPPER, stimulator);
      brainZones[3] = 
        new BrainZone(BrainZoneNames.O, Position.UPPER, stimulator);
      brainZones[4] = 
        new BrainZone(BrainZoneNames.CP5, Position.LEFT, stimulator);
      brainZones[5] = 
        new BrainZone(BrainZoneNames.CP6, Position.RIGHT, stimulator);

      countActiveZones = 0;
    }

    // tested
    public void activateZone(BrainZoneNames brainZoneName, 
      ElectrodeType electrodeType) {
        if (electrodeType != ElectrodeType.NO) {
          brainZones[(int)brainZoneName].stimulator.electrodeType 
            = electrodeType;

          countActiveZones++;
        }
    }

    public void activateZone(int brainZoneName,
      ElectrodeType electrodeType) {
      if (electrodeType != ElectrodeType.NO)
        activateZone((BrainZoneNames)brainZoneName, electrodeType);
    }

    // tested
    public void deactivateZone(BrainZoneNames brainZoneName) {
      brainZones[(int)brainZoneName].stimulator.electrodeType 
        = ElectrodeType.NO;

      countActiveZones--;
    }
  }
}