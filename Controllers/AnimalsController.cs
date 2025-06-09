using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
// using ZooManagement.Migrations;
using ZooManagementDB;
using ZooManagement.Models;
using System.Reflection;
using System.Globalization;

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
    [HttpGet]
    public IActionResult GetAllAnimals()
    {

        var animals = _context.Animals.ToList();
        return Ok(animals);

    }

    [Route("GetAllAnimalsWithPagination")]
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

        if (string.IsNullOrEmpty(paginationParams.Category)){
             animalsQuery = animalsQuery.OrderBy(b => b.Species);
        }
        else
        {
            switch (paginationParams.Category.ToLower()){
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

    [Route("GetByAnimalID")]
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
            var animal = animals.FirstOrDefault(u => u.Id == id);
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

    [Route("GetByAnimalType")]
    [HttpGet]
    public IActionResult GetAnimalByType(string type)
    {
        var animals = _context.Animals.ToList();
        var returnedAnimalList = animals.FindAll(t => t.Species.Equals(type, StringComparison.OrdinalIgnoreCase));
        if (returnedAnimalList.Count > 0)
        {
            return Ok(returnedAnimalList);
        }
        else
        {
            return NotFound("Animal species not found");
        }


    }


    [HttpPost]
    public IActionResult AddAnimal(Animal animal)
    {
        var newAnimal = new Animal { Name = animal.Name, Species = animal.Species, DOB = animal.DOB, Sex = animal.Sex, Classification = animal.Classification, ArrivedAtZoo = animal.ArrivedAtZoo };
        _context.Add(newAnimal);
        _context.SaveChanges();
        return Ok(newAnimal);

    }
}