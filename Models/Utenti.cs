using System;
using System.Collections.Generic;

namespace OmniSedeBackend.Models;

public partial class Utenti
{
    public long Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Nome { get; set; }

    public string? Cognome { get; set; }

    public long? RuoloId { get; set; }

    public long? SedeId { get; set; }

    public virtual ICollection<Documenti> DocumentiApprovatoDaNavigations { get; set; } = new List<Documenti>();

    public virtual ICollection<Documenti> DocumentiCaricatoDaNavigations { get; set; } = new List<Documenti>();

    public virtual Ruoli? Ruolo { get; set; }

    public virtual Sede? Sede { get; set; }
}
