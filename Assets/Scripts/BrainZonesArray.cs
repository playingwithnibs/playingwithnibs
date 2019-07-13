using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Application
{
  public class BrainZonesArray {
    public List<BrainZone> brainZones;

    public int countActiveZones;

    public BrainZonesArray(List<BrainZone> brainZonesList) {
      brainZones = brainZonesList;
      
      brainZonesList.ForEach(brainZone => {
        if (brainZone.stimulator.electrodeType != ElectrodeType.NO)
          countActiveZones++;
      });
    }

    public bool contains(BrainZoneNames name, Position pos) {
      BrainZone b = new BrainZone(name, pos);

      return brainZones.Contains(b) && b.isActive();
    }

    public bool containsOnly(BrainZoneNames name, Position pos) {
      return countActiveZones == 1 && contains(name, pos);
    }

    public bool containsOnly(BrainZoneNames[] names, Position[] pos) {
      if (names.Length != pos.Length || countActiveZones != names.Length)
        return false;

      bool check = true;

      for (int i = 0;  i < names.Length; i++) {
        check = check && contains(names[i], pos[i]);
      }

      return check;
    }

    public BrainZone getZone(BrainZoneNames name, Position pos) {
      return brainZones.Find(bz => bz.Equals(name, pos));
    }

    public List<BrainZone> getZones(BrainZoneNames name)
    {
      return brainZones.FindAll(bz => bz.Equals(name, Position.LEFT) 
        || bz.Equals(name, Position.RIGHT));
    }

    public bool isNeutral() {
      return brainZones.TrueForAll(bz => bz.stimulator.electrodeType 
        == ElectrodeType.NEUTRAL || bz.stimulator.electrodeType
        == ElectrodeType.NO);
    }

    public bool isAnodal(BrainZone source, BrainZone destination) {
      if (!containsOnly(
        new[] {source.brainZoneName, destination.brainZoneName}, 
        new[] {source.position, destination.position}))
        return false;

      return source.stimulator.electrodeType == ElectrodeType.POSITIVE &&
        destination.stimulator.electrodeType == ElectrodeType.NEGATIVE;
    }

    public bool isCathodal(BrainZone source, BrainZone destination) {
      if (!containsOnly(
        new[] { source.brainZoneName, destination.brainZoneName },
        new[] { source.position, destination.position }))
        return false;
      return source.stimulator.electrodeType == ElectrodeType.NEGATIVE &&
        destination.stimulator.electrodeType == ElectrodeType.POSITIVE;
    }

    public bool isAnodalOrCathodal() {
      int countPos = 0;
      int countNeg = 0;

      for (int i = 0; i< brainZones.Count; i++) {
        if (brainZones[i].stimulator.electrodeType == ElectrodeType.POSITIVE)
          countPos++;
        else if(brainZones[i].stimulator.electrodeType == ElectrodeType.NEGATIVE)
          countNeg++;
      }

      return countActiveZones == 2 && countPos == 1 && countNeg == 1;
    }

    public bool isUniqueStimulation(ElectrodeName en) {
      return brainZones.TrueForAll(bz => bz.stimulator.electrodeName == en ||
        bz.stimulator.electrodeName == ElectrodeName.NO);
    }

    public bool isMagneticStimulation()
    {
      return brainZones.TrueForAll(bz => bz.stimulator.electrodeName 
        == ElectrodeName.CIRCULAR || bz.stimulator.electrodeName 
        == ElectrodeName.EIGHT || bz.stimulator.electrodeName 
        == ElectrodeName.H || bz.stimulator.electrodeName == ElectrodeName.NO);
    }

    public bool isElectricStimulation()
    {
      return brainZones.TrueForAll(bz => bz.stimulator.electrodeName
        == ElectrodeName.HD || bz.stimulator.electrodeName
        == ElectrodeName.DEFAULT || bz.stimulator.electrodeName 
        == ElectrodeName.NO);
    }

    public bool doesNotContain(BrainZoneNames name) {
      for (int i = 0; i < brainZones.Count; i++) {
        if (brainZones[i].brainZoneName == name && brainZones[i].isActive())
          return false;
      }

      return true;
    }



    public override string ToString() {
      string result = "";

      for (int i = 0; i < 6; i++) {
        result += brainZones[i].brainZoneName + ", " + brainZones[i].position +
          ", " + brainZones[i].stimulator + "\n";
      }

      return result;
    }
  }
}