using System;

namespace Application {

  public class Pathology {

        public static string[] pathologyDisplayNames = new string[] {
            "Depression", "Post-stroke, hand", "Post-stroke, aphasia"
        };

    public PathologyName name;

    public Position position;

    public string[] descriptions;

    public Pathology(PathologyName name) {
      Array values = Enum.GetValues(typeof(Position));
      if (name == PathologyName.POST_STROKE_HAND ||
        name == PathologyName.POST_STROKE_APHASIA)
        position = (Position)values.GetValue(new Random().Next(1, 3));
      else
        position = Position.NO;

      descriptions = new string[6];

      descriptions[0] = "I'm always sad beacuse I only see negative things " +
        "happening in my life.\nI can't seem to control this negative " + 
        "feelings anymore... 😭";
      descriptions[1] = "I recently had a stroke and now my " +
        position.ToString().ToLower() + " hand is constantly shaking... 😐";
      descriptions[2] = "POST_STROKE_APHASIA DESCRIPTION";
      descriptions[3] = "OTHER PATHOLOGY DESC";
      descriptions[4] = "LIKE ABOVE";
      descriptions[5] = "LIKE ABOVE";

      this.name = name;
    }

    public Pathology(PathologyName name, Position p)
    {
      if (name == PathologyName.POST_STROKE_HAND ||
        name == PathologyName.POST_STROKE_APHASIA)
        position = p;
      else
        position = Position.NO;
      
      descriptions = new string[6];

      descriptions[0] = "I'm always sad beacuse I only see negative things " +
        "happening in my life.\nI can't seem to control this negative " +
        "feelings anymore... 😭";
      descriptions[1] = "I recently had a stroke and now my " +
        position.ToString().ToLower() + " hand is constantly shaking... 😐";
      descriptions[2] = "POST_STROKE_APHASIA DESCRIPTION";
      descriptions[3] = "OTHER PATHOLOGY DESC";
      descriptions[4] = "LIKE ABOVE";
      descriptions[5] = "LIKE ABOVE";

      this.name = name;
    }

    public Pathology() {
      Array values = Enum.GetValues(typeof(PathologyName));

      name = (PathologyName)values
        .GetValue(new Random().Next(1, 2));

      values = Enum.GetValues(typeof(Position));
      if (name == PathologyName.POST_STROKE_HAND ||
        name == PathologyName.POST_STROKE_APHASIA)
        position = (Position)values.GetValue(new Random().Next(1, 3));
      else
        position = Position.NO;
        
      descriptions = new string[6];

      descriptions[0] = "I'm always sad beacuse I only see negative things " +
        "happening in my life.\nI can't seem to control this negative " +
        "feelings anymore... 😭";
      descriptions[1] = "I recently had a stroke and now my " +
        position.ToString().ToLower() + " hand is constantly shaking... 😐";
      descriptions[2] = "POST_STROKE_APHASIA DESCRIPTION";
      descriptions[3] = "OTHER PATHOLOGY DESC";
      descriptions[4] = "LIKE ABOVE";
      descriptions[5] = "LIKE ABOVE";
    }

    public string getDescription() {
      return descriptions[(int)name];
    }

    public override string ToString(){
      return name + ", " + position;
    }

    public string getName()
        {
            return pathologyDisplayNames[(int)name];
        }
  }
} 