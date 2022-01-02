using BlogsDEMO.Application.DTOs.Email;
using BlogsDEMO.Application.Exceptions;
using BlogsDEMO.Application.Interfaces;
using BlogsDEMO.Domain.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        public MailSettings MailSettings { get; }
        public ILogger<EmailService> Logger { get; }

        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
        {
            MailSettings = mailSettings.Value;
            Logger = logger;
        }

        public async Task SendAsync(EmailRequest request)
        {
            try
            {
                // create message
                var email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(request.From ?? MailSettings.EmailFrom)
                };
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                var builder = new BodyBuilder
                {
                    HtmlBody = request.Body
                };
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(MailSettings.SmtpHost, MailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(MailSettings.SmtpUser, MailSettings.SmtpPass);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

        public async Task SendMultiAsync(EmailMultiRequest request)
        {
            try
            {
                // create message
                var email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(request.From ?? MailSettings.EmailFrom)
                };
                foreach (var item in request.To)
                {
                    email.To.Add(MailboxAddress.Parse(item));
                }
                email.Subject = request.Subject;
                var builder = new BodyBuilder
                {
                    HtmlBody = request.Body
                };
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(MailSettings.SmtpHost, MailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(MailSettings.SmtpUser, MailSettings.SmtpPass);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }
    }
}