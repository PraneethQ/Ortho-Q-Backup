using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.Application.Features.Providers.Commands.CreateProvider;
using Appointment.Application.Features.Providers.Queries.GetProvider;
using Appointment.Domain.Entities;
using AutoMapper;

namespace Appointment.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProviderProfileEntity, ProviderProfile>().ReverseMap(); 

            CreateMap<ProviderProfileEntity, ProviderCommand>().ReverseMap();
        }
    }
}
