using System;
using System.Collections.Generic;
using Application;

namespace Application {
  public class SimulationSolution {

    public Boolean isTms(MedicalEquipment medicalEquipment) {
      return medicalEquipment is Tms;
    }

    private Boolean isTdcs(MedicalEquipment medicalEquipment) {
      return medicalEquipment is Tdcs;
    }

    private Outcome getOutcomeDepression(MedicalEquipment medicalEquipment,
      Pathology pathology) {
        //if (medicalEquipment)

        return Outcome.BAD;
      }

    private Outcome getOutcomePostStrokeHand(MedicalEquipment medicalEquipment,
      Pathology pathology) {
        return Outcome.BAD;
    }

    // private string getOutcomeDepression(string equipment, string pathology,
    //   string unitMeasure, string intensity, string pulses,
    //   string stimulationType, string brainZones) {
    //     // 1
    //     if (equipment.Equals("TMS8") && insentity >= 90 && intensity <= 119
    //       && unitMeasure.Equals("%") && pulses.Equals("HIGH") 
    //       && stimulationType.Equals("NONE") && brainZones.Equals("DLPFC"))
    //       return "GOOD";

    //     // 1
    //     else if (equipment.Equals("TMS8") && intensity == 120
    //       && unitMeasure.Equals("%") && pulses.Equals("HIGH")
    //       && stimulationType.Equals("NONE") && brainZones.Equals("DLPFC"))
    //      return "VERY GOOD";

    //     // 2
    //     else if (!equipment.Equals("TMS8") && intensity <= 120
    //       && !unitMeasure.isNullOrEmpty() && pulses.Equals("LOW")
    //       && stimulationType.Equals("NONE") && brainZones.Equals("DLPFC"))
    //       return "UNCHANGED"; 

    //     // 3
    //     else if (isCoilEquipment(equipment) && intensity <= 120 && 
    //       !unitMeasure.isNullOrEmpty() && pulses.isNullOrEmpty() &&
    //       ((isCoilEquipment(equipment) && stimulationType.isNullOrEmpty()) 
    //       || (!isCoilEquipment(equipment) && 
    //         isElectricStimulation(stimulationType))))
    //       return "EXPLOSION";

    //     // 4
    //     else if (isCoilEquipment(equipment) && intensity <= 120 &&
    //       !unitMeasure.isNullOrEmpty() && pulses.isNullOrEmpty() && 
    //       !stimulationType.isNullOrEmpty() && !brainZones.isNullOrEmpty())
    //       return "EXPLOSION";

    //     // 5
    //     else if (!isCoilEquipment(equipment) && intensity <= 120 && 
    //       !unitMeasure.isNullOrEmpty() && !pulses.isNullOrEmpty() &&
    //       !stimulationType.isNullOrEmpty() && !brainZones.isNullOrEmpty())
    //       return "EXPLOSION";

    //     else
    //       return "UNCHANGED";
    // }

    // private string getOutcomePostStroke(string equipment, string pathology,
    //   string unitMeasure, string intensity, string pulses,
    //   string stimulationType, Position lesionLocation, BrainZone brainZone) {
    //     //1
    //     if (equipment.Equals("TDCS") && intensity >= 0.8 && intensity < 2 && 
    //       unitMeasure.Equals("mA") && pulses.isNullOrEmpty() &&
    //       brainZoneNegative.Equals("M1") && brainZonePositive.Equals("SO") &&
    //       !brainZonePositivePosition.Equals(lesionLocation))
    //       return "GOOD";

    //     //1
    //     else if (equipment.Equals("TDCS") && intensity == 2 &&
    //       unitMeasure.Equals("mA") && pulses.isNullOrEmpty() &&
    //       brainZoneNegative.Equals("M1") && brainZonePositive.Equals("SO") &&
    //       !brainZonePositivePosition.Equals(lesionLocation))
    //       return "VERY GOOD";

    //     //2
    //     else if (equipment.Equals("TDCS") && intensity >= 0.8 && intensity < 2 &&
    //       unitMeasure.Equals("mA") && pulses.isNullOrEmpty() && 
    //       brainZonePositive.Equals("M1") && brainZoneNegative.Equals("SO") &&
    //       brainZonePositivePosition.Equals(lesionLocation))
    //       return "GOOD";

    //   //3
    //   else if (equipment.Equals("TDCS") && intensity < 0.8 &&
    //     unitMeasure.Equals("mA") && pulses.isNullOrEmpty() &&
    //     brainZoneNegative.Equals("M1") && brainZonePositive.Equals("SO") &&
    //     brainZonePositivePosition.Equals(lesionLocation))
    //     return "BAD";    

    //   //4
    //   else if (equipment.Equals("TDCS") && intensity < 0.8 &&
    //     unitMeasure.Equals("mA") && pulses.isNullOrEmpty() && 
    //     brainZonePositive.Equals("M1") && brainZoneNegative.Equals("SO") &&
    //     !brainZonePositivePosition.Equals(lesionLocation))
    //     return "BAD";  

    //     //5
    //     //else if (equipment.Equals("TDCSHD") && intensity < 2 &&
    //     //unitMeasure.Equals("mA") && pulses.isNullOrEmpty() && )
    // }

    public Outcome getOutcome(MedicalEquipment medicalEquipment, 
      Pathology pathology) {

      return pathology.name == PathologyName.DEPRESSION ? 
        getOutcomeDepression(medicalEquipment, pathology) :
        getOutcomePostStrokeHand(medicalEquipment, pathology);
      }
  }
}
