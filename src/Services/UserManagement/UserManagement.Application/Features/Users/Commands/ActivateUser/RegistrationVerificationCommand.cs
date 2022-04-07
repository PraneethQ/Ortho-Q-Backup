using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Models;

namespace UserManagement.Application.Features.Users.Commands.ActivateUser
{
    public class RegistrationVerificationCommand:IRequest<ActionReturnType>
    {
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }

        public RegistrationVerificationCommand(string email, string confirmationToken)
        {
            Email = email!=null?email:string.Empty;
            ConfirmationToken = confirmationToken != null ? confirmationToken : string.Empty;
        }
    }
}
