using OmniSedeBackend.Dto.Request;
using OmniSedeBackend.Dto.Response;
using OmniSedeBackend.Services.Interfaces;

namespace OmniSedeBackend.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IJwtService _jwtService;

    public AuthController(IAuthService authService, IJwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }
    
    [HttpPost("login")]
    public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
    {
        var utente = _authService.ValidateCredentials(request.Email, request.Password);
        var token = _jwtService.GenerateToken(utente.Result);

        return Ok(new LoginResponse
        {
            Token = token.Result,
            ExpiresAt = DateTime.UtcNow.AddMinutes(30),
            Email = utente.Result.Email!,
            Role = utente.Result.Ruolo!.Nome!
        });
    }
}