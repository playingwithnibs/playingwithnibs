using System;
using System.Collections.Generic;
using System.Linq;

using Application;

namespace Application {
  public class ScoreCalculator {

    private const int BONUS_TIME = 20;

    private const int MALUS_MOLTIPLICATOR = 5;

    public ScoreCalculator() {}

    public double computeScore(Outcome simulationOutcome, 
      DateTime simulationStartTime, DateTime simulationEndTime) {
      
      return (int)simulationOutcome + BONUS_TIME - 
        (simulationEndTime - simulationStartTime) * 5;
    }
  }
}