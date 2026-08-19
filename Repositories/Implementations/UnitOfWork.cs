using OmniSedeBackend.Repositories.Interfaces;
using OmniSedeBackend.Models;

namespace OmniSedeBackend.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly OmnisedeContext _context;

    public IAziendeRepository Aziende { get; private set; }
    public IUtentiRepository Utenti { get; private set; }
    public IDocumentiRepository Documenti { get; private set; }
    public IRuoliRepository Ruoli { get; private set; }
    public ISedeRepository Sedi { get; private set; }

    public UnitOfWork(OmnisedeContext context)
    {
        _context = context;
        Aziende = new AziendeRepository(_context);
        Utenti = new UtentiRepository(_context);
        Documenti = new DocumentiRepository(_context);
        Ruoli = new RuoliRepository(_context);
        Sedi = new SedeRepository(_context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}