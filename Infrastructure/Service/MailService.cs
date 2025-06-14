using Core.Interfaces.Services;
using MailKit;

namespace Infrastructure.Service;

using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

public class MailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<MailService> _logger;

    public MailService(IOptions<EmailSettings> emailSettings, ILogger<MailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromAddress));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;

            var builder = new BodyBuilder();
            if (isHtml) builder.HtmlBody = body;
            else builder.TextBody = body;

            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();

            // Configuración especial para Gmail
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            client.CheckCertificateRevocation = false;

            await client.ConnectAsync(
                _emailSettings.SmtpServer,
                _emailSettings.SmtpPort,
                SecureSocketOptions.StartTls);

            // Autenticación explícita con parámetros nombrados
            await client.AuthenticateAsync(
                userName: _emailSettings.SmtpUsername,
                password: _emailSettings.SmtpPassword);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation($"Email sent to {toEmail}");
        }
        catch (AuthenticationException authEx)
        {
            _logger.LogError(authEx, $"Fallo de autenticación. Verifica:");
            _logger.LogError("- ¿Activaste contraseñas de aplicación? https://myaccount.google.com/apppasswords");
            _logger.LogError("- ¿Permitiste acceso a apps menos seguras? https://myaccount.google.com/lesssecureapps");
            throw new Exception("Error de autenticación. Verifica la configuración SMTP.", authEx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error enviando email a {toEmail}");
            throw;
        }
    }

    public async Task SendEmailWithAttachmentAsync(string toEmail, string subject, string body, byte[] fileBytes, string fileName)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromAddress));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = body
            };

            // Adjuntar archivo
            builder.Attachments.Add(fileName, fileBytes);

            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation($"Email with attachment sent to {toEmail}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error sending email with attachment to {toEmail}");
            throw;
        }
    }

}