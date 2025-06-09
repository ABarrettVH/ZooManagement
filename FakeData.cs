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
        List<string> AnimalSpecies = new List<string> { "Tiger", "Elephant", "Lion", "Giraffe", "Flamingo", "Owl" };
        Animal animal = new Animal();

        // var id = 0;
        // animal.AnimalID = id++;

        Random random = new Random();
        int speciesiter = random.Next(AnimalSpecies.Count);
        animal.Species = AnimalSpecies[speciesiter];

        switch (animal.Species)
        {
            case "Tiger":
            case "Lion":
                animal.Classification = "carnivore";
                break;
            case "Elephant":
            case "Giraffe":
                animal.Classification = "mammal";
                break;
            case "Flamingo":
            case "Owl":
                animal.Classification = "aviary";
                break;

        }

        var nameSexMap = new Dictionary<string, string>
        {

            {"Ed", "male"},
            {"Rob", "male"},
            {"Mark", "male"},
            {"Peter", "male"},
            {"JJ", "male"},
            {"Jasper","male"},
            {"Jane", "female"},
            {"Joy", "female"},
            {"Christina", "female"},
            {"Harriet", "female"},
            {"AJ", "female"},
            {"Dolly", "female"}
        };

        string randomNameKey = nameSexMap.Keys.ElementAt(random.Next(nameSexMap.Count));
        animal.Name = randomNameKey;
        animal.Sex = nameSexMap[$"{randomNameKey}"];       

        DateOnly startDateDOB = new DateOnly(2010, 1, 1);
        DateOnly endDateDOB = new DateOnly(2015, 12, 31);

        int range = random.Next(1, 10);
        DateOnly randomDOB = startDateDOB.AddDays(random.Next(range));

        animal.DOB = randomDOB.ToString();

        DateOnly startDateArrivedAtZoo = new DateOnly(2015, 1, 1);
        DateOnly endDateArrivedAtZoo = new DateOnly(2020, 12, 31);
        
        DateOnly randomArrivedAtZoo = startDateArrivedAtZoo.AddDays(random.Next(range));

        animal.ArrivedAtZoo = randomArrivedAtZoo.ToString();

        DateOnly now = DateOnly.FromDateTime(DateTime.Now);
        DateOnly dobParsed = DateOnly.Parse(animal.DOB);
        animal.Age = (int)( now.DayNumber - dobParsed.DayNumber)/365;
        


        return animal;

    }   
}