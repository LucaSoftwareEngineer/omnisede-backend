namespace OmniSedeBackend.Config;

public class SmtpConfig
{
    public string Host { get; set; } = string.Empty;
    public string Port { get; set; } = string.Empty;
    public string NameSender { get; set; } = string.Empty;
    public string MailSender { get; set; } = string.Empty;
    public string MailToken { get; set; } = string.Empty;
}