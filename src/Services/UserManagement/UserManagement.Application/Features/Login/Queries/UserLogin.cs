using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Features.Users.Queries.GetUser;
using UserManagement.Application.Models;

namespace UserManagement.Application.Features.Login.Queries
{
    public class UserLogin : IRequest<ActionReturnType>
    {
        public string Email { get; set; }
        public string EncriptedText { get; set; }

        public UserLogin(string email, string encriptedText)
        {
            Email = email != null ? email : string.Empty;
            EncriptedText = encriptedText != null ? encriptedText : string.Empty;
        }
    }
}
