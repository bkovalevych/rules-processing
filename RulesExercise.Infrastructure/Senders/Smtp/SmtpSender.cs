using Microsoft.Extensions.Options;
using RulesExercise.Domain.Enums;
using System.Net;
using System.Net.Mail;

namespace RulesExercise.Infrastructure.Senders.Smtp
{
    public class SmtpSender : BaseSender
    {
        private readonly SmtpConfiguration _configuration;
        private readonly SmtpClient _client;

        public SmtpSender(IOptions<SmtpConfiguration> smtpConfiguration)
        {
            _configuration = smtpConfiguration.Value;
            _client = BuildClient(smtpConfiguration.Value);
        }

        internal override Channel Channel => Channel.Smtp;

        public override async Task SendMessageAsync(string header, string message)
        {
            var mail = new MailMessage(
                _configuration.SenderUserName,
                _configuration.Reciever,
                header,
                message);
            await _client.SendMailAsync(mail);
        }

        private SmtpClient BuildClient(SmtpConfiguration smtpConfiguration)
        {
            var smtpClient = new SmtpClient(smtpConfiguration.Host, smtpConfiguration.Port);
            try
            {
                smtpClient.Credentials = new NetworkCredential(
                    smtpConfiguration.SenderUserName,
                    smtpConfiguration.SenderPassword);
                smtpClient.EnableSsl = smtpConfiguration.EnableSsl;
            }
            catch (Exception e)
            {
                // _logger.LogWarning(e, "Failed to build smtp client");
                // TODO Logger
            }
            return smtpClient;
        }
    }
}
