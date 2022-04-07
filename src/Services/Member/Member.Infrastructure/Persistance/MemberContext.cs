using Member.Application.Contracts.Persistance;
using Member.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Member.Infrastructure.Persistance
{
    public class MemberContext : IMemberContext
    {
        public MemberContext(IConfiguration configuration)
        {
            //MongoClient is responsible for the creating a connection with MongoDB
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            DentalEntity = database.GetCollection<DentalEntity>(configuration.GetValue<string>("DatabaseSettings:DentalEntityCollection"));
            PatientEntity = database.GetCollection<PatientEntity>(configuration.GetValue<string>("DatabaseSettings:PatientEntityCollection"));
            MemberContextSeed.SeedData(DentalEntity);
        }

        public IMongoCollection<DentalEntity> DentalEntity { get; }
        public IMongoCollection<PatientEntity> PatientEntity { get; }
    }
}
