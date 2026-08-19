using System.ComponentModel.DataAnnotations;

namespace OmniSedeBackend.Dto.Request;

public class LoginRequest
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = String.Empty;
}