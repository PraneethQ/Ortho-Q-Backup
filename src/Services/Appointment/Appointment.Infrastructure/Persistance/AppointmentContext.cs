using Appointment.Application.Contracts.Persistance;
using Appointment.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appointment.Domain.Common;
using Microsoft.Extensions.Configuration;

namespace Appointment.Infrastructure.Persistance
{
    public class AppointmentContext : IAppointmentContext
    {
        public AppointmentContext(IConfiguration configuration)
        {
            //MongoClient is responsible for the creating a connection with MongoDB
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            ProviderProfileEntity = database.GetCollection<ProviderProfileEntity>(configuration.GetValue<string>("DatabaseSettings:ProviderProfileCollection"));
           
            
        }

        public IMongoCollection<ProviderProfileEntity> ProviderProfileEntity { get; }


    }
}
