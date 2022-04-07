using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using UserManagement.Application.Contracts.Persistance;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Persistance
{
    public class UserContext:IUserContext
    {
        //IConfiguration provided by .net core extensions
        //inbuilt dependency injection by .net core

        public UserContext(IConfiguration configuration)
        {
            //MongoClient is responsible for the creating a connection with MongoDB
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            UserProfileEntity = database.GetCollection<UserProfileEntity>(configuration.GetValue<string>("DatabaseSettings:UserProfileCollection"));
            UserAccountEntity = database.GetCollection<UserAccountEntity>(configuration.GetValue<string>("DatabaseSettings:UserAccountCollection"));
            UserEmailSubscriptionEntity = database.GetCollection<UserEmailSubscriptionEntity>(configuration.GetValue<string>("DatabaseSettings:UserEmailSubscriptionCollection"));
            UserGeoLocationEntity = database.GetCollection<UserGeoLocationEntity>(configuration.GetValue<string>("DatabaseSettings:UserGeoLocationCollection"));
            UserContextSeed.SeedData(UserProfileEntity);
        }

        public IMongoCollection<UserProfileEntity> UserProfileEntity { get; }
        public IMongoCollection<UserAccountEntity> UserAccountEntity { get; }
        public IMongoCollection<UserEmailSubscriptionEntity> UserEmailSubscriptionEntity { get; }
        public IMongoCollection<UserGeoLocationEntity> UserGeoLocationEntity { get; }
    }
}
