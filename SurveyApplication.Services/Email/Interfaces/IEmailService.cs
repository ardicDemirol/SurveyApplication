namespace SurveyApplication.Services.Email.Interfaces;
public interface IEmailService
{
    void SendSurveyCreatedEmail(string email, string surveyTitle);

    Task SendEmailAsync(string to, string subject, string message);
}
