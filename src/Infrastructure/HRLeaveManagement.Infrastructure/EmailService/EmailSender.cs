using HRLeaveManagement.Application.Contracts.Email;
using HRLeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HRLeaveManagement.Infrastructure.EmailService;
public class EmailSender(IOptions<EmailSettings> emailSetting) : IEmailSender
{
    public EmailSettings _emailSetting { get; } = emailSetting.Value;

    public async Task<bool> SendEmail(EmailMessage email)
    {
        var client = new SendGridClient(_emailSetting.ApiKey);
        var to = new EmailAddress(email.To);
        var from = new EmailAddress
        {
            Email = _emailSetting.FromAddress,
            Name = _emailSetting.FromName
        };


        var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);

        var resp = await client.SendEmailAsync(message);

        return resp.IsSuccessStatusCode;
    }
}