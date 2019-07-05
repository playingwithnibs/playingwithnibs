using System;
using System.Collections.Generic;
using System.Linq;

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

    private Boolean isTdcsHd(MedicalEquipment me)
    {
      if (!isTdcs(me) || me == null)
        return false;

      Tdcs tdcs = (Tdcs)me;

      return tdcs.stimulator == TdcsStimulator.HD;
    }

    private Boolean usesMt(MedicalEquipment me) {
      return me.unitMeasure == UnitMeasure.PERCENTAGE_OF_MT;
    }

    private Boolean usesMa(MedicalEquipment me) {
      return me.unitMeasure == UnitMeasure.MILLIAMPERE;
    }

    private Boolean containsOnly(MedicalEquipment me, 
      BrainZoneNames brainZone) {
        return me.brainZones.countActiveZones == 1 &&
          me.brainZones[(int)brainZone].isActive();
    }

    private Boolean isAnodal(BrainZone source, BrainZone destination) {
      return source.electrodeType == ElectrodeType.POSITIVE &&
        destination.electrodeType == ElectrodeType.NEGATIVE;
    }

    private Boolean isCathodal(BrainZone source, BrainZone destination) {
      return !isAnodal(source, destination);
    }

    private Boolean isNeutral(BrainZone source, BrainZone destination) {
      return !isAnodal(source, destination) && 
        !isCathodal(source, destination);
    }

    private Boolean isControLateral(Position p1, Position p2) {
      return (
        (p1 == Position.RIGHT && p2 == Position.LEFT) 
        || (p1 == Position.LEFT && p2 == Position.RIGHT)
        );
    }

    private Boolean isIpsiLateral(Position p1, Position p2) {
      return (
        (p1 == Position.RIGHT && p2 == Position.RIGHT)
        || (p1 == Position.LEFT && p2 == Position.LEFT)
        );
    }

    private Outcome getOutcomeDepression(MedicalEquipment me,
      Pathology pathology) {

        // Depression, case number 1
        if (isTsmEightCoil(me) && usesMa(me) && me.pulse == Pulse.HIGH &&
              containsOnly(me, toBrainZone(BrainZoneNames.DLPFC)) && 
              me.stimulationType == StimulationType.NO) { 
              
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
          containsOnly(me, BrainZoneNames.DLPFC) &&
          me.stimulationType == StimulationType.NO
          )
          
          return Outcome.UNCHANGED;

        // Depression, case number 4
        //TODO should we catch this while creating the med. equip.?
        else if (isTms(me) && me.intensity <= 120 &&
          me.unitMeasure != UnitMeasure.NO && me.pulse == Pulse.NO && 
          me.brainZones.countActiveZones > 0
          )
          
          return Outcome.EXPLOSION;

        // Depression, case number 5
        //TODO should we catch this while creating the med. equip.?
        else if (isTdcs(me) && 
            me.intensity <= 120 && me.unitMeasure != 0 &&
            (me.pulse == Pulse.LOW || me.pulse == Pulse.HIGH || 
              me.pulse == Pulse.SINGLE
            ) && me.brainZones.countActiveZones > 0           
          )
            return Outcome.EXPLOSION;

        // Depression, case number 3
        else if (
          me != null && me.intensity <= 120 && 
          me.unitMeasure != UnitMeasure.NO && me.pulse == Pulse.LOW &&
          !me.brainZones[(int)BrainZoneNames.DLPFC].isActive() && (
            (
              isTms(me) && 
              me.stimulationType == StimulationType.NO
            ) || (
                isTdcs(me) &&
                !me.brainZones[(int)BrainZoneNames.SO].isActive() && 
                (me.stimulationType == StimulationType.ANODAL ||
                  me.stimulationType == StimulationType.CATHODAL)
              )
            ) 
          )
            return Outcome.BAD; 

        else 
          return Outcome.UNCHANGED;
      }

    private Outcome getOutcomePostStrokeHand(MedicalEquipment me,
      Pathology pathology) {
        //Post Stroke: Hand, case number 1.1
        if (isTdcs(me) && (
            (me.intensity >= 0.8 && me.intensity < 1) ||
            (me.intensity > 1 && me.intensity <= 2)) && 
          me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO && 
          me.stimulationType == StimulationType.CATHODAL &&
          me.brainZones[(int)BrainZoneNames.M1].isActive() &&
          me.brainZones[(int)BrainZoneNames.SO].isActive() &&
          isControLateral(BrainZones[(int)BrainZoneNames.M1].position, 
            pathology.position)
        )
          return Outcome.GOOD;

        //Post Stroke: Hand, case number 1.2
        else if (isTdcs(me) && me.intensity == 1 &&
          me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO &&
          me.stimulationType == StimulationType.CATHODAL &&
          me.brainZones[(int)BrainZoneNames.M1].isActive() &&
          me.brainZones[(int)BrainZoneNames.SO].isActive() &&
          isControLateral(BrainZones[(int)BrainZoneNames.M1].position,
            pathology.position)
        )
          return Outcome.VERY_GOOD;

        //Post Stroke: Hand, case number 2
        else if (isTdcs(me) && me.intensity >= 0.8 && me.intensity <= 2 &&
          me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO &&
          me.stimulationType == StimulationType.ANODAL &&
          me.brainZones[(int)BrainZoneNames.M1].isActive() &&
          me.brainZones[(int)BrainZoneNames.SO].isActive() &&
          isIpsiLateral(BrainZones[(int)BrainZoneNames.M1].position,
            pathology.position)
        )
          return Outcome.GOOD;

        //Post Stroke: Hand, case number 3
        else if (isTdcs(me) && me.intensity < 0.8 &&
          me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO &&
          me.stimulationType == StimulationType.CATHODAL &&
          me.brainZones[(int)BrainZoneNames.M1].isActive() &&
          me.brainZones[(int)BrainZoneNames.SO].isActive() &&
          isIpsiLateral(BrainZones[(int)BrainZoneNames.M1].position,
            pathology.position)
        )
          return Outcome.BAD;

        //Post Stroke: Hand, case number 4
        else if (isTdcs(me) && me.intensity < 0.8 &&
          me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO &&
          me.stimulationType == StimulationType.ANODAL &&
          me.brainZones[(int)BrainZoneNames.M1].isActive() &&
          me.brainZones[(int)BrainZoneNames.SO].isActive() &&
          isControLateral(BrainZones[(int)BrainZoneNames.M1].position,
            pathology.position)
        )
          return Outcome.BAD;
        
        //Post Stroke: Hand, case number 5
        else if (isTdcsHd(me) && me.intensity < 2 &&
          me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO &&
          (
            me.stimulationType == StimulationType.ANODAL || 
            me.stimulationType == StimulationType.CATHODAL
          ) && 
          me.brainZones[(int)BrainZoneNames.M1].isActive() &&
          me.brainZones[(int)BrainZoneNames.SO].isActive()
        )
          return Outcome.UNCHANGED;
        
        //Post Stroke: Hand, case number 6
        else if (isTdcs(me) && me.intensity > 2 &&
          me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO &&
          (
            me.stimulationType == StimulationType.ANODAL || 
            me.stimulationType == StimulationType.CATHODAL
          ) && 
          me.brainZones.countActiveZonesn > 0
        )
          return Outcome.VERY_BAD;

        //Post Stroke: Hand, case number 7
        else if (isTdcs(me) && me.intensity < 2 &&
          me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO &&
          (
            me.stimulationType == StimulationType.ANODAL || 
            me.stimulationType == StimulationType.CATHODAL
          ) && 
          me.brainZones.countActiveZonesn > 0 && 
          !me.brainZones[(int)BrainZoneNames.M1].isActive()
        )
          return Outcome.BAD;

        //Post Stroke: Hand, case number 8 and 11
        else if (isTsmEightCoil(me) && me.intensity >= 70 
          && me.intensity <= 120 &&
          me.unitMeasure == UnitMeasure.PERCENTAGE_OF_MT &&
          me.pulse == Pulse.HIGH && 
          me.stimulationType == StimulationType.MAGNETIC && 
          me.brainZones[(int)BrainZoneNames.M1].isActive()
        ) {
          // this is case number 8
          if (isIpsiLateral(me.brainZones[(int)BrainZoneNames.M1].position,
            pathology.position))
              return Outcome.GOOD;
          // this is case number 11
          else if (isControLateral(me.brainZones[(int)BrainZoneNames.M1].position,
            pathology.position))
              return Outcome.BAD;
        }

        //Post Stroke: Hand, case number 9 and 10
        else if (isTsmEightCoil(me) && me.intensity >= 70 
          && me.intensity <= 120 &&
          me.unitMeasure == UnitMeasure.PERCENTAGE_OF_MT &&
          me.pulse == Pulse.LOW && 
          me.stimulationType == StimulationType.MAGNETIC && 
          me.brainZones[(int)BrainZoneNames.M1].isActive()
        ) {
          // this is case number 10
          if (isIpsiLateral(me.brainZones[(int)BrainZoneNames.M1].position,
            pathology.position))
              return Outcome.BAD;
          // this is case number 9
          else if (isControLateral(me.brainZones[(int)BrainZoneNames.M1].position,
            pathology.position))
              return Outcome.GOOD;
        }

        //Post Stroke: Hand, case number 12
        else if ((isTsmCircularCoil(me) || isTsmHCoil(me)) && 
          me.intensity < 120 && 
          me.unitMeasure == UnitMeasure.PERCENTAGE_OF_MT &&
          !me.pulse == Pulse.NO &&
          me.brainZones[(int)BrainZoneNames.M1].isActive()
        )
          return Outcome.UNCHANGED;

        //Post Stroke: Hand, case number 13
        //TODO decide whether we want to catch somewhere else during the config
        //     process
        else if (isTsm(me) && 
          (
            (me.intensity <= 120 
              && me.unitMeasure == UnitMeasure.PERCENTAGE_OF_MT) 
              || 
            (me.intensity <= 2 && me.unitMeasure == UnitMeasure.MILLIAMPERE)
          ) && me.pulse == Pulse.NO && 
          me.stimulationType != StimulationType.NO &&
          me.brainZones.countActiveZones > 0
        )
          return Outcome.EXPLOSION;
        
        //Post Stroke: Hand, case number 13
        //TODO decide whether we want to catch somewhere else during the config
        //     process
        else if (isTdcs(me) && 
          (
            (me.intensity <= 120 
              && me.unitMeasure == UnitMeasure.PERCENTAGE_OF_MT) 
              || 
            (me.intensity <= 2 && me.unitMeasure == UnitMeasure.MILLIAMPERE)
          ) && !me.pulse == Pulse.NO && 
          me.stimulationType != StimulationType.NO &&
          me.brainZones.countActiveZones > 0
        )
          return Outcome.EXPLOSION;

        else return Outcome.BAD;
    }

    public Outcome getOutcome(MedicalEquipment me, 
      Pathology pathology) {

      return pathology.name == PathologyName.DEPRESSION ? 
        getOutcomeDepression(me, pathology) :
        getOutcomePostStrokeHand(me, pathology);
      }
  }
}
