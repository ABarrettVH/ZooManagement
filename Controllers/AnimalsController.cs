using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
// using ZooManagement.Migrations;
using ZooManagementDB;
using ZooManagement.Models;

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
        var totalAnimalRecords = await animalsQuery.CountAsync();
         var animals = await animalsQuery.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                                    .Take(paginationParams.PageSize)
                                    .ToListAsync();

        var pagedResponse = new PagedResponse<Animal>(animals, paginationParams.PageNumber, paginationParams.PageSize, totalAnimalRecords);

        if (paginationParams.PageNumber == 0 || paginationParams.PageSize == 0 ) {
            return BadRequest("Enter a page number and page size");
        }
        else if (pagedResponse.Data.Count == 0){
            return BadRequest("No data for this page");
        }
        else {
            return Ok(pagedResponse);
        }
        // return Ok(animals);

    }

    [Route("GetByAnimalID")]
    [HttpGet]
    public IActionResult GetAnimalByAnimalId(int id)
    {
        if (id.GetType() != typeof(int)) {
            return BadRequest();
        }
        else {
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
        var returnedAnimalList = animals.FindAll(t => t.Species.Equals(type,StringComparison.OrdinalIgnoreCase)); 
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
            var newAnimal = new Animal { Name = animal.Name, Species = animal.Species, DOB = animal.DOB, Sex= animal.Sex, Classification=animal.Classification, ArrivedAtZoo= animal.ArrivedAtZoo };
            _context.Add(newAnimal);
            _context.SaveChanges();
            return Ok(newAnimal);
        
    }
}