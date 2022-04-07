using Appointment.Application.Features.Providers.Commands.CreateProvider;
using Appointment.Application.Features.Providers.Queries.GetProvider;

using AutoMapper;
using  MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Appointment.API.Controllers
{
    [ApiController]
    [Route("api/appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        //private readonly IPublishEndpoint _publishEndpoint;

        public AppointmentController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet(Name = "GetProvider")]
        [ProducesResponseType(typeof(IEnumerable<ProviderProfile>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetProviderByProviderName(string providerName, string Id)
        {
          


            var query = new ProviderQuery(providerName, Id);
            var provider = await _mediator.Send(query);
            return StatusCode((int)provider.StatusCode, provider.ResultSet);

            //return StatusCode((int)provider.StatusCode, provider.ResultSet);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateProvider([FromBody] ProviderCommand command)
        {


           var result = await _mediator.Send(command);
           return StatusCode((int)result.StatusCode, result.ResultSet);
            //return Ok("");
        }
    }
}
