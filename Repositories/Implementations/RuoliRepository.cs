using OmniSedeBackend.Models;
using OmniSedeBackend.Repositories.Interfaces;

namespace OmniSedeBackend.Repositories.Implementations;

public class RuoliRepository : Repository<Ruoli>, IRuoliRepository
{
    public RuoliRepository(OmnisedeContext context) : base(context)
    {
    }
}