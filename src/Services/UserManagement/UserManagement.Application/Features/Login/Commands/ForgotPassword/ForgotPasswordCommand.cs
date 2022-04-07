using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Models;

namespace UserManagement.Application.Features.Login.Commands.ForgotPassword
{
    public class ForgotPasswordCommand:IRequest<ActionReturnType>
    {
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }

        public ForgotPasswordCommand(string email, string confirmationToken)
        {
            Email = email != null ? email : string.Empty;
            ConfirmationToken = confirmationToken != null ? confirmationToken : string.Empty;
        }
    }
}
