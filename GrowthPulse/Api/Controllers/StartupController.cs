using Api.Database;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;


[ApiController]
[Route("startup")]
public class StartupController : ControllerBase
{
    private readonly StartupContext _context;

    public StartupController(StartupContext context)
    {
        _context = context;
    }

    // GET: api/v1/Startup
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Startup>>> GetStartups()
    {
        return await _context.Startup.ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Startup>> GetStartup(int id)
    {
        var startup = await _context.Startup.FindAsync(id);

        if (startup == null)
        {
            return NotFound();
        }

        return startup;
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStartup(int id, StartupDTO startupDTO)
    {
        if (!StartupExists(id)) return NotFound();
        var startup = await _context.Startup.FindAsync(id);

        startup!.Name = startupDTO.Name;

        _context.Entry(startup).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StartupExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
    
    [HttpPost]
    public async Task<ActionResult<Startup>> PostStartup(StartupDTO startupDTO)
    {
        var startupEntity = new Startup()
        {
            Name = startupDTO.Name
        };
        _context.Startup.Add(startupEntity);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStartup), new { id = startupEntity.Id }, startupEntity);
    }
    
    private bool StartupExists(int id)
    {
        return _context.Startup.Any(e => e.Id == id);
    }
    
    [HttpPost("InsertTestData")]
    public void InsertData()
    {
        _context.Database.EnsureCreated();

        var startup = new Startup
        {
            Name = "DaTester"
        };
        var startup2 = new Startup
        {
            Name = "DaTester2"
        };
        _context.Startup.Add(startup);
        _context.Startup.Add(startup2);
        _context.SaveChanges();
    }
}