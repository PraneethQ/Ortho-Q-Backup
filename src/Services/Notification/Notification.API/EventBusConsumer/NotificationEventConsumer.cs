using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Application.Features.Notification.Commands.EmailNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.API.EventBusConsumer
{
    public class NotificationEventConsumer : IConsumer<NotificationEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationEventConsumer> _logger;

        public NotificationEventConsumer(IMediator mediator, IMapper mapper, ILogger<NotificationEventConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<NotificationEvent> context)
        {
            //create a mapping b/w the NotificationCommand and NotificationEvent
            // as for creating order MediatR handler expecting the NotificationCommand

            //context.Message contains the NotificationEvent, which is maps to NotificationCommand
            var command = _mapper.Map<NotificationCommand>(context.Message);

            //NotificationCommand handler recieves the command request, from where
            //it calls the repository to send email.

            var result = await _mediator.Send(command);

            _logger.LogInformation("NotificationEvent consumed successfully. Sent Email Successfully", result);
        }
    }
}
