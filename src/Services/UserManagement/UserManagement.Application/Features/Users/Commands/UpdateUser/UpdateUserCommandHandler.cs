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

namespace UserManagement.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ActionReturnType>
    {
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly IMapper _mapper;
        private ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(IUserManagementRepository userManagementRepository, IMapper mapper, ILogger<UpdateUserCommandHandler> logger)
        {
            _userManagementRepository = userManagementRepository ?? throw new ArgumentNullException(nameof(userManagementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ActionReturnType> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            UserProfileEntity userProfileEntity = new UserProfileEntity();
            _mapper.Map(request, userProfileEntity, typeof(UpdateUserCommand), typeof(UserProfileEntity));

            //update the db with enity object
            var data =await _userManagementRepository.UpdateUser(userProfileEntity);

            _logger.LogInformation($"User is successfully updated.");

            // if there is no return type for the MediatR, we will use 'Unit' as the return type
            //return Unit.Value;

            return new ActionReturnType
            {
                StatusCode=data.StatusCode,
                ResultSet=data.ResultSet
            };
        }

        //public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        //{
        //    UserProfileEntity userProfileEntity = new UserProfileEntity();

        //    _mapper.Map(request, userProfileEntity, typeof(UpdateUserCommand), typeof(UserProfileEntity));

        //    //update the db with enity object
        //    await _userManagementRepository.UpdateUser(userProfileEntity);

        //    _logger.LogInformation($"User is successfully updated.");

        //    // if there is no return type for the MediatR, we will use 'Unit' as the return type
        //    return Unit.Value;
        //}
    }
}
