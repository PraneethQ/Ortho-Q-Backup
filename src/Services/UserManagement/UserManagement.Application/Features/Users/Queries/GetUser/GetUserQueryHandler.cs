using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistance;
using UserManagement.Application.Models;

namespace UserManagement.Application.Features.Users.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ActionReturnType>
    {
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUserManagementRepository userManagementRepository, IMapper mapper)
        {
            _userManagementRepository = userManagementRepository ?? throw new ArgumentNullException(nameof(userManagementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ActionReturnType> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.Id))
            {
                var userObject = await _userManagementRepository.GetUserById(request.Id);
                var data=_mapper.Map<UserProfile>(userObject.ResultSet);
                return new ActionReturnType 
                {
                    StatusCode= userObject.StatusCode,
                    ResultSet=data
                };
            }
            else if (!string.IsNullOrEmpty(request.UserName))
            {
                var userObject = await _userManagementRepository.GetUserByName(request.UserName);
                var data = _mapper.Map<UserProfile>(userObject.ResultSet);
                return new ActionReturnType
                {
                    StatusCode = userObject.StatusCode,
                    ResultSet = data
                };
            }
            var usersList = await _userManagementRepository.GetUsers();
            var dataList = _mapper.Map<UserProfile>(usersList.ResultSet);
            return new ActionReturnType
            {
                StatusCode = usersList.StatusCode,
                ResultSet = dataList
            };
        }

        //public async Task<List<UserProfile>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        //{
        //    if (!string.IsNullOrEmpty(request.Id))
        //    {
        //        var userById = await _userManagementRepository.GetUserById(request.Id);
        //        return _mapper.Map<List<UserProfile>>(userById);
        //    }
        //    else if (!string.IsNullOrEmpty(request.UserName))
        //    {
        //        var userByName = await _userManagementRepository.GetUserByName(request.UserName);
        //        return _mapper.Map<List<UserProfile>>(userByName);
        //    }
        //    var users = await _userManagementRepository.GetUsers();
        //    return _mapper.Map<List<UserProfile>>(users);

        //}

    }
}
