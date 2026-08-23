namespace OmniSedeBackend.Services.Interfaces;

public interface IEmailService
{
    public Task SendEmail(string toMail, string subject, string body);
}