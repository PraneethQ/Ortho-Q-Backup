using Appointment.Application.Contracts.Persistance;
using Appointment.Application.Models;
using Appointment.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmenttRepository
    {

        private readonly IAppointmentContext _dbContext;

        public AppointmentRepository(IAppointmentContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ActionReturnType> CreateProvider(ProviderProfileEntity providerProfile)
        {
            await _dbContext.ProviderProfileEntity.InsertOneAsync(providerProfile);

           return ActionSet.ActionReturnType(System.Net.HttpStatusCode.Created, "Provider Created Successfully");
        }

        public async Task<ActionReturnType> GetProviderById(string Id)
        {
            var providerObj = await _dbContext.ProviderProfileEntity.Find(p => p.Id == Id).FirstOrDefaultAsync();
            if (providerObj != null)
            {
                return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, providerObj);
            }
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.NotFound, "provider not found");
        }

        public async Task<ActionReturnType> GetProviderByName(string providertName)
        {
            
            FilterDefinition<ProviderProfileEntity> filter = Builders<ProviderProfileEntity>.Filter.Eq(p => p.ProviderName, providertName);

            var providerObj = await _dbContext
                            .ProviderProfileEntity
                            .Find(filter)
                            .FirstOrDefaultAsync();
            if (providerObj != null)
            {
                return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, providerObj);
            }
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.NotFound, "user not found");
        }

        public async Task<ActionReturnType> GetProviders()
        {
            var providerList = await _dbContext.ProviderProfileEntity.Find(p => true).ToListAsync();
            if (providerList.Count > 0)
            {
                return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, providerList);
            }
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.NotFound, "no providers exist");
        }

    }
}
