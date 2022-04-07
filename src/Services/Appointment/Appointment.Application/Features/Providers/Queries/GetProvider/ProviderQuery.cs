using Appointment.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Application.Features.Providers.Queries.GetProvider
{
    public class ProviderQuery : IRequest<ActionReturnType>
    {
        public string ProviderName { get; set; }
        public string Id { get; set; }

        public ProviderQuery(string providerName, string id)
        {
            ProviderName = providerName != null ? providerName : string.Empty;
            Id = id != null ? id : string.Empty;
        }
    }
}
