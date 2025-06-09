// using Bogus;
using Microsoft.VisualBasic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using ZooManagement;

namespace ZooManagement;

public class FakeData

{
    public static Animal generateFakeAnimalData()
    {
        List<string> AnimalSpecies = new List<string> { "lion", "giraffe", "flamingo", "owl", "lizard", "hippopotamus", "aligator", "parrot","python" };
        Animal animal = new Animal();

        // var id = 0;
        // animal.AnimalID = id++;

        Random random = new Random();
        int speciesiter = random.Next(AnimalSpecies.Count);
        animal.Species = AnimalSpecies[speciesiter];

        switch (animal.Species)
        {
            case "lion":
                animal.Classification = "carnivore";
                break;
            case "elephant":
            case "giraffe":
            case "hippopotamus":
                animal.Classification = "mammal";
                break;
            case "flamingo":
            case "owl":
            case "parrot":
                animal.Classification = "aviary";
                break;
            case "aligator":
            case "lizard":
            case "python":
                animal.Classification = "reptile";
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

    public static Enclosure createEnclosure()
    {
        Enclosure newEnclosure = new Enclosure();

        List<string> enclosureNames = new List<string> { "Lion Enclosure", "Aviary", "Hippo Enclosure", "Giraffe Enclosure", "Reptile House" };


        foreach (var enclosure in enclosureNames) {
            newEnclosure.EnclosureName = enclosure;
        }
        return newEnclosure;  
    }
}