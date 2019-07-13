using System.Collections.Generic;
using UnityEngine;

namespace Application {
  public class SimulationSolution {

    // tested
    public bool isTms(MedicalEquipment me) { return me is Tms; }

    // tested
    public bool isTdcs(MedicalEquipment me) { return me is Tdcs; }

    // tested
    public bool isTsmEightCoil(MedicalEquipment me) {
      if (!isTms(me) || me == null) return false;

      Tms tms = (Tms)me;
      
      return tms.stimulator == TmsStimulator.EIGHT;
    }

    // tested
    public bool isTsmHCoil(MedicalEquipment me) {
      if (!isTms(me) || me == null) return false;

      Tms tms = (Tms)me;

      return tms.stimulator == TmsStimulator.H;
    }

    // tested
    public bool isTsmCircularCoil(MedicalEquipment me) {
      if (!isTms(me) || me == null) return false;

      Tms tms = (Tms)me;

      return tms.stimulator == TmsStimulator.CIRCULAR;
    }

    // tested
    public bool isTdcsHd(MedicalEquipment me)
    {
      if (!isTdcs(me) || me == null) return false;

      Tdcs tdcs = (Tdcs)me;

      return tdcs.stimulator == TdcsStimulator.HD;
    }

    public bool isTdcsDefault(MedicalEquipment me)
    {
      if (!isTdcs(me) || me == null) return false;

      Tdcs tdcs = (Tdcs)me;

      return tdcs.stimulator == TdcsStimulator.DEFAULT;
    }

    public bool isIpsiLateral(Position p1, Position p2) {
      return (
        (p1 == Position.RIGHT && p2 == Position.RIGHT)
        || (p1 == Position.LEFT && p2 == Position.LEFT)
        );
    }

    public bool isControLateral(Position p1, Position p2) {
      return (
        (p1 == Position.RIGHT && p2 == Position.LEFT) 
        || (p1 == Position.LEFT && p2 == Position.RIGHT)
        );
    }

    public bool inRange(double val, double r1, double r2) {
      return r1 <= val && val <= r2;
    }

    // tested
    public Outcome getOutcomeDepression(MedicalEquipment me,
      MedicalReport mr, BrainZonesArray brain) {
        // depression 1.1 tested
        if (brain.isUniqueStimulation(ElectrodeName.EIGHT) &&
          inRange(me.intensity, 90, 119) &&
          me.usesMt() &&
          me.isHighPulse() &&
          brain.isNeutral() &&
          brain.containsOnly(BrainZoneNames.DLPFC, Position.UPPER)
          ) 
          return Outcome.GOOD;
        
        // depression 1.2 tested
        else if (brain.isUniqueStimulation(ElectrodeName.EIGHT) &&
          me.intensity == 120 &&
          me.usesMt() &&
          me.isHighPulse() &&
          brain.isNeutral() &&
          brain.containsOnly(BrainZoneNames.DLPFC, Position.UPPER)
          )
          return Outcome.VERY_GOOD;
        
        // depression 2 tested, TODO add h
        else if ((brain.containsOnly(BrainZoneNames.DLPFC, Position.UPPER) 
            && brain.isUniqueStimulation(ElectrodeName.CIRCULAR) ||
            brain.isUniqueStimulation(ElectrodeName.H)) &&
          me.intensity <= 120 &&
          me.hasUnitMeasure() &&
          me.isLowPulse() &&
          brain.isNeutral())
          return Outcome.UNCHANGED;
        
        // depression 3
        else if (((brain.isElectricStimulation() && 
          brain
          .brainZones
          .TrueForAll(bz => 
            bz.brainZoneName == BrainZoneNames.DLPFC && !bz.isActive())
          && brain.isNeutral())
            || (brain.isMagneticStimulation() && 
              brain.brainZones.TrueForAll(bz => (bz.brainZoneName == BrainZoneNames.DLPFC || bz.brainZoneName == BrainZoneNames.SO) && !bz.isActive())) )
          &&
          me.intensity <= 120 &&
          me.hasUnitMeasure() &&
          me.isLowPulse())
          return Outcome.UNCHANGED;


        Debug.Log("config:\n" + me + "\n" + brain + "\n");
        Debug.Log(me.intensity <= 120);
        Debug.Log(me.hasUnitMeasure());
        Debug.Log(me.isLowPulse());
        Debug.Log(brain.isNeutral());
        Debug.Log(brain.containsOnly(BrainZoneNames.DLPFC, Position.UPPER));
        Debug.Log(brain.isUniqueStimulation(ElectrodeName.CIRCULAR));
        Debug.Log(brain.isUniqueStimulation(ElectrodeName.H));

        Debug.Log(
          brain.containsOnly(BrainZoneNames.DLPFC, Position.UPPER));
        return Outcome.EXPLOSION;
      }

