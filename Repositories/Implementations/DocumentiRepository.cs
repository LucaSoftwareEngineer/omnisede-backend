using OmniSedeBackend.Models;
using OmniSedeBackend.Repositories.Interfaces;

namespace OmniSedeBackend.Repositories.Implementations;

public class DocumentiRepository : Repository<Documenti>, IDocumentiRepository
{
    public DocumentiRepository(OmnisedeContext context) : base(context)
    {
    }
}