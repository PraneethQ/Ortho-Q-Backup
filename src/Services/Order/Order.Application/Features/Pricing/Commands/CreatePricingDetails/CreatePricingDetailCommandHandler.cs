using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Order.Application.Contracts.Persistance;
using Order.Application.Models;
using Order.Domain.Entities;

namespace Order.Application.Features.Pricing.Commands.CreatePricingDetails
{
    public class CreatePricingDetailCommandHandler : IRequestHandler<CreatePricingDetailCommand, ActionReturnType>
    {
        private readonly IOrderRepository _PricingRepository;
        private readonly IMapper _mapper;

        public CreatePricingDetailCommandHandler(IOrderRepository pricingRepository, IMapper mapper)
        {
            _PricingRepository = pricingRepository ?? throw new ArgumentNullException(nameof(pricingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ActionReturnType> Handle(CreatePricingDetailCommand request, CancellationToken cancellationToken)
        {
            //before saving we need to convert the request DTO object into the underlying DB entity.
            PricingEntity pricingEntity = _mapper.Map<PricingEntity>(request);

            var data = await _PricingRepository.CreatePricingDetail(pricingEntity);
            return new ActionReturnType
            {
                StatusCode = data.StatusCode,
                ResultSet = data.ResultSet
            };
        }
    }
}
