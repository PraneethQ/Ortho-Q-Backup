using AutoMapper;
using Member.Application.Features.Dental.Queries.GetDentalData;
using Member.Application.Features.Patient.Queries.GetPatientData;
using Member.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Member.Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<DentalInformation, DentalEntity>().ReverseMap();
            CreateMap<PatientInformation, PatientEntity>().ReverseMap();
        }
    }
}
