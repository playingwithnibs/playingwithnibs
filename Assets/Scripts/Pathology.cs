using System;

namespace Application {

  public class Pathology {
    public PathologyName name;

    public Position position;

    public string[] descriptions;

    public Pathology(PathologyName name) {
      descriptions = new string[6];

      descriptions[0] = "DEPRESSION DESCRIPTION";
      descriptions[1] = "POST_STROKE_HAND DESCRIPTION";
      descriptions[2] = "POST_STROKE_APHASIA DESCRIPTION";
      descriptions[3] = "OTHER PATHOLOGY DESC";
      descriptions[4] = "LIKE ABOVE";
      descriptions[5] = "LIKE ABOVE";

      this.name = name;

      Array values = Enum.GetValues(typeof(Position));
      if (name == PathologyName.POST_STROKE_HAND || 
        name == PathologyName.POST_STROKE_APHASIA)
        this.position = (Position)values.GetValue(new Random().Next(1, 3));
      else 
        position = Position.NO;
    }

    public Pathology(PathologyName name, Position p)
    {
      descriptions = new string[6];

      descriptions[0] = "DEPRESSION DESCRIPTION";
      descriptions[1] = "POST_STROKE_HAND DESCRIPTION";
      descriptions[2] = "POST_STROKE_APHASIA DESCRIPTION";
      descriptions[3] = "OTHER PATHOLOGY DESC";
      descriptions[4] = "LIKE ABOVE";
      descriptions[5] = "LIKE ABOVE";

      this.name = name;

      if (name == PathologyName.POST_STROKE_HAND ||
        name == PathologyName.POST_STROKE_APHASIA)
        position = p;
      else
        position = Position.NO;
    }

    public Pathology() {
      descriptions = new string[6];

      descriptions[0] = "DEPRESSION DESCRIPTION";
      descriptions[1] = "POST_STROKE_HAND DESCRIPTION";
      descriptions[2] = "POST_STROKE_APHASIA DESCRIPTION";
      descriptions[3] = "OTHER PATHOLOGY DESC";
      descriptions[4] = "LIKE ABOVE";
      descriptions[5] = "LIKE ABOVE";
      
      Array values = Enum.GetValues(typeof(PathologyName));
      
      name = (PathologyName)values
        .GetValue(new Random().Next(0, 2));

      values = Enum.GetValues(typeof(Position));
      if (name == PathologyName.POST_STROKE_HAND || 
        name == PathologyName.POST_STROKE_APHASIA)
        position = (Position)values.GetValue(new Random().Next(1, 3));
      else 
        position = Position.NO;
    }

    public string getDescription() {
      return descriptions[(int)name];
    }
  }
} 