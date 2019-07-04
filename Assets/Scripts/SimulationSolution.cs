using System;
using System.Collections.Generic;
using Application;

namespace Application {
  public class SimulationSolution {

    public Boolean isTms(MedicalEquipment me) {
      return me is Tms;
    }

    private Boolean isTdcs(MedicalEquipment me) {
      return me is Tdcs;
    }

    private Boolean isTsmEightCoil(MedicalEquipment me) {
      if (!isTms(me) || me == null)
        return false;

      Tms tms = (Tms)me;
      
      return tms.stimulator == TmsStimulator.EIGHT;
    }

    private Boolean isTsmHCoil(MedicalEquipment me) {
      if (!isTms(me) || me == null)
        return false;

      Tms tms = (Tms)me;

      return tms.stimulator == TmsStimulator.H;
    }

    private Boolean isTsmCircularCoil(MedicalEquipment me) {
      if (!isTms(me) || me == null)
        return false;

      Tms tms = (Tms)me;

      return tms.stimulator == TmsStimulator.CIRCULAR;
    }

    private Boolean usesMt(MedicalEquipment me) {
      return me.unitMeasure == UnitMeasure.PERCENTAGE_OF_MT;
    }

    private Boolean usesMa(MedicalEquipment me) {
      return me.unitMeasure == UnitMeasure.MILLIAMPERE;
    }

    private Boolean containsOnly(MedicalEquipment me, BrainZone brainZone) {
      return me.brainZones.Count == 1 &&
        me.brainZones.Contains(brainZone);
    }

    private Boolean containsAll(MedicalEquipment me,
      HashSet<BrainZone> brainZones) {
      Boolean containsAll = true;

      foreach(BrainZone bz in brainZones) {
        if (!me.brainZones.Contains(bz))
          containsAll = false; 
      }

      return containsAll;
    }

    private BrainZone toBrainZone(BrainZoneNames brainZoneName) {
      return new BrainZone(brainZoneName, Position.LEFT, 
        ElectrodeType.NEGATIVE);
    }

    private Outcome getOutcomeDepression(MedicalEquipment me,
      Pathology pathology) {

        // Depression, case number 1
        if (isTsmEightCoil(me) && usesMa(me) && me.pulse == Pulse.HIGH &&
              containsOnly(me, toBrainZone(BrainZoneNames.DLPFC))) { 
              
            if (me.intensity >= 90 && me.intensity < 120) 
              return Outcome.GOOD;

            else if (me.intensity == 120)
              return Outcome.VERY_GOOD;

            // had to do it, in order to prevent the "not all code paths return
            // a value" compilation error :)
            else return Outcome.UNCHANGED; 

        }
        // Depression, case number 2
        else if ((isTms(me) || isTdcs(me)) && !isTsmEightCoil(me) && 
          me.intensity <= 120 && me.unitMeasure != UnitMeasure.NO &&
          me.pulse == Pulse.LOW &&
          containsOnly(me, toBrainZone(BrainZoneNames.DLPFC))
          )
          
          return Outcome.UNCHANGED;

        // Depression, case number 4
        //TODO should we catch this while creating the med. equip.?
        else if (isTms(me) && me.intensity <= 120 &&
          me.unitMeasure != UnitMeasure.NO && me.pulse == Pulse.NO && 
          me.brainZones.Count > 1 
          )
          
          return Outcome.EXPLOSION;

      // Depression, case number 5
      //TODO should we catch this while creating the med. equip.?
      else if (isTdcs(me) && 
          me.intensity <= 120 && me.unitMeasure != 0 &&
          (me.pulse == Pulse.LOW || 
            me.pulse == Pulse.HIGH || 
            me.pulse == Pulse.SINGLE
          ) && me.brainZones.Count > 1           
        )
          return Outcome.EXPLOSION;

      // Depression, case number 3
      else if (
        me != null && me.intensity <= 120 && me.unitMeasure != UnitMeasure.NO && 
        me.pulse == Pulse.LOW && (
          (
            isTms(me) && 
            me.brainZones.Contains(toBrainZone(BrainZoneNames.DLPFC))
          ) || (
              isTdcs(me) && 
              !me.brainZones.Contains(toBrainZone(BrainZoneNames.DLPFC)) &&
              !me.brainZones.Contains(toBrainZone(BrainZoneNames.SO))
            )
          )
        )
          return Outcome.BAD; 

      else 
        return Outcome.UNCHANGED;
      }

    private Outcome getOutcomePostStrokeHand(MedicalEquipment me,
      Pathology pathology) {
        return Outcome.BAD;
    }

    public Outcome getOutcome(MedicalEquipment me, 
      Pathology pathology) {

      return pathology.name == PathologyName.DEPRESSION ? 
        getOutcomeDepression(me, pathology) :
        getOutcomePostStrokeHand(me, pathology);
      }
  }
}
