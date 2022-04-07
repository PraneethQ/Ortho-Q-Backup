using AutoMapper;
using EventBus.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.API.Mappings
{
    public class NotificationProfile:Profile
    {
        public NotificationProfile()
        {
            CreateMap<EmailSubscriptionEventEntity, NotificationEvent>().ReverseMap();
        }
    }
}
