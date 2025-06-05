using Microsoft.AspNetCore.Mvc;
using ZooManagement.Migrations;
using ZooManagementDB;

namespace ZooManagement.Controllers;

[ApiController]
[Route("[controller]")]
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
    
    [HttpPost]
    public IActionResult AddAnimal()
    {

            var animals = _context.Animals.ToList();
            return Ok(animals);
        
    }
}