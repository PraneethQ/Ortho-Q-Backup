using AutoMapper;
using MediatR;
using Member.Application.Contracts.Persistance;
using Member.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Member.Application.Features.Patient.Queries.GetPatientData
{
    public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, ActionReturnType>
    {
        private readonly IMemberRepository _patientRepository;
        private readonly IMapper _mapper;

        public GetPatientQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _patientRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ActionReturnType> Handle(GetPatientQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.Id))
            {
                var patientObject = await _patientRepository.GetPatientById(request.Id);
                var data = _mapper.Map<PatientInformation>(patientObject.ResultSet);
                return new ActionReturnType
                {
                    StatusCode = patientObject.StatusCode,
                    ResultSet = data
                };
            }
            var patientList = await _patientRepository.GetPatients();
            var dataList = _mapper.Map<List<PatientInformation>>(patientList.ResultSet);
            return new ActionReturnType
            {
                StatusCode = patientList.StatusCode,
                ResultSet = dataList
            };
        }
    }                                               
}
