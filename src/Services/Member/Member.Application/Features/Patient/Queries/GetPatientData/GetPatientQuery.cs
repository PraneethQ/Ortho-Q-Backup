using MediatR;
using Member.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Member.Application.Features.Patient.Queries.GetPatientData
{
    public class GetPatientQuery : IRequest<ActionReturnType>
    {
        public string Id { get; set; }

        public GetPatientQuery(string id)
        {
            Id = id != null ? id : string.Empty;
        }
    }
}
