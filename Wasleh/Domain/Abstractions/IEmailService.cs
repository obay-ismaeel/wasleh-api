namespace Wasleh.Domain.Abstractions;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body);
}
