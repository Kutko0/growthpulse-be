using Api.Database;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;


[ApiController]
[Route("startup")]
public class StartupController
{
    [HttpGet("All")]
    public string getAll()
    {
        using (var ctx = new StartupContext())
        {
            var startups = ctx.Startup.OrderBy(p => p.Name);
            return startups.First().Name;
        }
    }

    [HttpPost("InsertData")]
    public void InsertData()
    {
        using (var ctx = new StartupContext())
        {
            ctx.Database.EnsureCreated();

            var startup = new Startup
            {
                Name = "DaTester"
            };
            var startup2 = new Startup
            {
                Name = "DaTester2"
            };
            ctx.Startup.Add(startup);
            ctx.Startup.Add(startup2);
            ctx.SaveChanges();
        }
    }
}