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

namespace UserManagement.Application.Features.Login.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ActionReturnType>
    {
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly IMapper _mapper;
        private ILogger<ResetPasswordCommandHandler> _logger;

        public ResetPasswordCommandHandler(IUserManagementRepository userManagementRepository, IMapper mapper, ILogger<ResetPasswordCommandHandler> logger)
        {
            _userManagementRepository = userManagementRepository ?? throw new ArgumentNullException(nameof(userManagementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ActionReturnType> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var data=await _userManagementRepository.ResetPassword(request.Email, request.ConfirmationToken, request.Password, request.PasswordSalt, request.PasswordHash);
            return new ActionReturnType
            {
                StatusCode = data.StatusCode,
                ResultSet = data.ResultSet
            };
        }

        //public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        //{
        //    await _userManagementRepository.ResetPassword(request.Email, request.ConfirmationToken, request.Password,request.PasswordSalt,request.PasswordHash);
        //    return Unit.Value;
        //}
    }
}
