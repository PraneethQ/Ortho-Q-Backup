using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Pricing.Queries.GetPricing
{
    public class PricingDetails
    {
        public string Id { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
    }
}
