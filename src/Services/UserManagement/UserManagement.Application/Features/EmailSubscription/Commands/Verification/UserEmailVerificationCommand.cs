using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Models;

namespace UserManagement.Application.Features.EmailSubscription.Commands.Verification
{
    public class UserEmailVerificationCommand:IRequest<ActionReturnType>
    {
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }

        public UserEmailVerificationCommand(string email, string confirmationToken)
        {
            Email = email != null ? email : string.Empty;
            ConfirmationToken = confirmationToken != null ? confirmationToken : string.Empty;
        }
    }
}
