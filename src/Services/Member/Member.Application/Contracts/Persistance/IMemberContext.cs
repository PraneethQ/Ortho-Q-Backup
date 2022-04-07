using Member.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Member.Application.Contracts.Persistance
{
    public interface IMemberContext
    {
        public IMongoCollection<DentalEntity> DentalEntity { get; }
        public IMongoCollection<PatientEntity> PatientEntity { get; }
    }
}
