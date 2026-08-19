using OmniSedeBackend.Models;

namespace OmniSedeBackend.Services.Interfaces;

public interface IAuthService
{
    Task<Utenti> ValidateCredentials(string email, string password);
}