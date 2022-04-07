using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Order.Application.Contracts.Persistance;
using Order.Domain.Entities;

namespace Order.Infrastructure.Persistance
{
    class OrderContext : IOrderContext
    {
        //IConfiguration provided by .net core extensions
        //inbuilt dependency injection by .net core

        public OrderContext (IConfiguration configuration)
        {
            //MongoClient is responsible for the creating a connection with MongoDB
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            PricingEntity = database.GetCollection<PricingEntity>(configuration.GetValue<string>("DatabaseSettings:PricingCollection"));
        }

        public IMongoCollection<PricingEntity> PricingEntity { get; }
    }
}
