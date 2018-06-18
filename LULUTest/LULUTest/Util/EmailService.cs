using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using LULUTest.Util.Abstract;

namespace LULUTest.Util
{
    public class EmailService : IEmailService
    {
        private SmtpConfig _config;

        public EmailService(SmtpConfig config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string recepientName, string recepientEmail, string subject, string body, bool isHtml = true)
        {
            var from = new MailboxAddress(_config.Name, _config.EmailAddress);
            var to = new MailboxAddress(recepientName, recepientEmail);

            await SendEmailAsync(from, to, subject, body, _config, isHtml);
        }

        private async Task SendEmailAsync(MailboxAddress sender, MailboxAddress recepient, string subject, string body, SmtpConfig config = null, bool isHtml = true)
        {
            MimeMessage message = new MimeMessage();

            message.From.Add(sender);
            message.To.Add(recepient);
            message.Subject = subject;
            message.Body = isHtml ? new BodyBuilder { HtmlBody = body }.ToMessageBody() : new TextPart("plain") { Text = body };

            if (config == null)
                config = _config;

            using (SmtpClient client = new SmtpClient())
            {
                if (!config.UseSSL)
                    client.ServerCertificateValidationCallback = (object sender2, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;

                await client.ConnectAsync(config.Host, config.Port, config.UseSSL).ConfigureAwait(false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                if (!string.IsNullOrWhiteSpace(config.Username))
                    await client.AuthenticateAsync(config.Username, config.Password).ConfigureAwait(false);

                await client.SendAsync(message).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }

        public class SmtpConfig
        {
            public string Host { get; set; }

            public int Port { get; set; }

            public bool UseSSL { get; set; }

            public string Name { get; set; }

            public string Username { get; set; }

            public string EmailAddress { get; set; }

            public string Password { get; set; }
        }
    }
}