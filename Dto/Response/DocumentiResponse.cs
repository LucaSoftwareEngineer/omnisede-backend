namespace OmniSedeBackend.Dto.Response;

public class DocumentCreateResponse
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

    public virtual UtentiResponse? ApprovatoDaNavigation { get; set; }

    public virtual UtentiResponse? CaricatoDaNavigation { get; set; }

    public virtual SedeResponse? Sede { get; set; }
}