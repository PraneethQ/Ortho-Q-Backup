using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Application.Models;

namespace Order.Application.Features.Pricing.Queries.GetPricing
{
    public class GetPricingQuery : IRequest<ActionReturnType>
    {
        public string Id { get; set; }

        public GetPricingQuery(string id)
        {
            Id = id != null ? id : string.Empty;
        }
    }
}
