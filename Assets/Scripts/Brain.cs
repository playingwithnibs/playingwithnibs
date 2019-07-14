using System.Collections.Generic;

namespace Application
{
  public class Brain {
    private Dictionary<BrainZone, Stimulator> brain;

    public Brain(List<BrainZone> brain) {
      brain.ForEach(brainZone => {
        this.brain.Add(brainZone, brainZone.stimulator);
      });
    }

    public bool containsZone(BrainZoneNames name, Position pos) {
      return brain.ContainsKey(new BrainZone(name, pos));
    }
  }
}