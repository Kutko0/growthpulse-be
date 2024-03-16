using System.Configuration;
using Api.Models;
using Microsoft.EntityFrameworkCore;
namespace Api.Database;

public class StartupContext : DbContext
{
    public StartupContext(DbContextOptions<StartupContext> options) : base(options)
    {
    }
    
    
    public DbSet<Startup> Startup { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Startup>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
        });
    }
    
}