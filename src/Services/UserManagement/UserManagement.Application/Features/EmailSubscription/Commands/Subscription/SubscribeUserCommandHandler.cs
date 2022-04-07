using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Infrastructure;
using UserManagement.Application.Contracts.Persistance;
using UserManagement.Application.Models;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.EmailSubscription.Commands.Subscription
{
    public class SubscribeUserCommandHandler : IRequestHandler<SubscribeUserCommand, ActionReturnType>
    {
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private ILogger<SubscribeUserCommandHandler> _logger;


        public SubscribeUserCommandHandler(IUserManagementRepository userManagementRepository, IMapper mapper, IEmailService emailService, ILogger<SubscribeUserCommandHandler> logger)
        {
            _userManagementRepository = userManagementRepository ?? throw new ArgumentNullException(nameof(userManagementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ActionReturnType> Handle(SubscribeUserCommand request, CancellationToken cancellationToken)
        {
            var data = await _userManagementRepository.UserSubscription(request.Email, request.ZipCode, request.ConfirmationToken);
            return new ActionReturnType
            {
                StatusCode = data.StatusCode,
                ResultSet = data.ResultSet
            };

        }
        //public async Task<Unit> Handle(SubscribeUserCommand request, CancellationToken cancellationToken)
        //{
        //    await _userManagementRepository.UserSubscription(request.Email,request.ZipCode,request.ConfirmationToken);
        //    return Unit.Value;
        //}
    }
}
