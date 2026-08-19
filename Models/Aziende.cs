using System;
using System.Collections.Generic;

namespace OmniSedeBackend.Models;

public partial class Aziende
{
    public long Id { get; set; }

    public string? RagioneSociale { get; set; }

    public string? PartitaIva { get; set; }

    public virtual ICollection<Sede> Sedes { get; set; } = new List<Sede>();
}
