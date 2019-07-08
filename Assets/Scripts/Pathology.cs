using System;
using Application;

namespace Application {

  public class Pathology {
    public Application.PathologyName name;

    public Position position;

    public Pathology(PathologyName name, Position position) {
      this.name = name;

      if (name == PathologyName.POST_STROKE_HAND || 
        name == PathologyName.POST_STROKE_APHASIA)
        this.position = position;
      else 
        position = Position.NO;
    }

    public Pathology() {
      Array values = Enum.GetValues(typeof(PathologyName));
      this.name = (PathologyName)values
        .GetValue(new Random().Next(values.Length));

      values = Enum.GetValues(typeof(Position));
      if (name == PathologyName.POST_STROKE_HAND || 
        name == PathologyName.POST_STROKE_APHASIA)
        this.position = (Position)values.GetValue(new Random().Next(1, 3));
      else 
        position = Position.NO;
    }
  }
} 