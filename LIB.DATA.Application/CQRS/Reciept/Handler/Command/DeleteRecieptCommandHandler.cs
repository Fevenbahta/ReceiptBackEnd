using AutoMapper;
using LIB.API.Application.Contracts.Persistent;
using LIB.API.Application.CQRS.Reciept.Request.Command;
using LIBPROPERTY.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.API.Application.CQRS.Reciept.Handler.Command
{
    public class DeleteRecieptCommandHandler : IRequestHandler<DeleteRecieptCommand>
    {
        private IRecieptRepository _RecieptRepository;
        private IMapper _mapper;
        public DeleteRecieptCommandHandler(IRecieptRepository RecieptRepository, IMapper mapper)
        {
            _RecieptRepository = RecieptRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteRecieptCommand request, CancellationToken cancellationToken)
        {
            var Reciept = await _RecieptRepository.GetById(request.Id);
            await _RecieptRepository.Delete(Reciept);
            return Unit.Value;
        }

        async Task IRequestHandler<DeleteRecieptCommand>.Handle(DeleteRecieptCommand request, CancellationToken cancellationToken)
        {
            var Reciept = await _RecieptRepository.GetById(request.Id);
            if (Reciept == null)
                throw new NotFoundException(nameof(Reciept), request.Id);
            Reciept.Status = "1";
            await _RecieptRepository.Update(Reciept);

        }
    }
}
