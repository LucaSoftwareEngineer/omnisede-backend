using OmniSedeBackend.Dto.Request;
using OmniSedeBackend.Dto.Response;

namespace OmniSedeBackend.Services.Interfaces;

public interface IDocumentiService
{
    public Task<DocumentiResponse> Create(DocumentCreateRequest request);
    public Task<DocumentiResponse> Approve(DocumentApprovaRequest request);
}