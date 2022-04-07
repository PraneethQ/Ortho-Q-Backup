using AutoMapper;
using Notification.Application.Features.Notification.Commands.EmailNotification;
using Notification.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<NotificationCommand, NotificationEntity>().ReverseMap();
        }
    }
}
