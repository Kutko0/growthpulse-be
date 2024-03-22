using Api.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface IAccountService
{
    Task<IActionResult> Login(LoginModel model);
    Task<IActionResult> Register(RegisterModel model);
}