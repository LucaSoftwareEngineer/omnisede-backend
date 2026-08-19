using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OmniSedeBackend.Config;
using OmniSedeBackend.Exceptions;
using OmniSedeBackend.Models;
using OmniSedeBackend.Repositories.Interfaces;
using OmniSedeBackend.Services.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace OmniSedeBackend.Services.Implementations;

public class JwtService : IJwtService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtSettings _jwtSettings;

    public JwtService(JwtSettings jwtSettings, IUnitOfWork unitOfWork)
    {
        _jwtSettings = jwtSettings;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<string> GenerateToken(Utenti utente)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var ruolo = await _unitOfWork.Ruoli.GetByLongIdAsync(utente.RuoloId.Value);
        
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, utente.Id.ToString()),
            new(ClaimTypes.Name, utente.Email!),
            new(ClaimTypes.Role, ruolo!.Nome!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
 
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
            signingCredentials: credentials
        );
 
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
 
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtSettings.Issuer,
 
            ValidateAudience = true,
            ValidAudience = _jwtSettings.Audience,
 
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
 
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
 
        try
        {
            var principal = handler.ValidateToken(token, validationParameters, out var validatedToken);
            
            if (validatedToken is JwtSecurityToken jwtToken &&
                jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }
 
            throw new OmniSedeException("Invalid token");
        }
        catch
        {
            throw new OmniSedeException("Invalid token");
        }
    }
}