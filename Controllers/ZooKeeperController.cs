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

}