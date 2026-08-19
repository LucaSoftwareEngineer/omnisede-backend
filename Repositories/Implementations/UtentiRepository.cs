using Microsoft.EntityFrameworkCore;
using OmniSedeBackend.Models;
using OmniSedeBackend.Repositories.Interfaces;

namespace OmniSedeBackend.Repositories.Implementations;

public class UtentiRepository : Repository<Utenti>, IUtentiRepository
{
    public UtentiRepository(OmnisedeContext context) : base(context) {}
    
    public async Task<Utenti?> GetByEmail(string email)
    {
        Utenti? utenti = await _context.Utentis.Where(u => u.Email == email).FirstOrDefaultAsync();
        return utenti;
    }
}