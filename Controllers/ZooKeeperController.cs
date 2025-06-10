using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ZooManagementDB;
using ZooManagement.Models;
using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SQLitePCL;
using System.Linq.Expressions;
using ZooManagement;

namespace ZooManagement.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ZooKeeperController : ControllerBase
{
    private readonly ILogger<ZooKeeperController> _logger;
    private readonly ZooManagementDBContext _context;
    public ZooKeeperController(ILogger<ZooKeeperController> logger, ZooManagementDBContext context)
    {
        _logger = logger;
        _context = context;
    }


    [Route("/zookeeper/{id}")]
    [HttpGet]
    public IActionResult GetEnclosuresByID(int id)
    {

        if (id.GetType() != typeof(int))
        {
            return BadRequest();
        }
        else
        {
            
            var enclosures = _context.EnclosureZooKeeper
            .Select(x => new ZooKeeperResponse
            {
                ZooKeeperID = x.ZooKeeperID,
                EnclosureName = (from record in _context.Enclosure where record.EnclosureID == x.EnclosureID select record.EnclosureName).ToList(),
                Animals = (from record in _context.Animals where record.EnclosureID == x.EnclosureID select record.AnimalId).ToList(),
                
              })
                            
            .ToList();
            var enclosure = enclosures.FirstOrDefault(u => u.ZooKeeperID== id);
            if (enclosure != null)
            {
                return Ok(enclosure);
            }
            else
            {
                return NotFound("Zoo Keeper ID does not exist");
            }

        }

    }

   

    [Route("/zookeeper")]
    [HttpPost]
    public IActionResult AddZookeeper(CreateZooKeeper zooKeeper)
    {

        ZooKeeper newZooKeeper = new ZooKeeper();
        

        newZooKeeper.ZooKeeperName = zooKeeper.ZooKeeperName!.ToLower();
        var inputEnclosure = zooKeeper.EnclosureName;
        

        var enclosures = _context.Enclosure.ToList();
        var enclosureNames = _context.Enclosure.Select(x => x.EnclosureName).ToList();
        if (enclosureNames.Contains(inputEnclosure))
        {
            newZooKeeper.EnclosureName = inputEnclosure;
            _context.ZooKeeper.Add(newZooKeeper);
            _context.SaveChanges();
            EnclosureZooKeeper newEnclosureZooKeeper = new EnclosureZooKeeper();
            newEnclosureZooKeeper.EnclosureID = _context.Enclosure.FirstOrDefault(x => x.EnclosureName!.Equals(zooKeeper.EnclosureName))!.EnclosureID;
            newEnclosureZooKeeper.ZooKeeperID = newZooKeeper.ZooKeeperID;
            _context.EnclosureZooKeeper.Add(newEnclosureZooKeeper);
            _context.SaveChanges();
        }
        else
        {
            return BadRequest("Enter a valid enclosure");
        }

        return Ok(newZooKeeper);

        


    }

}