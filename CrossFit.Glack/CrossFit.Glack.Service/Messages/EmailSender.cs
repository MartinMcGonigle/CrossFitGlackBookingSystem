using CrossFit.Glack.Repository.Wrapper;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;

namespace CrossFit.Glack.Service.Messages
{
    public class EmailSender : IEmailSender
    {
        private readonly IRepositoryWrapper repositoryWrapper;

        public EmailSender(IRepositoryWrapper repositoryWrapper)
        {
            this.repositoryWrapper = repositoryWrapper;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailServer = repositoryWrapper.MailServerRepository.FindByCondition(x => x.Active).FirstOrDefault();
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("CrossFit Glack", mailServer.MailServerUserName));
                message.To.Add(new MailboxAddress("", email));

                message.Subject = subject;

                message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = htmlMessage
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(mailServer.MailServerIP, mailServer.MailServerPort, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(mailServer.MailServerUserName, mailServer.MailServerPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}