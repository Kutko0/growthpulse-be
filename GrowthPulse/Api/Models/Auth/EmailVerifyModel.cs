using System.ComponentModel.DataAnnotations;

namespace Api.Models.Auth;

public class EmailVerifyModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Verification code is required")]
    public string VerificationCode { get; set; }
}