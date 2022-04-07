using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Application.Features.Users.Commands.CreateUser;
using UserManagement.Application.Features.Users.Commands.DeleteUser;
using UserManagement.Application.Features.Users.Commands.UpdateUser;
using UserManagement.Application.Features.Users.Queries.GetUser;
using UserManagement.Application.Features.Login.Queries;
using EventBus.Messages.Utilities;
using UserManagement.Application.Features.Login.Commands.ForgotPassword;
using UserManagement.Domain.Entities;
using EventBus.Messages.Events;
using AutoMapper;
using MassTransit;
using UserManagement.Application.Features.Login.Commands.ResetPassword;
using static EventBus.Messages.Utilities.EnumCollection;
using UserManagement.Application.Features.Users.Commands.ActivateUser;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("api/usermanagement")]
    public class UserManagementController:ControllerBase
    {
        //IMediator if from using MediatR. To handle all our CRUD operations 
        // in controller we use IMediator.
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public UserManagementController(IMediator mediator, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        #region GetUserByUserName

        //[HttpGet("{userName}", Name = "GetUser")]
        [HttpGet(Name = "GetUser")]
        [ProducesResponseType(typeof(IEnumerable<UserProfile>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetUserByUserName(string userName, string Id)
        {
            // we construct a query and send it to mediator. Mediator will handle the request using
            // IRequestHnadler & communicate with DB and gets the result.
            //using this mediator, our code is clean and mediator will handle the request.

            var query = new GetUserQuery(userName,Id);
            var user = await _mediator.Send(query);
            return StatusCode((int)user.StatusCode,user.ResultSet);
        }
        #endregion

        #region CreateUser

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            //generate a confirmation token  and send to db for save
            string confirmationToken = Helper.GenerateNotificationCode();
            command.ConfirmationToken = confirmationToken;

            var result = await _mediator.Send(command);

            //now we have to publish the event to notofication API

            EmailSubscriptionEventEntity entity = new EmailSubscriptionEventEntity()
            {
                Email = command.Email,
                ConfirmationToken = confirmationToken,
                EmailType = Convert.ToString(EmailType.Registration)
            };
            var eventMessage = _mapper.Map<NotificationEvent>(entity);

            // 3) to publish basket checkout event we use IPublishEndpoint of MassTransit, by injecting
            // this into controller constructor. with IPublsihEndpoint variable we can publish the
            // event.

            await _publishEndpoint.Publish(eventMessage);

            return StatusCode((int)result.StatusCode, result.ResultSet);
        }

        #endregion

        #region UpdateUser

        [HttpPut(Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var result=await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result.ResultSet);
        }

        #endregion

        #region DeleteUser

        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var command = new DeleteUserCommand() { Id = id };
            var result=await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result.ResultSet);
        }
        #endregion

        #region RegistrationVerification

        [HttpPost]
        [Route("verify")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> RegistrationVerification([FromQuery] string email, [FromQuery] string confirmationToken)
        {
            var command = new RegistrationVerificationCommand(email, confirmationToken);
            var result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result.ResultSet);
        }
        #endregion

        #region UserLogin

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(UserLoginProfile), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UserLogin(string email, string encriptedText)
        {
            var command = new UserLogin(email, encriptedText);
            var result = await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result.ResultSet);
        }
        #endregion

        #region ForgotPassword

        [HttpPost]
        [Route("forgotpassword")]
        public async Task<ActionResult> ForgotPassword([FromQuery]string email)
        {
            string confirmationToken = Helper.GenerateNotificationCode();
            var command = new ForgotPasswordCommand(email, confirmationToken);
            var result = await _mediator.Send(command);

            //1) send notification event to rabbitmq
            //2) converting the class into the event to send it to the rabbitmq.

            EmailSubscriptionEventEntity entity = new EmailSubscriptionEventEntity()
            {
                Email = email,
                ConfirmationToken = confirmationToken,
                EmailType = Convert.ToString(EmailType.ForgotPassword)
            };
            var eventMessage = _mapper.Map<NotificationEvent>(entity);

            // 3) to publish basket checkout event we use IPublishEndpoint of MassTransit, by injecting
            // this into controller constructor. with IPublsihEndpoint variable we can publish the
            // event.

            await _publishEndpoint.Publish(eventMessage);

            return StatusCode((int)result.StatusCode, result.ResultSet);

        }

        #endregion

        #region ResetPassword

        [HttpPost]
        [Route("resetpassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result=await _mediator.Send(command);
            return StatusCode((int)result.StatusCode, result.ResultSet);
        }

        #endregion
    }
}
