using Appointment.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Application.Contracts.Persistance
{
    public interface IAppointmentContext
    {
        public IMongoCollection<ProviderProfileEntity> ProviderProfileEntity { get; }
    }
}
