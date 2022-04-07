using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Application.Contracts;
using Notification.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notification.Application.Features.Notification.Commands.EmailNotification
{
    public class NotificationCommandHandler : IRequestHandler<NotificationCommand>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private ILogger<NotificationCommandHandler> _logger;

        public NotificationCommandHandler(INotificationRepository notificationRepository, IMapper mapper, ILogger<NotificationCommandHandler> logger)
        {
            _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(NotificationCommand request, CancellationToken cancellationToken)
        {
            //before saving we need to convert the request DTO object into the underlying DB entity.
            NotificationEntity entity = _mapper.Map<NotificationEntity>(request);
            await _notificationRepository.SendEmail(entity);
            return Unit.Value;
        }
    }
}
