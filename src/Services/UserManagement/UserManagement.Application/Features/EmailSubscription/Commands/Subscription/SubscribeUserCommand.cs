using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Models;

namespace UserManagement.Application.Features.EmailSubscription.Commands.Subscription
{
  public  class SubscribeUserCommand : IRequest<ActionReturnType>
    {
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public string ConfirmationToken { get; set; }

        public SubscribeUserCommand(string email, string zipCode,string confirmationToken)
        {
            Email = email != null ? email : string.Empty;
            ZipCode = zipCode != null ? zipCode : string.Empty;
            ConfirmationToken = confirmationToken != null ? confirmationToken : string.Empty;
        }
    }
}
