using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Contracts.Persistance
{
    public interface IUserContext
    {
        public IMongoCollection<UserProfileEntity> UserProfileEntity { get; }
        public IMongoCollection<UserEmailSubscriptionEntity> UserEmailSubscriptionEntity { get; }
        public IMongoCollection<UserAccountEntity> UserAccountEntity { get; }
        public IMongoCollection<UserGeoLocationEntity> UserGeoLocationEntity { get; }
    }
}
