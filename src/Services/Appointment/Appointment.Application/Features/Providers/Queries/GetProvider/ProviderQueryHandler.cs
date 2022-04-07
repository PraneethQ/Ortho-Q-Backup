using Appointment.Application.Contracts.Persistance;
using Appointment.Application.Models;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Appointment.Application.Features.Providers.Queries.GetProvider
{
    public class ProviderQueryHandler : IRequestHandler<ProviderQuery, ActionReturnType>
    {
        private readonly IAppointmenttRepository _providerManagementRepository;
        private readonly IMapper _mapper;

        public ProviderQueryHandler(IAppointmenttRepository providerManagementRepository, IMapper mapper)
        {
            _providerManagementRepository = providerManagementRepository ?? throw new ArgumentNullException(nameof(providerManagementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ActionReturnType> Handle(ProviderQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.Id))
            {
                var providerObject = await _providerManagementRepository.GetProviderById(request.Id);
                var data = _mapper.Map<ProviderProfile>(providerObject.ResultSet);
                return new ActionReturnType
                {
                    StatusCode = providerObject.StatusCode,
                    ResultSet = data
                };
            }
            else if (!string.IsNullOrEmpty(request.ProviderName))
            {
                var providerObject = await _providerManagementRepository.GetProviderByName(request.ProviderName);
                var data = _mapper.Map<ProviderProfile>(providerObject.ResultSet);
                return new ActionReturnType
                {
                    StatusCode = providerObject.StatusCode,
                    ResultSet = data
                };
            }
            var providersList = await _providerManagementRepository.GetProviders();
            var dataList = _mapper.Map<ProviderProfile>(providersList.ResultSet);
            return new ActionReturnType
            {
                StatusCode = providersList.StatusCode,
                ResultSet = dataList
            };
        }
    }
}
