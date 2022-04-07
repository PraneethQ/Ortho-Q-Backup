using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Domain.Entities;

namespace Order.Application.Contracts.Persistance
{
    public interface IOrderContext
    {
        public IMongoCollection<PricingEntity> PricingEntity { get; }
    }
}
