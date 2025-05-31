using CIDRS.Shared.Utility.EmailManipulator.Models;
using CIDRS.Shared.Utility.EmailManipulator.Options;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Shared.Utility.EmailManipulator.Services
{
    /// <summary>
    /// The class Email Sender Service
    /// </summary>
    public class EmailSenderService : IEmailSenderService
    {

        private readonly EmailConfiguration _emailConfig;

        /// <summary>
        /// The Constructor of email sender service
        /// </summary>
        /// <param name="emailConfig">emailConfig</param>
        public EmailSenderService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        /// <summary>
        /// The method of send mail async
        /// </summary>
        /// <param name="message">message</param>
        /// <returns></returns>
        public async Task SendEmailAsync(NotificationMessage message)
        {
            var emailMessage = CreateEmailMessage(message); // Create email message


            await Send(emailMessage);
        }


        #region Eamil Private method
        /// <summary>
        /// The Method Create Email Message
        /// </summary>
        /// <param name="message">message</param>
        /// <returns></returns>
        private MimeMessage CreateEmailMessage(NotificationMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            if (!string.IsNullOrEmpty(_emailConfig.Bcc))
                emailMessage.Bcc.Add(new MailboxAddress(_emailConfig.Bcc));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };


            return emailMessage;
        }

        /// <summary>
        /// The Method send email
        /// </summary>
        /// <param name="mailMessage">mailMessage</param>
        /// <returns></returns>
        private async Task Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.SmtpPort, false);

                    if (!string.IsNullOrEmpty(_emailConfig.SmtpUsername) && !string.IsNullOrEmpty(_emailConfig.SmtpPassword))
                        await client.AuthenticateAsync(_emailConfig.SmtpUsername, _emailConfig.SmtpPassword);

                    await client.SendAsync(mailMessage);
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
        #endregion
    }
}
