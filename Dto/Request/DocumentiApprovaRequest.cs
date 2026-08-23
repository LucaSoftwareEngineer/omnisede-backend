using System.ComponentModel.DataAnnotations;

public class DocumentApprovaRequest
{
    [Required(ErrorMessage = "Non è stato specificato l'id del documento")]
    public long Id { get; set; }

    [Required(ErrorMessage = "Inserire la descrizione")]
    public string Descrizione { get; set; } = string.Empty;

    [Required(ErrorMessage = "Non è stato specificato l'Id dell'utente che sta approvando il file")]
    public long? ApprovatoDa { get; set; }
}