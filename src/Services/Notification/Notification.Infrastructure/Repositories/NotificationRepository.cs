using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Notification.Application.Contracts;
using Notification.Domain.Entities;
using Notification.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Notification.Infrastructure.Utilities.Helper;

namespace Notification.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<NotificationRepository> _logger;

        public NotificationRepository(IConfiguration configuration, ILogger<NotificationRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SendEmail(NotificationEntity notificationEntity)
        {
            MailMessage mm = new MailMessage(_configuration.GetValue<string>("EmailSettings:FromAddress"), notificationEntity != null ? notificationEntity.Email : "bharath.kosuri@qentelli.com");

            //instead of sending the subject and body for mail from usermanagement API to we can construct here!

            string url = string.Empty, userName = string.Empty, emailType = string.Empty;
            if (notificationEntity != null)
            {
                emailType = notificationEntity.EmailType;
                string redirectUrl = GetRedirectMailUrl(emailType);
                url = @redirectUrl + notificationEntity.Email + "&token="
                          + notificationEntity.ConfirmationToken;
                userName = notificationEntity.Email!=null? (notificationEntity.Email.Split('@')[0]).ToUpper():"Subscriber";
            }

            //mm.Body = notificationEntity != null ? @"http://localhost:5000?email=" + notificationEntity.Email +
            //                                                                      "&token=" + notificationEntity.ConfirmationToken : notificationEntity.Message;
            mm.Subject = MailSubject(emailType);
            var emailBody = CreateEmailBody(emailType,userName, url, "",notificationEntity.Email);
            mm.Body = emailBody;
            mm.IsBodyHtml = true;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = _configuration.GetValue<string>("EmailSettings:Host");
            smtp.EnableSsl = _configuration.GetValue<bool>("EmailSettings:SSL"); ;
            smtp.UseDefaultCredentials = false;

            NetworkCredential NetworkCred = new NetworkCredential(_configuration.GetValue<string>("EmailSettings:UserName"), _configuration.GetValue<string>("EmailSettings:Password"));
            smtp.Credentials = NetworkCred;
            smtp.Port = _configuration.GetValue<int>("EmailSettings:Port");
            await smtp.SendMailAsync(mm);

            _logger.LogInformation("Email sent.");
        }

        private string MailSubject(string emailType)
        {
            EmailType email = (EmailType)Enum.Parse(typeof(EmailType), emailType);
            switch (email)
            {
                
                case EmailType.Registration:
                    return "Ortho-Q Registration";
                case EmailType.Subscription:
                    return "Ortho-Q Subscription";
                case EmailType.ForgotPassword:
                    return "Ortho-Q Forgot Password";
                default:
                    return "Welcome to Ortho-Q";
            }
        }

        private string GetRedirectMailUrl(string emailType)
        {
            EmailType email = (EmailType)Enum.Parse(typeof(EmailType), emailType);
            switch (email)
            {

                case EmailType.Registration:
                    return "http://localhost:3000/registration/verified?email=";
                case EmailType.Subscription:
                    return "http://localhost:3000/subscriber/verified?email=";
                case EmailType.ForgotPassword:
                    return "http://localhost:3000/reset/password?email=";
                default:
                    return "Welcome to Ortho-Q";
            }
        }
    }
}
