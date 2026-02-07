using System.Net;
using System.Net.Mail;
using FlowerShopAPI.Common.Configuration;
using FlowerShopAPI.Common.Results;
using FlowerShopAPI.Common.Services.EmailSender.Contract;
using Microsoft.Extensions.Options;

namespace FlowerShopAPI.Common.Services.EmailSender;

public class EmailSender(IOptions<EmailSettings> emailSettings) : IEmailSender
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var message = new MailMessage();
        message.From = new MailAddress(emailSettings.Value.From, emailSettings.Value.FromName);
        message.To.Add(to);
        message.Subject = subject;
        message.Body = body;
        
        var client = new SmtpClient(emailSettings.Value.Host)
        {
            Port = emailSettings.Value.Port,
            Credentials = new NetworkCredential(
                emailSettings.Value.Username, 
                emailSettings.Value.Password
            ),
            EnableSsl = true
        };

        await client.SendMailAsync(message);
    }
}