using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
// using ZooManagement.Migrations;
using ZooManagementDB;
using ZooManagement.Models;
using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SQLitePCL;
using System.Linq.Expressions;

namespace ZooManagement.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class AnimalController : ControllerBase
{
    private readonly ILogger<AnimalController> _logger;
    private readonly ZooManagementDBContext _context;
    public AnimalController(ILogger<AnimalController> logger, ZooManagementDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    // Example endpoint to view all animals in the database
    [Route("/animals")]
    [HttpGet]
    public IActionResult GetAllAnimals()
    {

        // var animals = _context.Animals.ToList();
      
        var animalResponse = _context.Animals
            .Select(x => new AnimalResponse
            {
                AnimalId = x.AnimalId,
                Species = x.Species,
                Classification = x.Classification,
                Name = x.Name,
                Sex = x.Sex,
                DOB = x.DOB,
                ArrivedAtZoo = x.ArrivedAtZoo,
                Age = x.Age,
                EnclosureID = x.EnclosureID, 
                EnclosureName = _context.Enclosure.FirstOrDefault(i => i.EnclosureID == x.EnclosureID).EnclosureName,         
            })
            .ToList();
          
        return Ok(animalResponse);

    }

    [Route("/animals/pagination")]
    [HttpGet]
    public async Task<ActionResult<PagedResponse<Animal>>> GetAllAnimalsWithPagination([FromQuery] PaginationParams paginationParams)
    {

        // var animalsQuery = _context.Animals.AsQueryable();
        var animalsQuery = _context.Animals
        
            .Select(x => new AnimalResponse
            {
                AnimalId = x.AnimalId,
                Species = x.Species,
                Classification = x.Classification,
                Name = x.Name,
                Sex = x.Sex,
                DOB = x.DOB,
                ArrivedAtZoo = x.ArrivedAtZoo,
                Age = x.Age,
                EnclosureID = x.EnclosureID,
                EnclosureName = _context.Enclosure.FirstOrDefault(i => i.EnclosureID == x.EnclosureID).EnclosureName,
            })
            .AsQueryable();

        if (!string.IsNullOrEmpty(paginationParams.SearchTerm))
        {
            int ageSearch;
            bool isAgeSearch = int.TryParse(paginationParams.SearchTerm, out ageSearch);

            animalsQuery = animalsQuery.Where(i =>
                (i.Name != null && i.Name.ToLower().Contains(paginationParams.SearchTerm.ToLower())) ||
                (i.Species != null && i.Species.ToLower().Contains(paginationParams.SearchTerm.ToLower())) ||
                (i.Classification != null && i.Classification.ToLower().Contains(paginationParams.SearchTerm.ToLower())) ||
                (isAgeSearch && i.Age == ageSearch) ||
                (i.ArrivedAtZoo != null && i.ArrivedAtZoo.Contains(paginationParams.SearchTerm))
            );
        }

        if (string.IsNullOrEmpty(paginationParams.orderByCategory)){
             animalsQuery = animalsQuery.OrderBy(b => b.Species);
        }
        else
        {
            switch (paginationParams.orderByCategory.ToLower()){
                case "species":
                    animalsQuery = animalsQuery.OrderBy(b => b.Species);
                    break;
                case "name":
                    animalsQuery = animalsQuery.OrderBy(b => b.Name);
                    break;
                case "classification":
                    animalsQuery = animalsQuery.OrderBy(b => b.Classification);
                    break;
                case "age":
                    animalsQuery = animalsQuery.OrderBy(b => b.Age);
                    break;                
                case "arrival at zoo":
                    animalsQuery = animalsQuery.OrderBy(b => b.ArrivedAtZoo);
                    break;
                default:
                    return BadRequest("Cannot sort by this category - enter Name, Classification, Species, Age or Arrival at zoo");
            }
            }
            
       

        var totalAnimalRecords = await animalsQuery.CountAsync();
        var animals = await animalsQuery.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                                    .Take(paginationParams.PageSize)
                                    .ToListAsync();

        var pagedResponse = new PagedResponse<AnimalResponse>(animals, paginationParams.PageNumber, paginationParams.PageSize, totalAnimalRecords);

        if (paginationParams.PageNumber == 0 || paginationParams.PageSize == 0)
        {
            return BadRequest("Enter a page number and page size");
        }
        else if (pagedResponse.Data.Count == 0)
        {
            return BadRequest("No data for this page");
        }
        else
        {
            return Ok(pagedResponse);
        }
        
    }

    [Route("/animal/{id}")]
    [HttpGet]
    public IActionResult GetAnimalByAnimalId(int id)
    {
        if (id.GetType() != typeof(int))
        {
            return BadRequest();
        }
        else
        {
            var animals = _context.Animals
            .Select(x => new AnimalResponse
            {
                AnimalId = x.AnimalId,
                Species = x.Species,
                Classification = x.Classification,
                Name = x.Name,
                Sex = x.Sex,
                DOB = x.DOB,
                ArrivedAtZoo = x.ArrivedAtZoo,
                Age = x.Age,
                EnclosureID = x.EnclosureID,
                EnclosureName = _context.Enclosure.FirstOrDefault(i => i.EnclosureID == x.EnclosureID).EnclosureName,
            })
            .ToList();
            var animal = animals.FirstOrDefault(u => u.AnimalId == id);
            if (animal != null)
            {
                return Ok(animal);
            }
            else
            {
                return NotFound("Animal ID not found");
            }

        }
    }

    [Route("/animals/findBySpecies/{species}")]
    [HttpGet]
    public IActionResult GetAnimalBySpecies(string species)
    {
        var animals = _context.Animals
            .Select(x => new AnimalResponse
            {
                AnimalId = x.AnimalId,
                Species = x.Species,
                Classification = x.Classification,
                Name = x.Name,
                Sex = x.Sex,
                DOB = x.DOB,
                ArrivedAtZoo = x.ArrivedAtZoo,
                Age = x.Age,
                EnclosureID = x.EnclosureID,
                EnclosureName = _context.Enclosure.FirstOrDefault(i => i.EnclosureID == x.EnclosureID).EnclosureName,
            })
            .ToList();
        var returnedAnimalList = animals.FindAll(t => t.Species!.Equals(species, StringComparison.OrdinalIgnoreCase));
        if (returnedAnimalList.Count > 0)
        {
            return Ok(returnedAnimalList);
        }
        else
        {
            return NotFound("Animal species not found");
        }
    }

    [Route("/animals")]
    [HttpPost]
    public IActionResult AddAnimal(Animal animal)
    {
        var newAnimal = new Animal { Name = animal.Name!.ToLower(), Species = animal.Species!.ToLower(), DOB = animal.DOB, Sex = animal.Sex!.ToLower(), Classification = animal.Classification!.ToLower(), ArrivedAtZoo = animal.ArrivedAtZoo };
        var enclosures = _context.Enclosure.ToList();
        var animals = _context.Animals.ToList();

        string pattern = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$";
        
        List<string> AnimalSpecies = new List<string> { "lion", "giraffe", "flamingo", "owl", "lizard", "hippopotamus", "aligator", "parrot","python" };
        if (!AnimalSpecies.Contains(newAnimal.Species)) {
            return BadRequest("Only lions, giraffes, birds, reptiles or hippos are welcome at this zoo");
        }
        else if((newAnimal.Sex != "male") && ( newAnimal.Sex != "female"))  {
            return BadRequest("Enter male or female for sex");
        }
        else if(! Regex.IsMatch(newAnimal.DOB!, pattern ) || ! Regex.IsMatch(newAnimal.ArrivedAtZoo!, pattern ) ) {
            return BadRequest("Enter date in dd/mm/yyyy format");
        }
        else
        {
           switch (newAnimal.Species)
            {
                case "lion":
                    var lionEnclosure = enclosures.Find(e => e.EnclosureName!.Equals("Lion Enclosure"));
                    if (animals.Count(x => x.EnclosureID.Equals(lionEnclosure!.EnclosureID)) < 200)
                    // if (enclosures.Count(x => x.EnclosureName == "lion enclosure") < 2)
                    {
                        // newEnclosure.EnclosureName = "lion enclosure";
                        newAnimal.Classification = "carnivore";
                        newAnimal.EnclosureID = lionEnclosure!.EnclosureID;
                    }
                    else
                    {
                        return BadRequest("Lion Enclosure is full (10)");
                    }
                    break;
                case "flamingo":
                case "owl":
                case "parrot":
                    var aviaryEnclosure = enclosures.Find(e => e.EnclosureName!.Equals("Aviary"));
                    if (animals.Count(x => x.EnclosureID.Equals(aviaryEnclosure!.EnclosureID)) < 50)
                    // if (enclosures.Count(x => x.EnclosureName == "aviary") < 50)
                    {
                        // newEnclosure.EnclosureName = "aviary";
                        newAnimal.Classification = "aviary";

                        newAnimal.EnclosureID = aviaryEnclosure!.EnclosureID;
                    }
                    else
                    {
                        return BadRequest("Aviary is full (50)");
                    }
                    break;
                case "aligator":
                case "lizard":
                case "python":
                    var reptileEnclosure = enclosures.Find(e => e.EnclosureName!.Equals("Reptile House"));
                    if (animals.Count(x => x.EnclosureID.Equals(reptileEnclosure!.EnclosureID)) < 40)
                    // if (enclosures.Count(x => x.EnclosureName == "reptile house") < 40)
                    {
                        // newEnclosure.EnclosureName = "reptile house";
                        newAnimal.Classification = "reptile";
                        
                        newAnimal.EnclosureID = reptileEnclosure!.EnclosureID;
                    }
                    else
                    {
                        return BadRequest("reptile house is full (40)");
                    }
                    break;
                case "hippopotamus":
                    var hippoEnclosure = enclosures.Find(e => e.EnclosureName!.Equals("Hippo Enclosure"));
                    if (animals.Count(x => x.EnclosureID.Equals(hippoEnclosure!.EnclosureID)) < 6)
                    // if (enclosures.Count(x => x.EnclosureName == "hippo enclosure") < 6)
                    {
                        // newEnclosure.EnclosureName = "hippo enclosure";
                        newAnimal.Classification = "mammal";
                        newAnimal.EnclosureID = hippoEnclosure!.EnclosureID;
                    }
                    else
                    {
                        return BadRequest("Hippo Enclosure is full (6)");
                    }
                    break;
                case "giraffe":
                    var giraffeEnclosure = enclosures.Find(e => e.EnclosureName!.Equals("Giraffe Enclosure"));
                    if (animals.Count(x => x.EnclosureID.Equals(giraffeEnclosure!.EnclosureID)) < 10)
                    // if (enclosures.Count(x => x.EnclosureName == "giraffe enclosure") < 10)
                    {
                        // newEnclosure.EnclosureName = "gireaffe enclosure";
                        newAnimal.Classification = "mammal";

                        newAnimal.EnclosureID = giraffeEnclosure!.EnclosureID;
                    }
                    else
                    {
                        return BadRequest("Giraffe Enclosure is full (10)");
                    }
                    break;
            }



        DateOnly now = DateOnly.FromDateTime(DateTime.Now);
        DateOnly dobParsed = DateOnly.Parse(animal.DOB!);

        newAnimal.Age = (int)(now.DayNumber - dobParsed.DayNumber) / 365;
        
        _context.Add(newAnimal);
        _context.SaveChanges();
            var id = newAnimal.AnimalId;
            var createdAnimal = _context.Animals.Select(x => new AnimalResponse
            {
                AnimalId = x.AnimalId,
                Species = x.Species,
                Classification = x.Classification,
                Name = x.Name,
                Sex = x.Sex,
                DOB = x.DOB,
                ArrivedAtZoo = x.ArrivedAtZoo,
                Age = x.Age,
                EnclosureID = x.EnclosureID,
                EnclosureName = _context.Enclosure.FirstOrDefault(i => i.EnclosureID == x.EnclosureID).EnclosureName,
            }).Where(x => x.AnimalId == id);            
        return Ok(createdAnimal);
        }
    }
}