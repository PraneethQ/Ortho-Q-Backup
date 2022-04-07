using AutoMapper;
using EventBus.Messages.Events;
using EventBus.Messages.Utilities;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Application.Features.EmailSubscription.Commands.Subscription;
using UserManagement.Application.Features.EmailSubscription.Commands.Verification;
using UserManagement.Domain.Entities;
using static EventBus.Messages.Utilities.EnumCollection;

namespace UserManagement.API.Controllers
{
    [Route("api/emailsubscription")]
    [ApiController]
    public class EmailSubscriptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public EmailSubscriptionController(IMediator mediator, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        //To API Test
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> UserSubscription([FromQuery] string email, [FromQuery] string zipcode)
        {
            string confirmationToken = Helper.GenerateNotificationCode();
            var command = new SubscribeUserCommand(email, zipcode, confirmationToken);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #region EmailSubscription

        [HttpPost]
        [Route("subscribe")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> EmailSubscription([FromQuery] string email, [FromQuery] string zipcode)
        {
            //generate a code here and send it to DB & Notification event, for constructing a URL to user
            string confirmationToken = Helper.GenerateNotificationCode();

            var command = new SubscribeUserCommand(email, zipcode, confirmationToken);
            var result = await _mediator.Send(command);

            //1) send notification event to rabbitmq
            //2) converting the class into the event to send it to the rabbitmq.

            EmailSubscriptionEventEntity entity = new EmailSubscriptionEventEntity()
            {
                Email = email,
                ConfirmationToken = confirmationToken,
                EmailType = EmailType.Subscription.GetDescription()
            };
            var eventMessage = _mapper.Map<NotificationEvent>(entity);

            // 3) to publish basket checkout event we use IPublishEndpoint of MassTransit, by injecting
            // this into controller constructor. with IPublsihEndpoint variable we can publish the
            // event.

            await _publishEndpoint.Publish(eventMessage);

            return StatusCode((int)result.StatusCode, result.ResultSet);
        }

        #endregion

        #region EmailVerification
        [HttpPost]
        [Route("verify")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> EmailVerification([FromQuery] string email, [FromQuery] string confirmationToken)
        {
            var command = new UserEmailVerificationCommand(email, confirmationToken);
            var result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result.ResultSet);
        }

        #endregion
    }
}
