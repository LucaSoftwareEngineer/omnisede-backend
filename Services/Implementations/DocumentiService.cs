using MapsterMapper;
using OmniSedeBackend.Dto.Request;
using OmniSedeBackend.Dto.Response;
using OmniSedeBackend.Exceptions;
using OmniSedeBackend.Models;
using OmniSedeBackend.Repositories.Interfaces;
using OmniSedeBackend.Services.Interfaces;
using OmniSedeBackend.Utils;

namespace OmniSedeBackend.Services.Implementations;

public class DocumentiService : IDocumentiService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUploaderService _uploaderService;
    private readonly IEmailService _emailService;

    public DocumentiService(IUnitOfWork unitOfWork, IMapper mapper, IUploaderService uploaderService, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _uploaderService = uploaderService;
        _emailService = emailService;
    }

    public async Task<DocumentiResponse> Create(DocumentCreateRequest request)
    {
        Sede? sede = await _unitOfWork.Sedi.GetByLongIdAsync(request.SedeId);
        if (sede == null) throw new OmniSedeException("Sede non trovata");

        await _uploaderService.Upload(request.DocumentoFile);

        Documenti documenti = _mapper.Map<Documenti>(request);
        documenti.NomeFile = request.DocumentoFile.FileName;
        documenti.DataCaricamento = DateTime.Now;

        await _unitOfWork.Documenti.AddAsync(documenti);
        await _unitOfWork.CompleteAsync();

        SedeResponse sedeRes = _mapper.Map<SedeResponse>(sede);
        DocumentiResponse docRes = _mapper.Map<DocumentiResponse>(documenti);
        docRes.Sede = sedeRes;

        List<Utenti> amministratori = await _unitOfWork.Utenti.GetBySedeAndRuolo(sede.Id, 1L);
        NotifyAmministratori(amministratori);

        return docRes;
    }

    public async Task<DocumentiResponse> Approve(DocumentApprovaRequest request)
    {
        Documenti? documenti = await _unitOfWork.Documenti.GetByLongIdAsync(request.Id);

        if (documenti == null) throw new OmniSedeException("Documento non trovato");

        documenti.ApprovatoDa = request.ApprovatoDa;
        documenti.Descrizione = request.Descrizione;
        documenti.DataApprovazione = DateTime.Now;
        documenti.DataModifica = DateTime.Now;

        await _unitOfWork.CompleteAsync();

        Sede? sede = await _unitOfWork.Sedi.GetByLongIdAsync(documenti.SedeId.Value);
        if (sede == null) throw new OmniSedeException("Sede non trovata");

        SedeResponse sedeRes = _mapper.Map<SedeResponse>(sede);
        DocumentiResponse docRes = _mapper.Map<DocumentiResponse>(documenti);
        docRes.Sede = sedeRes;

        Utenti utente = await _unitOfWork.Utenti.GetByLongIdAsync(documenti.CaricatoDa.Value);
        _emailService.SendEmail(utente.Email, "Documento Approvato", MailTemplate.DocumentoApprovato);   

        return docRes;
    }

    public void NotifyAmministratori(List<Utenti> amministratori)
    {
        if (amministratori != null)
        {
            amministratori.ForEach(a =>
            {
                if (a.Email != null)
                {
                    _emailService.SendEmail(a.Email, "Nuovo Documento da Approvare", MailTemplate.NuovoDocumentoDaValidare);   
                }
            });
        }
    }
}