using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Application.Features.Pricing.Queries.GetPricing;
using Order.Application.Features.Pricing.Commands.CreatePricingDetails;
using Order.Domain.Entities;

namespace Order.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PricingEntity, PricingDetails>().ReverseMap();
            CreateMap<PricingEntity, CreatePricingDetailCommand>().ReverseMap();
        }
    }
}
