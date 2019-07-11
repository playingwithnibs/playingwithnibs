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

    // tested
    public bool containsOnly(MedicalEquipment me, 
      BrainZoneNames brainZone) {
        return me.brainZones.countActiveZones == 1 &&
          me.brainZones.brainZones[(int)brainZone].isActive();
    }

    // tested
    public bool isAnodal(BrainZone source, BrainZone destination) {
      return source.electrodeType == ElectrodeType.POSITIVE &&
        destination.electrodeType == ElectrodeType.NEGATIVE;
    }

    // tested
    public bool isAnodalOrCathodal(BrainZonesArray bza) {
      int countPositive = 0;
      int countNegative = 0;

      for (int i = 0; i < 6; i++) {
        if (bza.brainZones[i].electrodeType == ElectrodeType.POSITIVE)
          countPositive++;
        else if (bza.brainZones[i].electrodeType == ElectrodeType.NEGATIVE)
          countNegative++;
      }

      return countNegative == 1 && countPositive == 1;
    }

    // tested
    public bool isCathodal(BrainZone source, BrainZone destination) {
      return source.electrodeType == ElectrodeType.NEGATIVE &&
        destination.electrodeType == ElectrodeType.POSITIVE;
    }

    // tested
    public bool isNeutral(BrainZonesArray bza) {
      bool isNeutral = true;

      for (int i = 0; i < 6; i++) {
        if (bza.brainZones[i].electrodeType == ElectrodeType.POSITIVE ||
          bza.brainZones[i].electrodeType == ElectrodeType.NEGATIVE)
          isNeutral = false;
      }

      return isNeutral;
    }

    // tested
    public bool isControLateral(Position p1, Position p2) {
      return (
        (p1 == Position.RIGHT && p2 == Position.LEFT) 
        || (p1 == Position.LEFT && p2 == Position.RIGHT)
        );
    }

    // tested
    public bool isIpsiLateral(Position p1, Position p2) {
      return (
        (p1 == Position.RIGHT && p2 == Position.RIGHT)
        || (p1 == Position.LEFT && p2 == Position.LEFT)
        );
    }

    // tested
    public Outcome getOutcomeDepression(MedicalEquipment me,
      MedicalReport mr) {

        // Depression, case number 1
        // tested
        if (isTsmEightCoil(me) && 
          me.unitMeasure == UnitMeasure.PERCENTAGE_OF_MT && 
          me.pulse == Pulse.HIGH &&
          containsOnly(me, BrainZoneNames.DLPFC) && isNeutral(me.brainZones)) { 
              
            if (me.intensity >= 90 && me.intensity < 120) {
              //Console.WriteLine("Depression, case number 1");
              return Outcome.GOOD;
            }

            else if (me.intensity == 120) {
              //Console.WriteLine("Depression, case number 1");
              return Outcome.VERY_GOOD;
            }
              

            // had to do it, in order to prevent the "not all code paths return
            // a value" compilation error :)
            else {
              //Console.WriteLine("Depression, case number 1");
              return Outcome.UNCHANGED; 
            }

        }

        // Depression, case number 2
        // tested
        else if ((isTms(me) || isTdcs(me)) && !isTsmEightCoil(me) && 
          me.intensity <= 120 && me.unitMeasure != UnitMeasure.NO &&
          me.pulse == Pulse.LOW &&
          containsOnly(me, BrainZoneNames.DLPFC) && isNeutral(me.brainZones)) {
            //Console.WriteLine("Depression, case number 2");
            return Outcome.UNCHANGED;
          }

      // Depression, case number 3
      else if (
        (isTdcs(me) || isTms(me)) && me.intensity <= 120 &&
        me.unitMeasure != UnitMeasure.NO && me.pulse == Pulse.LOW &&
        !me.brainZones.brainZones[(int)BrainZoneNames.DLPFC].isActive() && (
          (
            isTms(me) &&
            me.stimulationType == StimulationType.NO && isNeutral(me.brainZones)
          ) || 
          (
            isTdcs(me) &&
            !me
              .brainZones
              .brainZones[(int)BrainZoneNames.SO]
              .isActive() &&
            isAnodalOrCathodal(me.brainZones)
          )
        )
      ) {
        //Console.WriteLine("Depression, case number 3");
        return Outcome.BAD; 
      }

      // Depression, case number 4
      // tested
      //TODO should we catch this while creating the med. equip.?
      else if (isTms(me) && me.intensity <= 120 &&
          me.unitMeasure != UnitMeasure.NO && me.pulse == Pulse.NO) {
            //Console.WriteLine("Depression, case number 4");
            return Outcome.EXPLOSION;
          }

        // Depression, case number 5
        // tested
        //TODO should we catch this while creating the med. equip.?
        else if (isTdcs(me) && 
            me.intensity <= 120 && me.unitMeasure != 0 &&
            (me.pulse == Pulse.HIGH || me.pulse == Pulse.SINGLE
            ) && me.brainZones.countActiveZones > 0           
          ) {
            //Console.WriteLine("Depression, case number 5");
            return Outcome.EXPLOSION;
          }

        // tested
        else {
          //Console.WriteLine("Depression, unmodelled case");
          return Outcome.UNCHANGED;
        }
      }

    public Outcome getOutcomePostStrokeHand(MedicalEquipment me,
      MedicalReport mr) {
        //Post Stroke: Hand, case number 1.1
        // tested
        if (isTdcsDefault(me) && (
            (me.intensity >= 0.8 && me.intensity < 1) ||
            (me.intensity > 1 && me.intensity <= 2)) && 
          me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO && 
          isCathodal(me.brainZones.brainZones[(int)BrainZoneNames.M1], 
            me.brainZones.brainZones[(int)BrainZoneNames.SO]) &&
          me.brainZones.brainZones[(int)BrainZoneNames.M1].isActive() &&
          me.brainZones.brainZones[(int)BrainZoneNames.SO].isActive() &&
          isControLateral(
            me.brainZones.brainZones[(int)BrainZoneNames.M1].position, 
            mr.pathology.position)
        ) {
          //Console.WriteLine("P_S_H, case number 1.1");
          return Outcome.GOOD;
        }
          

        //Post Stroke: Hand, case number 1.2
        // tested
        else if (isTdcsDefault(me) && me.intensity == 1 &&
          me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO &&
          isCathodal(me.brainZones.brainZones[(int)BrainZoneNames.M1],
            me.brainZones.brainZones[(int)BrainZoneNames.SO]) &&
          me.brainZones.brainZones[(int)BrainZoneNames.M1].isActive() &&
          me.brainZones.brainZones[(int)BrainZoneNames.SO].isActive() &&
          isControLateral(
            me.brainZones.brainZones[(int)BrainZoneNames.M1].position,
            mr.pathology.position)
        ) {
          //Console.WriteLine("P_S_H, case number 1.2");
          return Outcome.VERY_GOOD;
        }
          

        //Post Stroke: Hand, case number 2
        // tested
        else if (isTdcsDefault(me) && me.intensity >= 0.8 && 
          me.intensity <= 2 && me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO &&
          isAnodal(me.brainZones.brainZones[(int)BrainZoneNames.M1],
            me.brainZones.brainZones[(int)BrainZoneNames.SO]) &&
          me.brainZones.brainZones[(int)BrainZoneNames.M1].isActive() &&
          me.brainZones.brainZones[(int)BrainZoneNames.SO].isActive() &&
          isIpsiLateral(
            me.brainZones.brainZones[(int)BrainZoneNames.M1].position,
            mr.pathology.position)
        ) {
          ////Console.WriteLine("P_S_H, case number 2");
          return Outcome.GOOD;
        }
          

        //Post Stroke: Hand, case number 3
        // tested
        else if (isTdcsDefault(me) && me.intensity < 0.8 &&
          me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO &&
          isCathodal(me.brainZones.brainZones[(int)BrainZoneNames.M1],
            me.brainZones.brainZones[(int)BrainZoneNames.SO]) &&
          me.brainZones.brainZones[(int)BrainZoneNames.M1].isActive() &&
          me.brainZones.brainZones[(int)BrainZoneNames.SO].isActive() &&
          isIpsiLateral(
            me.brainZones.brainZones[(int)BrainZoneNames.M1].position,
            mr.pathology.position)
        ) {
          ////Console.WriteLine("P_S_H, case number 3");
          return Outcome.BAD;
        }

        //Post Stroke: Hand, case number 4
        // tested
        else if (isTdcsDefault(me) && me.intensity < 0.8 &&
          me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO &&
          isAnodal(me.brainZones.brainZones[(int)BrainZoneNames.M1],
            me.brainZones.brainZones[(int)BrainZoneNames.SO]) &&
          me.brainZones.brainZones[(int)BrainZoneNames.M1].isActive() &&
          me.brainZones.brainZones[(int)BrainZoneNames.SO].isActive() &&
          isControLateral(
            me.brainZones.brainZones[(int)BrainZoneNames.M1].position,
            mr.pathology.position)
        ) {
          ////Console.WriteLine("P_S_H, case number 4");
          return Outcome.BAD;
        }
        
        //Post Stroke: Hand, case number 5
        // tested
        else if (isTdcsHd(me) && me.intensity < 2 &&
          me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO &&
          (
            isAnodal(me.brainZones.brainZones[(int)BrainZoneNames.M1],
            me.brainZones.brainZones[(int)BrainZoneNames.SO]) || 
            isCathodal(me.brainZones.brainZones[(int)BrainZoneNames.M1],
            me.brainZones.brainZones[(int)BrainZoneNames.SO])
          ) && 
          me.brainZones.brainZones[(int)BrainZoneNames.M1].isActive() &&
          me.brainZones.brainZones[(int)BrainZoneNames.SO].isActive()
        ) {
          ////Console.WriteLine("P_S_H, case number 5");
          return Outcome.UNCHANGED;
        }
        
        //Post Stroke: Hand, case number 6
        // tested
        else if (isTdcs(me) && me.intensity > 2 &&
          me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO &&
          (
            isAnodal(me.brainZones.brainZones[(int)BrainZoneNames.M1],
            me.brainZones.brainZones[(int)BrainZoneNames.SO]) || 
            isCathodal(me.brainZones.brainZones[(int)BrainZoneNames.M1],
            me.brainZones.brainZones[(int)BrainZoneNames.SO])
          ) && me.brainZones.countActiveZones == 2
        ) {
          ////Console.WriteLine("P_S_H, case number 6");
          return Outcome.VERY_BAD;
        }

        //Post Stroke: Hand, case number 7
        else if (isTdcs(me) && me.intensity < 2 &&
          me.unitMeasure == UnitMeasure.MILLIAMPERE &&
          me.pulse == Pulse.NO && isAnodalOrCathodal(me.brainZones) && 
          me.brainZones.countActiveZones == 2 && 
          !me.brainZones.brainZones[(int)BrainZoneNames.M1].isActive()
        ) {
          ////Console.WriteLine("P_S_H, case number 7");          
          return Outcome.BAD;
        }

        //Post Stroke: Hand, case number 8 and 11
        // tested
        else if (isTsmEightCoil(me) && me.intensity >= 70 
          && me.intensity <= 120 &&
          me.unitMeasure == UnitMeasure.PERCENTAGE_OF_MT &&
          me.pulse == Pulse.HIGH && 
          isNeutral(me.brainZones) && 
          me.brainZones.brainZones[(int)BrainZoneNames.M1].isActive()
        ) {
          // this is case number 8
          // tested
          if (isIpsiLateral(
            me.brainZones.brainZones[(int)BrainZoneNames.M1].position,
            mr.pathology.position)) {
            ////Console.WriteLine("P_S_H, case number 8");
              return Outcome.GOOD;
            }
          // this is case number 11
          // tested
          else if (isControLateral(
            me.brainZones.brainZones[(int)BrainZoneNames.M1].position,
            mr.pathology.position)) {
              ////Console.WriteLine("P_S_H, case number 11");
              return Outcome.BAD;
            }
          
          // had to do it, in order to avoi the "not all code paths return a 
          // value" error.
          else
            return Outcome.UNCHANGED;
        }

        //Post Stroke: Hand, case number 9 and 10
        // tested
        else if (isTsmEightCoil(me) && me.intensity >= 70 
          && me.intensity <= 120 &&
          me.unitMeasure == UnitMeasure.PERCENTAGE_OF_MT &&
          me.pulse == Pulse.LOW && 
          isNeutral(me.brainZones) && 
          me.brainZones.brainZones[(int)BrainZoneNames.M1].isActive()
        ) {
          // this is case number 10
          //tested
          if (isIpsiLateral(
            me.brainZones.brainZones[(int)BrainZoneNames.M1].position,
            mr.pathology.position)) {
              ////Console.WriteLine("P_S_H, case number 10");
              return Outcome.BAD;
            }
              
          // this is case number 9
          // tested 
          else if (isControLateral(
            me.brainZones.brainZones[(int)BrainZoneNames.M1].position,
            mr.pathology.position)) {
              ////Console.WriteLine("P_S_H, case number 9");
              return Outcome.GOOD;
            }
              
          // had to do it, in order to avoi the "not all code paths return a 
          // value" error.
          else
            return Outcome.UNCHANGED;
        }

        //Post Stroke: Hand, case number 12
        // tested
        else if ((isTsmCircularCoil(me) || isTsmHCoil(me)) && 
          me.intensity < 120 && 
          me.unitMeasure == UnitMeasure.PERCENTAGE_OF_MT &&
          me.pulse != Pulse.NO &&
          me.brainZones.brainZones[(int)BrainZoneNames.M1].isActive()
        ) {
          ////Console.WriteLine("P_S_H, case number 12");
          return Outcome.UNCHANGED;
        }
          

        //Post Stroke: Hand, case number 13
        //TODO decide whether we want to catch somewhere else during the config
        //     process
        // tested
        else if (isTms(me) && 
          (
            (me.intensity <= 120 
              && me.unitMeasure == UnitMeasure.PERCENTAGE_OF_MT) 
              || 
            (me.intensity <= 2 && me.unitMeasure == UnitMeasure.MILLIAMPERE)
          ) && me.pulse == Pulse.NO) {
            ////Console.WriteLine("P_S_H, case number 13");
            return Outcome.EXPLOSION;
        }
          
        
        //Post Stroke: Hand, case number 14
        //TODO decide whether we want to catch somewhere else during the config
        //     process
        // tested
        else if (isTms(me) && 
          (
            (me.intensity <= 120 
              && me.unitMeasure == UnitMeasure.PERCENTAGE_OF_MT
            ) 
              || 
            (me.intensity <= 2 && me.unitMeasure == UnitMeasure.MILLIAMPERE)
          ) && me.pulse != Pulse.NO) {
          ////Console.WriteLine("P_S_H, case number 14");
          return Outcome.EXPLOSION;
        }
          

        else {
          ////Console.WriteLine("P_S_H, unmodelled case");
          return Outcome.UNCHANGED;
        }
    }

    public Outcome getOutcome(MedicalEquipment me, MedicalReport mr) {

        switch(mr.pathology.name) {
          case PathologyName.DEPRESSION:
            return getOutcomeDepression(me, mr);
          
          case PathologyName.POST_STROKE_HAND:
            return getOutcomePostStrokeHand(me, mr);

          default:
            return Outcome.UNCHANGED;
        }
      }
  }
}