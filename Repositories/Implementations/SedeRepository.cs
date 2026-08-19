using OmniSedeBackend.Models;
using OmniSedeBackend.Repositories.Interfaces;

namespace OmniSedeBackend.Repositories.Implementations;

public class SedeRepository : Repository<Sede>, ISedeRepository
{
    public SedeRepository(OmnisedeContext context) : base(context)
    {
    }
}