using System;
using Application;

namespace Application {

  public class Pathology {
    public Application.PathologyName name;

    public Position position;

    public Pathology(PathologyName name, Position position) {
      this.name = name;
      this.position = position;
    }
  }
} 