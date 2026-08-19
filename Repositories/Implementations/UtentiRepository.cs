using OmniSedeBackend.Models;
using OmniSedeBackend.Repositories.Interfaces;

namespace OmniSedeBackend.Repositories.Implementations;

public class UtentiRepository : Repository<Utenti>, IUtentiRepository
{
    public UtentiRepository(OmnisedeContext context) : base(context)
    {
    }
}