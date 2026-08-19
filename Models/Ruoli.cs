using System;
using System.Collections.Generic;

namespace OmniSedeBackend.Models;

public partial class Ruoli
{
    public long Id { get; set; }

    public string? Nome { get; set; }

    public virtual ICollection<Utenti> Utentis { get; set; } = new List<Utenti>();
}
