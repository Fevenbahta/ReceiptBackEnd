using AutoMapper;
using LIB.API.Application.Contracts.Persistent;
using LIB.API.Application.CQRS.Reciept.Request.Queries;
using LIB.API.Application.DTOs.Reciept;
using LIBPROPERTY.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.API.Application.CQRS.Reciept.Handler.Queries
{
    internal class GetEmployeDetaileRequestHandler : IRequestHandler<GetRecieptDetaileRequest, RecieptDto>
    {
        private IRecieptRepository _RecieptRepository;
        private IMapper _mapper;
        public GetEmployeDetaileRequestHandler(IRecieptRepository RecieptRepository, IMapper mapper)
        {
            _RecieptRepository = RecieptRepository;
            _mapper = mapper;
        }
        public async Task<RecieptDto> Handle(GetRecieptDetaileRequest request, CancellationToken cancellationToken)
        {
            var Reciept = await _RecieptRepository.GetById(request.Id);
            if (Reciept == null || Reciept

                .Status == "1")
                throw new NotFoundException(nameof(Reciept), request.Id);
            return _mapper.Map<RecieptDto>(Reciept);
        }
    }
}
