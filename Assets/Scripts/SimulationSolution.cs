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

    public bool inRange(double val, double r1, double r2) {
      return r1 <= val && val <= r2;
    }

    // tested
    public Outcome getOutcomeDepression(MedicalEquipment me, 
      BrainZonesArray brain) {
        // depression 1.1 tested
        if (brain.isUniqueStimulation(ElectrodeName.EIGHT) &&
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
        else if (brain.isUniqueStimulation(ElectrodeName.EIGHT) &&
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
        else if ((brain.containsOnly(BrainZoneNames.DLPFC, Position.UPPER) 
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
              brain.isNeutral())
            ||
            (brain.doesNotContain(BrainZoneNames.DLPFC) &&
              brain.doesNotContain(BrainZoneNames.SO) &&
              brain.isElectricStimulation() &&
              brain.isAnodalOrCathodal()))
          &&
          me.intensity <= 120 &&
          me.hasUnitMeasure() &&
          me.isLowPulse()) {
            Debug.Log("DEPRESSIN 3");
            return Outcome.BAD;
          }

        // depression 4/5 tested
        else if (
          ((brain.isMagneticStimulation() && !me.hasPulse())
          ||
          (
            brain.isElectricStimulation() && 
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