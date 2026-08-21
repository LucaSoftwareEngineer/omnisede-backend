namespace OmniSedeBackend.Services.Interfaces;

public interface IUploaderService
{
    public Task Upload(IFormFile file);
}