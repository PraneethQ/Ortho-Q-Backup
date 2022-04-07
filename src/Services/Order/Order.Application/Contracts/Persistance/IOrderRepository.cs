using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Application.Models;
using Order.Domain.Entities;

namespace Order.Application.Contracts.Persistance
{
    public interface IOrderRepository
    {
        Task<ActionReturnType> GetPricingDetails();
        Task<ActionReturnType> GetPriceById(string Id);
        Task<ActionReturnType> CreatePricingDetail(PricingEntity pricingDetail);
    }
}
