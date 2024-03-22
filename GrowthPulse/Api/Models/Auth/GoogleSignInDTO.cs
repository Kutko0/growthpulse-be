using System.ComponentModel.DataAnnotations;

namespace Api.Models.Auth;

public class GoogleSignInDTO
{
    [Required]
    public string IdToken { get; set; } 
}