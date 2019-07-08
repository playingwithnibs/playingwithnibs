using System;
using System.Collections.Generic;
using System.Linq;

using Application;

namespace Application {
  public class ScoreCalculator {

    private const int TIME_BONUS = 20;

    private const int SAVED_TIME_BONUS_MOLTIPLICATOR = 5;

    private const int DEVICE_CONFIG_SCORE_PERCENTAGE = 40;

    public ScoreCalculator() {}

    public double computeMedicalEquipmentScore(Outcome simulationOutcome) {
      return (int)simulationOutcome * DEVICE_CONFIG_SCORE_PERCENTAGE / 100;
    }

    public double computeOutcomeScore(Outcome simulationOutcome) {
      return (int)simulationOutcome;
    }

    // to get the start and end timestamps:
    // https://stackoverflow.com/questions/21219797/how-to-get-correct-timestamp-in-c-sharp/21219819
    public double computeTimeBonus(double simStart, double simEnd) {
        return TIME_BONUS +
          (simEnd - simStart) * SAVED_TIME_BONUS_MOLTIPLICATOR;
      }
  }
}