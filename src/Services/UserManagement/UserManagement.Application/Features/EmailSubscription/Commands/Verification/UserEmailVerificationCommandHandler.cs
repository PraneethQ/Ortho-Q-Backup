using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistance;
using UserManagement.Application.Models;

namespace UserManagement.Application.Features.EmailSubscription.Commands.Verification
{
    public class UserEmailVerificationCommandHandler : IRequestHandler<UserEmailVerificationCommand,ActionReturnType>
    {
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly IMapper _mapper;
        private ILogger<UserEmailVerificationCommandHandler> _logger;

        public UserEmailVerificationCommandHandler(IUserManagementRepository userManagementRepository, IMapper mapper, ILogger<UserEmailVerificationCommandHandler> logger)
        {
            _userManagementRepository = userManagementRepository ?? throw new ArgumentNullException(nameof(userManagementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ActionReturnType> Handle(UserEmailVerificationCommand request, CancellationToken cancellationToken)
        {
            var data=await _userManagementRepository.VerifyUserSubscription(request.Email, request.ConfirmationToken);
            return new ActionReturnType
            {
                StatusCode=data.StatusCode,
                ResultSet=data.ResultSet
            };
        }

        //public async Task<Unit> Handle(UserEmailVerificationCommand request, CancellationToken cancellationToken)
        //{
        //    await _userManagementRepository.VerifyUserSubscription(request.Email, request.ConfirmationToken);
        //    return Unit.Value;
        //}
    }
}
