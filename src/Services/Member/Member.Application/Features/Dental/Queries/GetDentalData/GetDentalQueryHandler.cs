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

namespace Member.Application.Features.Dental.Queries.GetDentalData
{
    public class GetDentalQueryHandler : IRequestHandler<GetDentalQuery, ActionReturnType>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetDentalQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ActionReturnType> Handle(GetDentalQuery request, CancellationToken cancellationToken)
        {
            var dentalObject = await _memberRepository.GetDentalInformation();
            var data = _mapper.Map<List<DentalInformation>>(dentalObject.ResultSet);
            return new ActionReturnType
            {
                StatusCode = dentalObject.StatusCode,
                ResultSet = data
            };
        }
    }
}
