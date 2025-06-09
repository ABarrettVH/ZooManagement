// using Bogus;
using Microsoft.VisualBasic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using ZooManagement;
using ZooManagementDB;

namespace ZooManagement;

public class FakeData

{
    public static Animal generateFakeAnimalData(Microsoft.EntityFrameworkCore.DbContext context)
    {
        List<string> AnimalSpecies = new List<string> { "lion", "giraffe", "flamingo", "owl", "lizard", "hippopotamus", "aligator", "parrot","python" };
        Animal animal = new Animal();

        // var id = 0;
        // animal.AnimalID = id++;

        Random random = new Random();
        int speciesiter = random.Next(AnimalSpecies.Count);
        animal.Species = AnimalSpecies[speciesiter];

        var enclosures = context.Set<Enclosure>().ToList();

        switch (animal.Species)
        {
            case "lion":
                animal.Classification = "carnivore";
                var lionEnclosure = enclosures.Find(e => e.EnclosureName!.Equals("Lion Enclosure"));
                animal.EnclosureID = lionEnclosure!.EnclosureID;
                break;
            case "giraffe":
                animal.Classification = "mammal";
                var giraffeEnclosure = enclosures.Find(e => e.EnclosureName!.Equals("Giraffe Enclosure"));
                animal.EnclosureID = giraffeEnclosure!.EnclosureID;
                break;
            case "hippopotamus":
                animal.Classification = "mammal";
                var hippoEnclosure = enclosures.Find(e => e.EnclosureName!.Equals("Hippo Enclosure"));
                animal.EnclosureID = hippoEnclosure!.EnclosureID;
                break;
            case "flamingo":
            case "owl":
            case "parrot":
                animal.Classification = "aviary";
                var aviaryEnclosure = enclosures.Find(e => e.EnclosureName!.Equals("Aviary"));
                animal.EnclosureID = aviaryEnclosure!.EnclosureID;
                break;
            case "aligator":
            case "lizard":
            case "python":
                animal.Classification = "reptile";
                var reptileEnclosure = enclosures.Find(e => e.EnclosureName!.Equals("Reptile House"));
                animal.EnclosureID = reptileEnclosure!.EnclosureID;
                break;

        }

        var nameSexMap = new Dictionary<string, string>
        {

            {"ed", "male"},
            {"rob", "male"},
            {"mark", "male"},
            {"peter", "male"},
            {"jj", "male"},
            {"jasper","male"},
            {"jane", "female"},
            {"joy", "female"},
            {"christina", "female"},
            {"harriet", "female"},
            {"aj", "female"},
            {"dolly", "female"}
        };

        string randomNameKey = nameSexMap.Keys.ElementAt(random.Next(nameSexMap.Count));
        animal.Name = randomNameKey;
        animal.Sex = nameSexMap[$"{randomNameKey}"];

        DateOnly startDateDOB = new DateOnly(2010, 1, 1);
        DateOnly endDateDOB = new DateOnly(2015, 12, 31);

        int range = random.Next(1, 10);
        DateOnly randomDOB = startDateDOB.AddDays(random.Next(range));
        randomDOB = startDateDOB.AddMonths(random.Next(range));
        randomDOB = startDateDOB.AddYears(random.Next(range));

        animal.DOB = randomDOB.ToString();

        DateOnly startDateArrivedAtZoo = new DateOnly(2015, 1, 1);
        DateOnly endDateArrivedAtZoo = new DateOnly(2020, 12, 31);

        DateOnly randomArrivedAtZoo = startDateArrivedAtZoo.AddDays(random.Next(range));

        animal.ArrivedAtZoo = randomArrivedAtZoo.ToString();

        DateOnly now = DateOnly.FromDateTime(DateTime.Now);
        DateOnly dobParsed = DateOnly.Parse(animal.DOB);
        animal.Age = (int)(now.DayNumber - dobParsed.DayNumber) / 365;



        return animal;

    }

    public static Enclosure createEnclosure(string enclosureName)
    {
        Enclosure newEnclosure = new Enclosure();
        newEnclosure.EnclosureName = enclosureName;
        
        return newEnclosure;  
    }
}