using APBD_Final_Project.Exceptions.AuthExceptions;
using APBD_Final_Project.Services.Abstract;
using JWT.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Final_Project.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestModel model)
    {
        try
        {
            return Ok(await authService.Login(model));
        }
        catch (UnauthorizedException ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequestModel model)
    {
        try
        {
            await authService.Register(model);
        }
        catch (UserExistException ex)
        {
            return BadRequest(ex.Message);
        }

        return Created();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequestModel model)
    {
        try
        {
            return Ok(await authService.RefreshToken(model));
        }
        catch (WrongTokenException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}