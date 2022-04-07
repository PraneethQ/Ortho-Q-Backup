using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Models;

namespace UserManagement.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand: IRequest<ActionReturnType>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
    }
}
