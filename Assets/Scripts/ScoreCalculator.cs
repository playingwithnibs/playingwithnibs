using System;
using System.Collections.Generic;
using System.Linq;

using Application;

namespace Application {
  public class ScoreCalculator {

    private const int BONUS_TIME = 20;

    private const int MALUS_MOLTIPLICATOR = 5;

    public ScoreCalculator() {}

    // to get the start and end timestamps:
    // https://stackoverflow.com/questions/21219797/how-to-get-correct-timestamp-in-c-sharp/21219819
    public double computeScore(Outcome simulationOutcome, 
      DateTime simulationStartTime, DateTime simulationEndTime) {
      
      return (int)simulationOutcome + BONUS_TIME - 
        (simulationEndTime - simulationStartTime) * 5;
    }
  }
}