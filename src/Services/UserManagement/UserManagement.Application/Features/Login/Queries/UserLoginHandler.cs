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
using UserManagement.Application.Features.Login.Queries;
using UserManagement.Application.Models;
using UserManagement.Domain.Entities;
using UserManagement.Application.Features.Users.Queries.GetUser;

namespace UserManagement.Application.Features.Login.Queries
{
    public class UserLoginHandler : IRequestHandler<UserLogin, ActionReturnType>
    {
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly IMapper _mapper;
        private ILogger<UserLogin> _logger;


        public UserLoginHandler(IUserManagementRepository userManagementRepository, IMapper mapper, ILogger<UserLoginHandler> logger)
        {
            _userManagementRepository = userManagementRepository ?? throw new ArgumentNullException(nameof(userManagementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //_logger = (ILogger<UserLogin>)(logger ?? throw new ArgumentNullException(nameof(logger)));
        }

        public async Task<ActionReturnType> Handle(UserLogin request, CancellationToken cancellationToken)
        {
            var userObject = await _userManagementRepository.LoginAuth(request.Email, request.EncriptedText);
            var data= _mapper.Map<UserLoginProfile>(userObject.ResultSet);
            return new ActionReturnType
            {
                StatusCode = userObject.StatusCode,
                ResultSet = data
            };
        }
        //public async Task<List<UserLoginProfile>> Handle(UserLogin request, CancellationToken cancellationToken)
        //{ 
        //    var userByEmail = await _userManagementRepository.GetUsertByEmail(request.Email);
        //    return _mapper.Map<List<UserLoginProfile>>(userByEmail);
        //}
    }
}
