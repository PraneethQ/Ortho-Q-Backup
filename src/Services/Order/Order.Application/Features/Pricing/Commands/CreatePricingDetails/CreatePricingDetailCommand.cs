using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Application.Models;

namespace Order.Application.Features.Pricing.Commands.CreatePricingDetails
{
    public class CreatePricingDetailCommand : IRequest<ActionReturnType>
    {
        public string Heading { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
    }
}
