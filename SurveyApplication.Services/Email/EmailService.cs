using SurveyApplication.Services.Email.Interfaces;
using System.Net;
using System.Net.Mail;

namespace SurveyApplication.Services.Email;

public class EmailService : IEmailService
{
    private static readonly string fromMail = "laylaylom62jk@gmail.com";
    private static readonly string fromPassword = "rhrqxtrltdbjzjhw";
    private static readonly string hostSmtp = "smtp.gmail.com";
    private static readonly int smtpPort = 587;

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

            using var smtpClient = new SmtpClient(hostSmtp, smtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fromMail, fromPassword)
            };

            await smtpClient.SendMailAsync(message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


}
