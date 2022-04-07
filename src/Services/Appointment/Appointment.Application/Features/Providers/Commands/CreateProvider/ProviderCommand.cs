using Appointment.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Application.Features.Providers.Commands.CreateProvider
{
    public class ProviderCommand : IRequest<ActionReturnType>
    {
        public string ProviderName { get; set; }
    }
}
