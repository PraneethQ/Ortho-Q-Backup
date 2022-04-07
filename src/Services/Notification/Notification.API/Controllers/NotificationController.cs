using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Features.Notification.Commands.EmailNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Notification.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class NotificationController:ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> SubscriberNotification([FromBody] NotificationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
