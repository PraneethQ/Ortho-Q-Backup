using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.Features.Notification.Commands.EmailNotification
{
    public class NotificationCommand:IRequest
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string ConfirmationToken { get; set; }
        public string EmailType { get; set; }
        
    }
}
