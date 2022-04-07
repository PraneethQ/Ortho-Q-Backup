using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Application.Contracts.Persistance;
using Order.Application.Models;
using Order.Domain.Entities;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderContext _dbContext;

        public OrderRepository(IOrderContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ActionReturnType> GetPricingDetails()
        {
            var pricingList = await _dbContext.PricingEntity.Find(p => true).ToListAsync();
            if (pricingList.Count > 0)
            {
                return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, pricingList);
            }
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.NotFound, "no pricing details exist");
        }
        public async Task<ActionReturnType> GetPriceById(string Id)
        {
            var pricingList = await _dbContext.PricingEntity.Find(p => p.Id == Id).FirstOrDefaultAsync();
            if (pricingList != null)
            {
                return ActionSet.ActionReturnType(System.Net.HttpStatusCode.OK, pricingList);
            }
            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.NotFound, "Price Detail not found");
        }

        public async Task<ActionReturnType> CreatePricingDetail(PricingEntity pricingDetail)
        {
            await _dbContext.PricingEntity.InsertOneAsync(pricingDetail);

            return ActionSet.ActionReturnType(System.Net.HttpStatusCode.Created, "Pricing Detail Created Successfully");
        }
    }
}
