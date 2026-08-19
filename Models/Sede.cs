using System;
using System.Collections.Generic;

namespace OmniSedeBackend.Models;

public partial class Sede
{
    public long Id { get; set; }

    public string? Indirizzo { get; set; }

    public long? AziendaId { get; set; }

    public virtual Aziende? Azienda { get; set; }

    public virtual ICollection<Documenti> Documentis { get; set; } = new List<Documenti>();

    public virtual ICollection<Utenti> Utentis { get; set; } = new List<Utenti>();
}
