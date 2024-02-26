using System.Configuration;
using Api.Models;
using Microsoft.EntityFrameworkCore;
namespace Api.Database;

public class StartupContext : DbContext
{
    public DbSet<Startup> Startup { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Fill in your own.
        optionsBuilder.UseMySQL("server=;database=;user=;password=");
    }
    
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