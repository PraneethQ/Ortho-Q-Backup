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

namespace UserManagement.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand,ActionReturnType>
    {
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserManagementRepository userManagementRepository, IMapper mapper, IEmailService emailService, ILogger<CreateUserCommandHandler> logger)
        {
            _userManagementRepository = userManagementRepository ?? throw new ArgumentNullException(nameof(userManagementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ActionReturnType> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //before saving we need to convert the request DTO object into the underlying DB entity.
            UserProfileEntity userProfileEntity = _mapper.Map<UserProfileEntity>(request);
            UserAccountEntity userAccountEntity = _mapper.Map<UserAccountEntity>(request);

            var data=await _userManagementRepository.CreateUser(userProfileEntity, userAccountEntity);
            return new ActionReturnType
            {
                StatusCode=data.StatusCode,
                ResultSet=data.ResultSet
            };
        }

        //public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        //{
        //    //before saving we need to convert the request DTO object into the underlying DB entity.
        //    UserProfileEntity userProfileEntity = _mapper.Map<UserProfileEntity>(request);
        //    UserAccountEntity userAccountEntity = _mapper.Map<UserAccountEntity>(request);
        //    await _userManagementRepository.CreateUser(userProfileEntity, userAccountEntity);
        //    return Unit.Value;
        //}

        private async Task SendEmail(UserProfileEntity newUser)
        {
            // Email is from UserManagement.Application.Models
            var email = new Email { To = "bharath.kosuri@qentelli.com", Body = $"New user was created", Subject = "New user was created" };

            try
            {
                //implement the SendEmail in the request handler
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"user creation with {newUser.Id} is failed due to an error with email service : {ex.Message}");
            }
        }
    }
}
