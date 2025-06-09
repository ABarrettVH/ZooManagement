using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
// using ZooManagement.Migrations;
using ZooManagementDB;
using ZooManagement.Models;
using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;

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

        var animals = _context.Animals.ToList();
        return Ok(animals);

    }

    [Route("/animals/pagination")]
    [HttpGet]
    public async Task<ActionResult<PagedResponse<Animal>>> GetAllAnimalsWithPagination([FromQuery] PaginationParams paginationParams)
    {

        var animalsQuery = _context.Animals.AsQueryable();


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

        var pagedResponse = new PagedResponse<Animal>(animals, paginationParams.PageNumber, paginationParams.PageSize, totalAnimalRecords);

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
        // return Ok(animals);

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
            var animals = _context.Animals.ToList();
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
        var animals = _context.Animals.ToList();
        var returnedAnimalList = animals.FindAll(t => t.Species.Equals(species, StringComparison.OrdinalIgnoreCase));
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
        var newAnimal = new Animal { Name = animal.Name.ToLower(), Species = animal.Species.ToLower(), DOB = animal.DOB, Sex = animal.Sex.ToLower(), Classification = animal.Classification.ToLower(), ArrivedAtZoo = animal.ArrivedAtZoo };
        Console.WriteLine(newAnimal.Sex);
        Console.WriteLine(newAnimal.DOB + " " + newAnimal.ArrivedAtZoo);
        var newEnclosure = new Enclosure();
        var enclosures = _context.Enclosure.ToList();

        string pattern = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$";
        
        List<string> AnimalSpecies = new List<string> { "lion", "giraffe", "flamingo", "owl", "lizard", "hippopotamus", "aligator", "parrot","python" };
        if (!AnimalSpecies.Contains(newAnimal.Species)) {
            return BadRequest("Only lions, giraffes, birds, reptiles or hippos are welcome at this zoo");
        }
        else if((newAnimal.Sex != "male") && ( newAnimal.Sex != "female"))  {
            return BadRequest("Enter male or female for sex");
        }
        else if(! Regex.IsMatch(newAnimal.DOB, pattern ) || ! Regex.IsMatch(newAnimal.ArrivedAtZoo, pattern ) ) {
            return BadRequest("Enter date in dd/mm/yyyy format");
        }
        else
        {
           
            switch (newAnimal.Species)
            {
                case "Lion":
                    if (enclosures.Count(x => x.EnclosureName == "lion enclosure") < 2)
                    {
                        newEnclosure.EnclosureName = "lion enclosure";
                        newAnimal.Classification = "carnivore";
                    }
                    else
                    {
                        return BadRequest("Lion Enclosure is full (10)");
                    }
                    break;
                case "flamingo":
                case "owl":
                case "parrot":
                    if (enclosures.Count(x => x.EnclosureName == "aviary") < 50)
                    {
                        newEnclosure.EnclosureName = "aviary";
                        newAnimal.Classification = "aviary";
                    }
                    else
                    {
                        return BadRequest("Aviary is full (50)");
                    }
                    break;
                case "aligator":
                case "lizard":
                case "python":
                    if (enclosures.Count(x => x.EnclosureName == "reptile house") < 40)
                    {
                        newEnclosure.EnclosureName = "reptile house";
                        newAnimal.Classification = "reptile";
                    }
                    else
                    {
                        return BadRequest("reptile house is full (40)");
                    }
                    break;
                case "hippopotamus":
                    if (enclosures.Count(x => x.EnclosureName == "hippo enclosure") < 6)
                    {
                        newEnclosure.EnclosureName = "hippo enclosure";
                        newAnimal.Classification = "mammal";
                    }
                    else
                    {
                        return BadRequest("Hippo Enclosure is full (6)");
                    }
                    break;
                case "giraffe":
                    if (enclosures.Count(x => x.EnclosureName == "giraffe enclosure") < 10)
                    {
                        newEnclosure.EnclosureName = "gireaffe enclosure";
                        newAnimal.Classification = "mammal";
                    }
                    else
                    {
                        return BadRequest("Giraffe Enclosure is full (10)");
                    }
                    break;
            }



        DateOnly now = DateOnly.FromDateTime(DateTime.Now);
        DateOnly dobParsed = DateOnly.Parse(animal.DOB);
        newAnimal.Age = (int)(now.DayNumber - dobParsed.DayNumber) / 365;
        _context.Add(newAnimal);
        _context.Add(newEnclosure);

        _context.SaveChanges();
        return Ok(newAnimal);
        }
    }
}