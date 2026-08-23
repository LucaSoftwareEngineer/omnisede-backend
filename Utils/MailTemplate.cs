namespace OmniSedeBackend.Utils;

public static class MailTemplate
{
    public static string NuovoDocumentoDaValidare = ""
        + "Gentile Amministratore, <br>"
        + "la informiamo che è presente un nuovo documento da validare per la sua sede <br>"
        + "Cordialmente <br>"
        + "Il team di Omnisede";

    public static string DocumentoApprovato = ""
        + "Gentile Utente, <br>"
        + "La informiamo che il documento da lei caricato è stato approvato <br>"
        + "Cordialmente <br>"
        + "Il team di Omnisede";
}