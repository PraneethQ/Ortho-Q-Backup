using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.Infrastructure.Utilities
{
    public static class Helper
    {
        public static string CreateEmailBody(string emailType, string userName, string title, string message,string email)
        {
            string body = string.Empty;

            //var path = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates\\htmlpage.html");
            //loading the html template based on the emailType
            var path = LoadHtmlBodyTemplate(emailType);

            //using streamreader for reading my htmltemplate   
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName); //replacing the required things  
            body = body.Replace("{Title}", title);
            body = body.Replace("{message}", message);
            body = body.Replace("{Email}", email);

            return body;
        }

        private static string LoadHtmlBodyTemplate(string emailType)
        {
            //string to enum
            EmailType email = (EmailType)Enum.Parse(typeof(EmailType), emailType);

            switch (email)
            {
                case EmailType.Registration:
                    return Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates\\Registration.html");
                case EmailType.Subscription:
                    return Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates\\EmailSubscription.html");
                case EmailType.ForgotPassword:
                    return Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates\\ForgotPassword.html");
                default:
                    return null;
            }
        }

        public enum EmailType
        {
            Subscription = 1,
            Registration = 2,
            ForgotPassword = 3,
            Appointment = 4,
            Payment = 5,
            PaymentConfirmation = 6
        }

        public static string GetDescription<T>(this T enumValue)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }

    }
}
