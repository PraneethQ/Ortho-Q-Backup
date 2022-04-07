using MediatR;
using Member.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Member.Application.Features.Dental.Queries.GetDentalData
{
    public class GetDentalQuery:IRequest<ActionReturnType>
    {
        public string UserName { get; set; }
        public string Id { get; set; }

        public GetDentalQuery(string userName, string id)
        {
            UserName = userName != null ? userName : string.Empty;
            Id = id != null ? id : string.Empty;
        }
    }
}
