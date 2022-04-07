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

namespace UserManagement.Application.Features.Login.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ActionReturnType>
    {
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly IMapper _mapper;
        private ILogger<ForgotPasswordCommandHandler> _logger;

        public ForgotPasswordCommandHandler(IUserManagementRepository userManagementRepository, IMapper mapper, ILogger<ForgotPasswordCommandHandler> logger)
        {
            _userManagementRepository = userManagementRepository ?? throw new ArgumentNullException(nameof(userManagementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ActionReturnType> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var data=await _userManagementRepository.ForgotPassword(request.Email, request.ConfirmationToken);
            return new ActionReturnType
            {
                StatusCode = data.StatusCode,
                ResultSet = data.ResultSet
            };
        }

        //public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        //{
        //    await _userManagementRepository.ForgotPassword(request.Email, request.ConfirmationToken);
        //    return Unit.Value;
        //}
    }
}
