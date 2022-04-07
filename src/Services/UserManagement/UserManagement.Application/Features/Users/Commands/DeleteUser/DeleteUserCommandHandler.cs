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
using UserManagement.Application.Exceptions;
using UserManagement.Application.Models;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand,ActionReturnType>
    {
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly IMapper _mapper;
        private ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUserManagementRepository userManagementRepository, IMapper mapper, ILogger<DeleteUserCommandHandler> logger)
        {
            _userManagementRepository = userManagementRepository ?? throw new ArgumentNullException(nameof(userManagementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ActionReturnType> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var data=await _userManagementRepository.DeleteUser(request.Id);
            _logger.LogInformation($"User is successfully deleted.");

            return new ActionReturnType
            {
                StatusCode=data.StatusCode,
                ResultSet=data.ResultSet
            };
        }

        //public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        //{
        //    await _userManagementRepository.DeleteUser(request.Id);
        //    _logger.LogInformation($"User is successfully deleted.");

        //    return Unit.Value;
        //}
    }
}
