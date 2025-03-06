using AutoMapper;
using LIB.API.Application.Contracts.Persistent;
using LIB.API.Application.CQRS.Reciept.Request.Queries;
using LIB.API.Application.DTOs.Reciept;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.API.Application.CQRS.Reciept.Handler.Queries
{
    public class GetRecieptListRequestHandler : IRequestHandler<GetRecieptListRequest, List<RecieptDto>>
    {
        private IRecieptRepository _RecieptRepository;
        private IMapper _mapper;
        public GetRecieptListRequestHandler(IRecieptRepository RecieptRepository, IMapper mapper)
        {
            _RecieptRepository = RecieptRepository;
            _mapper = mapper;
        }
        public async Task<List<RecieptDto>> Handle(GetRecieptListRequest request, CancellationToken cancellationToken)
        {
            var Reciept = await _RecieptRepository.GetAll();
            var fur = Reciept.Where(s => s.Status != "1").ToList();
            return _mapper.Map<List<RecieptDto>>(fur);
        }
    }
}
