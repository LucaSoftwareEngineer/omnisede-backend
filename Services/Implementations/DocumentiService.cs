using MapsterMapper;
using OmniSedeBackend.Dto.Request;
using OmniSedeBackend.Dto.Response;
using OmniSedeBackend.Exceptions;
using OmniSedeBackend.Models;
using OmniSedeBackend.Repositories.Implementations;
using OmniSedeBackend.Repositories.Interfaces;
using OmniSedeBackend.Services.Interfaces;

namespace OmniSedeBackend.Services.Implementations;

public class DocumentiService : IDocumentiService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUploaderService _uploaderService;

    public DocumentiService(IUnitOfWork unitOfWork, IMapper mapper, IUploaderService uploaderService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _uploaderService = uploaderService;
    }

    public async Task<DocumentiResponse> Create(DocumentCreateRequest request)
    {

        Sede? sede = await _unitOfWork.Sedi.GetByLongIdAsync(request.SedeId);
        if (sede == null) throw new OmniSedeException("Sede non trovata");

        await _uploaderService.Upload(request.DocumentoFile);

        Documenti documenti = _mapper.Map<Documenti>(request);
        documenti.NomeFile = request.DocumentoFile.FileName;

        await _unitOfWork.Documenti.AddAsync(documenti);
        await _unitOfWork.CompleteAsync();

        SedeResponse sedeRes = _mapper.Map<SedeResponse>(sede);
        DocumentiResponse docRes = _mapper.Map<DocumentiResponse>(documenti);
        docRes.Sede = sedeRes;

        return docRes;
    }

    public Task<DocumentiResponse> Approve(DocumentApprovaRequest request)
    {
        throw new NotImplementedException();
    }
}