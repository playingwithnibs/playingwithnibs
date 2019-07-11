using System;

namespace Application
{
  public class test {
    public static void Main() {
      SimulationSolution ss = new SimulationSolution();

      // Pathology p = new Pathology(PathologyName.POST_STROKE_HAND, 
      //   Position.RIGHT);

      // Tms tms = new Tms(UnitMeasure.PERCENTAGE_OF_MT, 9, Pulse.HIGH, 
      //   new BrainZonesArray(), TmsStimulator.EIGHT, StimulationType.NO);

      // Tdcs tdcs = new Tdcs(UnitMeasure.MILLIAMPERE, 0.2568742684, Pulse.NO,
      //   new BrainZonesArray(), TdcsStimulator.DEFAULT, StimulationType.NO);

      // tdcs.brainZones.activateZone(BrainZoneNames.DLPFC, ElectrodeType.NEGATIVE);
      // tdcs.brainZones.activateZone(BrainZoneNames.SO, ElectrodeType.POSITIVE);

      // tdcs.brainZones.brainZones[(int)BrainZoneNames.M1].position = 
      //   Position.LEFT;

      // Console.WriteLine(ss.getOutcomePostStrokeHand(tdcs, new MedicalReport()));

      MedicalReport mr = new MedicalReport();
      Console.WriteLine(mr.animojiPath);
      Console.WriteLine(mr.dateOfBirth);
      Console.WriteLine(mr.gender);
      Console.WriteLine(mr.name);
      Console.WriteLine(mr.surname);
      Console.WriteLine(mr.pathology.name);
      Console.WriteLine(mr.pathology.position);
    }
  }
}