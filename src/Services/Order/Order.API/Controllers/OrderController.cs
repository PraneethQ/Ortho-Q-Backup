using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Order.Application.Features.Pricing.Queries.GetPricing;
using Order.Application.Features.Pricing.Commands.CreatePricingDetails;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/Order")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet(Name = "GetPricing")]
        //[Route("api/GetPricingDetails")]
        [ProducesResponseType(typeof(IEnumerable<PricingDetails>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetPricingDetails(string Id)
        {
            var query = new GetPricingQuery(Id);
            var result = await _mediator.Send(query);
            return StatusCode((int)result.StatusCode, result.ResultSet);
        }

        [HttpPost(Name = "PostPricingDetail")]
        //[Route("api/PostPricingDetail")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateUser([FromBody] CreatePricingDetailCommand command)
        {
            var result = await _mediator.Send(command);

            return StatusCode((int)result.StatusCode, result.ResultSet);
        }
    }
}
