using SurveyApplication.Services.Email.Interfaces;
using System.Net;
using System.Net.Mail;

namespace SurveyApplication.Services.Email;

public class EmailService : IEmailService
{
    private static readonly string fromMail = "";
    private static readonly string fromPassword = "rhrqxtrltdbjzjhw";
    private static readonly int smtpPort = 587;

    public void SendSurveyCreatedEmail(string email, string surveyTitle)
    {
        Console.WriteLine($"Sending email to {email} for survey {surveyTitle}");
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            MailMessage message = new()
            {
                From = new MailAddress(fromMail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(to));

            using var smtpClient = new SmtpClient("smtp.gmail.com", smtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fromMail, fromPassword)
            };

            await smtpClient.SendMailAsync(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while sending the email: {ex.Message}");
            throw;
        }
    }


}
