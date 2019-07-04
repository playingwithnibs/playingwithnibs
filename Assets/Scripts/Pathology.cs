using System;
using Application;

namespace Application {
  public enum PathologyName { 
  DEPRESSION, 
  MIGRAIN,
  PARKINSON,
  POST_STROKE_HAND,
  TINNITUS,
  AUDITORY_ALLUCINATION,
  POST_STROKE_APHASIA
  }

  public class Pathology {
    public PathologyName name;

    public Position position;

    public Pathology(PathologyName name, Position position) {
      this.name = name;
      this.position = position;
    }
  }
} 