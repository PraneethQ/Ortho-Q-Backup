using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Models;

namespace UserManagement.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand:IRequest<ActionReturnType>
    {
        public string Id { get; set; }
    }
}
