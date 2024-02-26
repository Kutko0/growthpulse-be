using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}