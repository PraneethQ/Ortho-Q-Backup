using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Models;

namespace UserManagement.Application.Features.Users.Queries.GetUser
{
    public class GetUserQuery : IRequest<ActionReturnType>
    {
        public string UserName { get; set; }
        public string Id { get; set; }

        public GetUserQuery(string userName, string id)
        {
            UserName = userName != null ? userName : string.Empty;
            Id = id != null ? id : string.Empty;
        }
    }
}
