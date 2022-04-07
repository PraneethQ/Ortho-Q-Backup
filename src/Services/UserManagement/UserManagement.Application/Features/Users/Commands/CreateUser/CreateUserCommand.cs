using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Models;

namespace UserManagement.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand:IRequest<ActionReturnType>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string AlternateMobileNumber { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ZipCode { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHashAlgo { get; set; }
        public string ConfirmationToken { get; set; }


    }



}
