using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Order.Application.Contracts.Persistance;
using Order.Application.Models;


namespace Order.Application.Features.Pricing.Queries.GetPricing
{
    public class GetPricingQueryHandler : IRequestHandler<GetPricingQuery, ActionReturnType>
    {
        private readonly IOrderRepository _PricingRepository;
        private readonly IMapper _mapper;

        public GetPricingQueryHandler(IOrderRepository PricingRepository, IMapper mapper)
        {
            _PricingRepository = PricingRepository ?? throw new ArgumentNullException(nameof(PricingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ActionReturnType> Handle(GetPricingQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.Id))
            {
                var price = await _PricingRepository.GetPriceById(request.Id);
                var data = _mapper.Map<PricingDetails>(price.ResultSet);
                return new ActionReturnType
                {
                    StatusCode = price.StatusCode,
                    ResultSet = data
                };
            }
            var priceList = await _PricingRepository.GetPricingDetails();
            var dataList = _mapper.Map<PricingDetails>(priceList.ResultSet);
            return new ActionReturnType
            {
                StatusCode = priceList.StatusCode,
                ResultSet = dataList
            };
        }
    }
}
