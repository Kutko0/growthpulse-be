using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Startup : BaseEntity
{
    public string Name { get; set; }
}