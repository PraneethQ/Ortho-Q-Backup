using AutoMapper;
using MediatR;
using Member.Application.Features.Dental.Queries.GetDentalData;
using Member.Application.Features.Patient.Queries.GetPatientData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Member.API.Controllers
{
    [ApiController]
    [Route("api/v1/member")]
    public class MemberController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MemberController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        //[HttpGet("{userName}", Name = "GetUser")]
        [HttpGet]
        [Route("dentalinformation")]
        [ProducesResponseType(typeof(IEnumerable<DentalInformation>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetDentalInformation(string userName,string id)
        {
            // we construct a query and send it to mediator. Mediator will handle the request using
            // IRequestHnadler & communicate with DB and gets the result.
            //using this mediator, our code is clean and mediator will handle the request.

            var query = new GetDentalQuery(userName,id);
            var user = await _mediator.Send(query);
            return StatusCode((int)user.StatusCode, user.ResultSet);
        }

        [HttpGet]
        [Route("patientinformation")]
        [ProducesResponseType(typeof(IEnumerable<PatientInformation>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetPatientInformation(string id)
        {
            var query = new GetPatientQuery(id);
            var user = await _mediator.Send(query);
            return StatusCode((int)user.StatusCode, user.ResultSet);
        }

    }
}
