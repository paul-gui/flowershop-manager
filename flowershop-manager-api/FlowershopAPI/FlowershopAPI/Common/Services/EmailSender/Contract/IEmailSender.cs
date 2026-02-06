using FlowerShopAPI.Common.Results;

namespace FlowerShopAPI.Common.Services.EmailSender.Contract;

public interface IEmailSender
{
    public Task SendEmailAsync(string to, string subject, string body);
}