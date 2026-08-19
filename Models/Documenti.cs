using System;
using System.Collections.Generic;

namespace OmniSedeBackend.Models;

public partial class Documenti
{
    public long Id { get; set; }

    public string? Descrizione { get; set; }

    public string? NomeFile { get; set; }

    public long? CaricatoDa { get; set; }

    public long? ApprovatoDa { get; set; }

    public DateTime? DataCaricamento { get; set; }

    public DateTime? DataApprovazione { get; set; }

    public DateTime? DataModifica { get; set; }

    public long? SedeId { get; set; }

    public virtual Utenti? ApprovatoDaNavigation { get; set; }

    public virtual Utenti? CaricatoDaNavigation { get; set; }

    public virtual Sede? Sede { get; set; }
}
