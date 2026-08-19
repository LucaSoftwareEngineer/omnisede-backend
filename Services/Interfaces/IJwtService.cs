using System.Security.Claims;
using OmniSedeBackend.Models;

namespace OmniSedeBackend.Services.Interfaces;

public interface IJwtService
{
    Task<string> GenerateToken(Utenti utente);
    ClaimsPrincipal? ValidateToken(string token);
}