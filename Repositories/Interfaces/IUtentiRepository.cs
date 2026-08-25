using OmniSedeBackend.Models;

namespace OmniSedeBackend.Repositories.Interfaces;

public interface IUtentiRepository : IRepository<Utenti>
{
    public Task<Utenti?> GetByEmail(string email);
    public Task<List<Utenti>> GetBySedeAndRuolo(long idSede, long idRuolo);
}