namespace OmniSedeBackend.Dto.Response;

public class UtentiResponse
{
    public long Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Nome { get; set; }

    public string? Cognome { get; set; }

    public long? RuoloId { get; set; }

    public long? SedeId { get; set; }

    public virtual RuoliResponse? Ruolo { get; set; }

    public virtual SedeResponse? Sede { get; set; }
}