using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using OmniSedeBackend.Config;
using OmniSedeBackend.Services.Interfaces;

namespace OmniSedeBackend.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly SmtpConfig _smtpConfig;

    public EmailService(SmtpConfig smtpConfig)
    {
        _smtpConfig = smtpConfig;    
    }

    public async Task SendEmail(string toMail, string subject, string body)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(_smtpConfig.NameSender, _smtpConfig.MailSender));
        message.To.Add(MailboxAddress.Parse(toMail));

        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = body
        };
        message.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();

        await client.ConnectAsync(_smtpConfig.Host, int.Parse(_smtpConfig.Port), SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_smtpConfig.MailSender, _smtpConfig.MailToken);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}