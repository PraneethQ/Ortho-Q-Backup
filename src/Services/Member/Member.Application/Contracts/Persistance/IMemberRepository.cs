using Member.Application.Models;
using Member.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Member.Application.Contracts.Persistance
{
    public interface IMemberRepository
    {
        Task<ActionReturnType> GetDentalInformation();
        Task<ActionReturnType> CreateMember(DentalEntity dentalEntity);
        Task<ActionReturnType> GetPatientById(string Id);
        Task<ActionReturnType> GetPatients();

    }
}
