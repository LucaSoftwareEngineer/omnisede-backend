using System;
using System.Threading.Tasks;

namespace OmniSedeBackend.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAziendeRepository Aziende { get; }
    IUtentiRepository Utenti { get; }
    IDocumentiRepository Documenti { get; }
    IRuoliRepository Ruoli { get; }
    ISedeRepository Sedi { get; }

    Task<int> CompleteAsync();
}