    public Outcome getOutcomePostStrokeHand(MedicalEquipment me,
      MedicalReport mr, BrainZonesArray brain) {

        if(brain.isUniqueStimulation(ElectrodeName.DEFAULT) &&
          me.intensity == 1 &&
          me.usesMa() &&
          !me.hasPulse() &&
          (
            (brain.isCathodal(
              brain.getZone(BrainZoneNames.M1, Position.LEFT),
              brain.getZone(BrainZoneNames.SO, Position.RIGHT)) &&
              isControLateral(Position.LEFT, mr.pathology.position))||
            brain.isCathodal(
              brain.getZone(BrainZoneNames.M1, Position.RIGHT),
              brain.getZone(BrainZoneNames.SO, Position.LEFT))&&
              isControLateral(Position.RIGHT, mr.pathology.position))
          )
          return Outcome.VERY_GOOD;

        if(brain.isUniqueStimulation(ElectrodeName.DEFAULT) &&
          inRange(me.intensity, 0.8, 2) &&
          me.usesMa() &&
          !me.hasPulse() &&
          (
            (brain.isCathodal(
              brain.getZone(BrainZoneNames.M1, Position.LEFT),
              brain.getZone(BrainZoneNames.SO, Position.RIGHT)) &&
              isControLateral(Position.LEFT, mr.pathology.position))||
            brain.isCathodal(
              brain.getZone(BrainZoneNames.M1, Position.RIGHT),
              brain.getZone(BrainZoneNames.SO, Position.LEFT))&&
              isControLateral(Position.RIGHT, mr.pathology.position))
          ){
            Debug.Log("Catodico 0.8-2 contro");
            return Outcome.GOOD;
          }
          
          if(brain.isUniqueStimulation(ElectrodeName.DEFAULT) &&
          inRange(me.intensity, 0.8, 2) &&
          me.usesMa() &&
          !me.hasPulse() &&
          (
            (brain.isAnodal(
              brain.getZone(BrainZoneNames.M1, Position.LEFT),
              brain.getZone(BrainZoneNames.SO, Position.RIGHT)) &&
              isIpsiLateral(Position.LEFT, mr.pathology.position))||
            brain.isAnodal(
              brain.getZone(BrainZoneNames.M1, Position.RIGHT),
              brain.getZone(BrainZoneNames.SO, Position.LEFT))&&
              isIpsiLateral(Position.RIGHT, mr.pathology.position))
          )
          {
            Debug.Log("anodico 0.8-2 ipsi");
            return Outcome.GOOD;
          }

          if(brain.isUniqueStimulation(ElectrodeName.DEFAULT) &&
          me.intensity <= 0.8 &&
          me.usesMa() &&
          !me.hasPulse() &&
          (
            (brain.isCathodal(
              brain.getZone(BrainZoneNames.M1, Position.LEFT),
              brain.getZone(BrainZoneNames.SO, Position.RIGHT)) &&
              isIpsiLateral(Position.LEFT, mr.pathology.position))||
            brain.isCathodal(
              brain.getZone(BrainZoneNames.M1, Position.RIGHT),
              brain.getZone(BrainZoneNames.SO, Position.LEFT))&&
              isIpsiLateral(Position.RIGHT, mr.pathology.position))
          )
          {
            Debug.Log("Catodico <=0.8 ipsi");
            return Outcome.GOOD;
          }


  Debug.Log(brain.isAnodal(
              brain.getZone(BrainZoneNames.M1, Position.LEFT),
              brain.getZone(BrainZoneNames.SO, Position.RIGHT)).ToString() + " " +
              isControLateral(Position.LEFT, mr.pathology.position) + " " +
            brain.isAnodal(
              brain.getZone(BrainZoneNames.M1, Position.RIGHT),
              brain.getZone(BrainZoneNames.SO, Position.LEFT)).ToString()+ " " +
              isIpsiLateral(Position.RIGHT, mr.pathology.position).ToString()
          );
        return Outcome.EXPLOSION;
    }

    public Outcome getOutcome(MedicalEquipment me, MedicalReport mr, BrainZonesArray brain) {
      switch(mr.pathology.name) {
        case PathologyName.DEPRESSION:
          return this.getOutcomeDepression(me, mr, brain);
          
        case PathologyName.POST_STROKE_HAND:
          return this.getOutcomePostStrokeHand(me, mr, brain);

        default:
          return Outcome.UNCHANGED;
      }
    }
  }
}