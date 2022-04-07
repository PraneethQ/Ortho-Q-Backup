using AutoMapper;
using EventBus.Messages.Events;
using Notification.Application.Features.Notification.Commands.EmailNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.API.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<NotificationEvent, NotificationCommand>().ReverseMap();
        }
    }
}
