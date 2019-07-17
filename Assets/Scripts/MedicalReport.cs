using System;

namespace Application {

  public class MedicalReport {
    public PersonName name;

    public PersonSurname surname;

    public DateTime dateOfBirth;

    public Gender gender;

    public Pathology pathology;

    public string animojiPath;

    public MedicalReport() {
      gender = new Random().Next(0, 41) >= 20 ? (Gender) 40 : 0;

      name = (PersonName)Enum
        .GetValues(typeof(PersonName))
        .GetValue(new Random().Next(0, 6) % 3) + (int)gender;

      surname = (PersonSurname)Enum
        .GetValues(typeof(PersonSurname))
        .GetValue(new Random().Next(0, 7));

      DateTime start = new DateTime(1970, 1, 1);
      dateOfBirth = start
        .AddDays(new Random().Next((DateTime.Today - start).Days));

      pathology = new Pathology();

      animojiPath = "Sprites/" + (int) name + "_bad";
    }

    public string getMemojiPathWithOutcome(string outcome)
    {
        return "Sprites/" + (int)name + "_" + outcome.ToLower();
    }

    public override string ToString(){
      return name + ", " + surname + ", " + dateOfBirth + ", " + gender + ", " +
        pathology;
    }
  }
}