using Microsoft.AspNetCore.Identity;
using OmniSedeBackend.Exceptions;
using OmniSedeBackend.Models;
using OmniSedeBackend.Repositories.Interfaces;
using OmniSedeBackend.Services.Interfaces;

namespace OmniSedeBackend.Services.Implementations;

public class AuthService : IAuthService
{
    private IUnitOfWork _unitOfWork;
    private readonly PasswordHasher<Utenti> _passwordHasher = new();

    public AuthService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
 
    public async Task<Utenti> ValidateCredentials(string email, string password)
    {
        Utenti? utente = await _unitOfWork.Utenti.GetByEmail(email);

        if (utente is null)
        {
            throw new OmniSedeException("Email non valida.");
        }

        var result = _passwordHasher.VerifyHashedPassword(utente, utente.Password, password);

        if (result == PasswordVerificationResult.Failed)
        {
            throw new OmniSedeException("Credenziali non valide.");
        }

        return utente;
    }
}