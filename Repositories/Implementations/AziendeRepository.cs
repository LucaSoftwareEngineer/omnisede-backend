using OmniSedeBackend.Models;
using OmniSedeBackend.Repositories.Interfaces;

namespace OmniSedeBackend.Repositories.Implementations;

public class AziendeRepository : Repository<Aziende>, IAziendeRepository
{
    public AziendeRepository(OmnisedeContext context) : base(context)
    {
    }
}