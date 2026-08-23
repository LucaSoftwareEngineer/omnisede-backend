using System.ComponentModel.DataAnnotations;

namespace OmniSedeBackend.Dto.Request;

public class DocumentCreateRequest
{
    [Required(ErrorMessage = "Inserire la descrizione")]
    public string Descrizione { get; set; } = string.Empty;

    [Required(ErrorMessage = "Non è stato specificato l'utente che crea il documento")]
    public long CaricatoDa { get; set; }

    [Required(ErrorMessage = "Non è stata specificata la sede a cui deve essere associato il documento")]
    public long SedeId { get; set; }
}