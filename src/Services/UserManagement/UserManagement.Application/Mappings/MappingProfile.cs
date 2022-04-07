using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Features.EmailSubscription.Commands.Subscription;
using UserManagement.Application.Features.Login.Queries;
using UserManagement.Application.Features.Users.Commands.CreateUser;
using UserManagement.Application.Features.Users.Commands.UpdateUser;
using UserManagement.Application.Features.Users.Queries.GetUser;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserProfileEntity, UserProfile>().ReverseMap();
            CreateMap<UserProfileEntity, UserLoginProfile>().ReverseMap();

            CreateMap<UserProfileEntity, CreateUserCommand>().ReverseMap().ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.Date));
            CreateMap<UserAccountEntity, CreateUserCommand>().ReverseMap();

            CreateMap<UserProfileEntity, UpdateUserCommand>().ReverseMap();

            CreateMap<UserEmailSubscriptionEntity, SubscribeUserCommand>().ReverseMap();
        }
    }
}
