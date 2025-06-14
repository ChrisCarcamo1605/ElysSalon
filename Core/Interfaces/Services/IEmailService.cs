namespace Core.Interfaces.Services;

public interface IEmailService 
{
    Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true);

    Task SendEmailWithAttachmentAsync(string email, string subject, string htmlMessage, byte[] attachment,
        string fileName);
}