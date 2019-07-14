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
      BrainZonesArray brain) {
        // depression 1.1 tested
        if (isTms(me) && brain.isUniqueStimulation(ElectrodeName.EIGHT) &&
          inRange(me.intensity, 90, 119) &&
          me.usesMt() &&
          me.isHighPulse() &&
          brain.isNeutral() &&
          brain.containsOnly(BrainZoneNames.DLPFC, Position.UPPER)
          ) {
            Debug.Log("DEPRESSION 1.1");
            return Outcome.GOOD;
          }
        
        // depression 1.2 tested
        else if (isTms(me) && brain.isUniqueStimulation(ElectrodeName.EIGHT) &&
          me.intensity == 120 &&
          me.usesMt() &&
          me.isHighPulse() &&
          brain.isNeutral() &&
          brain.containsOnly(BrainZoneNames.DLPFC, Position.UPPER)
          ) {
            Debug.Log("DEPRESSION 1.2");
            return Outcome.VERY_GOOD;
          }
        
        // depression 2 tested, TODO add h
        else if (isTms(me) && 
            (brain.containsOnly(BrainZoneNames.DLPFC, Position.UPPER) 
            && brain.isUniqueStimulation(ElectrodeName.CIRCULAR) ||
            brain.isUniqueStimulation(ElectrodeName.H)) &&
          me.intensity <= 120 &&
          me.hasUnitMeasure() &&
          me.isLowPulse() &&
          brain.isNeutral()) {
            Debug.Log("DEPRESSION 2");
            return Outcome.UNCHANGED;
          }
        
        // depression 3 tested
        else if (
          (
            (brain.doesNotContain(BrainZoneNames.DLPFC) && 
              brain.isMagneticStimulation() &&
              brain.isNeutral() && isTms(me))
            ||
            (brain.doesNotContain(BrainZoneNames.DLPFC) &&
              brain.doesNotContain(BrainZoneNames.SO) &&
              brain.isElectricStimulation() &&
              brain.isAnodalOrCathodal() && isTdcs(me)))
          &&
          me.intensity <= 120 &&
          me.hasUnitMeasure() &&
          me.isLowPulse()) {
            Debug.Log("DEPRESSIN 3");
            return Outcome.BAD;
          }

        // depression 4/5 tested
        else if (
          ((brain.isMagneticStimulation() && !me.hasPulse() && isTms(me))
          ||
          (
            brain.isElectricStimulation() && isTdcs(me) &&
            (me.isHighPulse() || me.isSinglePulse()))
          ) 
          && me.intensity <= 120 && me.hasUnitMeasure()) {
            Debug.Log("DEPRESSION 4/5");
            return Outcome.EXPLOSION;
          }
        
        
        
        
        
        // Debug.Log("doesNotContain: " + brain.doesNotContain(BrainZoneNames.DLPFC));
        // Debug.Log("ismagnetic: " +brain.isMagneticStimulation() );
        // Debug.Log("isneutral: " + brain.isNeutral()); 
        
        Debug.Log("DEPRESSION UNMODELLED");
        return Outcome.EXPLOSION;
      }

    public Outcome getOutcomePostStrokeHand(MedicalEquipment me,
      MedicalReport mr, BrainZonesArray brain) {

        if(isTdcs(me) &&
          brain.isUniqueStimulation(ElectrodeName.DEFAULT) &&
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

        if(isTdcs(me) &&
          brain.isUniqueStimulation(ElectrodeName.DEFAULT) &&
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
          
          if(isTdcs(me) &&
          brain.isUniqueStimulation(ElectrodeName.DEFAULT) &&
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

          if(isTdcs(me) &&
          brain.isUniqueStimulation(ElectrodeName.DEFAULT) &&
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
            return Outcome.BAD;
          }

          if(isTdcs(me) &&
          brain.isUniqueStimulation(ElectrodeName.DEFAULT) &&
          me.intensity <= 0.8 &&
          me.usesMa() &&
          !me.hasPulse() &&
          (
            (brain.isAnodal(
              brain.getZone(BrainZoneNames.M1, Position.LEFT),
              brain.getZone(BrainZoneNames.SO, Position.RIGHT)) &&
              isControLateral(Position.LEFT, mr.pathology.position)) ||
            brain.isAnodal(
              brain.getZone(BrainZoneNames.M1, Position.RIGHT),
              brain.getZone(BrainZoneNames.SO, Position.LEFT)) &&
              isControLateral(Position.RIGHT, mr.pathology.position))
          )
          {
            Debug.Log("Anodico <=0.8 contro");
            return Outcome.BAD;
          }

          if(isTdcs(me) &&
          brain.isUniqueStimulation(ElectrodeName.HD) &&
          me.intensity <= 2 &&
          me.usesMa() &&
          !me.hasPulse() &&
          // brain.isAnodalOrCathodal() && 
          brain.isNeutral() &&
          (
            brain.containsOnly(BrainZoneNames.M1) || 
            brain.containsOnly(BrainZoneNames.SO)
          )
          )
              
          {
            return Outcome.UNCHANGED;
          }

          if(brain.isElectricStimulation() &&
          me.intensity > 2 &&
          me.usesMa() &&
          !me.hasPulse() &&
          brain.countActiveZones > 0
          )
              
          {
            Debug.Log("Anodo o catodo >2 dove vuoi");
            return Outcome.VERY_BAD;
          }

          if(isTdcs(me) &&
          brain.isUniqueStimulation(ElectrodeName.DEFAULT) &&
          me.intensity < 2 &&
          me.usesMa() &&
          !me.hasPulse() &&
          brain.doesNotContain(BrainZoneNames.M1) &&
          brain.countActiveZones > 0
          )
              
          {
            Debug.Log("Anodo o catodo < 2 no m1");
            return Outcome.BAD;
          }
          
          if(isTms(me) &&
          brain.isUniqueStimulation(ElectrodeName.EIGHT) &&
          inRange(me.intensity,70,120) &&
          me.usesMt() &&
          me.isHighPulse() &&
          brain.isNeutral() &&
          brain.containsOnly(BrainZoneNames.M1) &&
          isIpsiLateral(brain.getZones(BrainZoneNames.M1).Find(bz => bz.isActive()).position, mr.pathology.position)
          )
              
          {
            Debug.Log("eight 70-120 m1 ipsi");
            return Outcome.GOOD;
          }
          
          if(isTms(me) &&
          brain.isUniqueStimulation(ElectrodeName.EIGHT) &&
          inRange(me.intensity,70,120) &&
          me.usesMt() &&
          me.isLowPulse() &&
          brain.isNeutral() &&
          brain.containsOnly(BrainZoneNames.M1) &&
          isControLateral(brain.getZones(BrainZoneNames.M1).Find(bz => bz.isActive()).position, mr.pathology.position)
          )
              
          {
            Debug.Log("eight 70-120 m1 ipsi");
            return Outcome.GOOD;
          }
          
          if(isTms(me) &&
          brain.isUniqueStimulation(ElectrodeName.EIGHT) &&
          inRange(me.intensity,70,120) &&
          me.usesMt() &&
          me.isLowPulse() &&
          brain.isNeutral() &&
          brain.containsOnly(BrainZoneNames.M1) &&
          isIpsiLateral(brain.getZones(BrainZoneNames.M1).Find(bz => bz.isActive()).position, mr.pathology.position)
          )
              
          {
            Debug.Log("eight 70-120 low m1 ipsi bad");
            return Outcome.BAD;
          }
          
          if(isTms(me) &&
          (
            brain.isUniqueStimulation(ElectrodeName.H) ||
            brain.isUniqueStimulation(ElectrodeName.CIRCULAR)
          ) &&
          me.intensity < 120 &&
          me.usesMt() &&
          me.hasPulse() &&
          brain.containsOnly(BrainZoneNames.M1)
          )
              
          {
            Debug.Log("eight < 120 pulse m1");
            return Outcome.UNCHANGED;
          }


  // Debug.Log(
  //           isTms(me).ToString() + " " +
  //         brain.isUniqueStimulation(ElectrodeName.EIGHT).ToString() + " " +
  //         inRange(me.intensity,70,120).ToString() + " " +
  //         me.usesMt().ToString() + " " +
  //         me.isHighPulse().ToString() + " " +
  //         brain.isNeutral().ToString() + " " +
  //         brain.containsOnly(BrainZoneNames.M1).ToString() + " " +
  //         isIpsiLateral(Position.LEFT, mr.pathology.position).ToString() + " " +
  //          isIpsiLateral(Position.RIGHT, mr.pathology.position).ToString()
          
          
  //         );
        return Outcome.EXPLOSION;
    }

    public Outcome getOutcome(MedicalEquipment me, MedicalReport mr, BrainZonesArray brain) {
      switch(mr.pathology.name) {
        case PathologyName.DEPRESSION:
          return this.getOutcomeDepression(me, brain);
          
        case PathologyName.POST_STROKE_HAND:
          return this.getOutcomePostStrokeHand(me, mr, brain);

        default:
          return Outcome.UNCHANGED;
      }
    }
  }
}