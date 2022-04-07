using Appointment.Application.Models;
using Appointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Application.Contracts.Persistance
{
    public interface IAppointmenttRepository
    {
        Task<ActionReturnType> GetProviders();
        Task<ActionReturnType> GetProviderById(string Id);
        Task<ActionReturnType> GetProviderByName(string providerName);

        Task<ActionReturnType> CreateProvider(ProviderProfileEntity providerProfile);
    }
}
