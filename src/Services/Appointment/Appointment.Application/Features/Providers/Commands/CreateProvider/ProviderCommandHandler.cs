using Appointment.Application.Contracts.Persistance;
using Appointment.Application.Models;
using Appointment.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Appointment.Application.Features.Providers.Commands.CreateProvider
{
    public class ProviderCommandHandler : IRequestHandler<ProviderCommand, ActionReturnType>
    {

        private readonly IAppointmenttRepository _providerManagementRepository;
        private readonly IMapper _mapper;
        //private readonly IEmailService _emailService;
        private ILogger<ProviderCommandHandler> _logger;


        public ProviderCommandHandler(IAppointmenttRepository providerManagementRepository, IMapper mapper,  ILogger<ProviderCommandHandler> logger)
        {
            _providerManagementRepository = providerManagementRepository ?? throw new ArgumentNullException(nameof(providerManagementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public  async Task<ActionReturnType> Handle(ProviderCommand request, CancellationToken cancellationToken)
        {
            ProviderProfileEntity providerProfileEntity = _mapper.Map<ProviderProfileEntity>(request);
            
            var data = await _providerManagementRepository.CreateProvider(providerProfileEntity);
            return new ActionReturnType
            {
                StatusCode = data.StatusCode,
                ResultSet = data.ResultSet
            };
        }
    }
}
