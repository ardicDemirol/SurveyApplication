namespace SurveyApplication.Services.Email.Interfaces;
public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string message);
}
