using Microsoft.AspNetCore.Mvc;
using OmniSedeBackend.Dto.Request;
using OmniSedeBackend.Dto.Response;
using OmniSedeBackend.Services.Interfaces;

namespace OmniSedeBackend.Controllers;

[ApiController]
[Route("api/documenti")]
public class DocumentiController : ControllerBase
{
    private readonly IDocumentiService _documentiService;

    public DocumentiController(IDocumentiService documentiService)
    {
        _documentiService = documentiService;
    }

    [HttpPost("/create")]
    public async Task<ActionResult<DocumentiResponse>> Create([FromForm] DocumentCreateRequest request)
    {
        return await _documentiService.Create(request);
    }
    
}