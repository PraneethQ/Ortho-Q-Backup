using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Models;

namespace UserManagement.Application.Features.Login.Commands.ResetPassword
{
    public class ResetPasswordCommand:IRequest<ActionReturnType>
    {
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
    }
}
