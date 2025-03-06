
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using LIB.API.Application.Contracts.Persistent;
using LIB.API.Application.CQRS.Reciept.Request.Command;
using LIB.API.Application.DTOs.Reciept.Validators;
using LIBPROPERTY.Application.Exceptions;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.ComponentModel.Design;


public class UpdateRecieptCommandHandler : IRequestHandler<UpdateRecieptCommand, Unit>
{
 
    private readonly IRecieptRepository _RecieptRepository;
    private readonly IMapper _mapper;


    public UpdateRecieptCommandHandler(IRecieptRepository RecieptRepository, 
        IMapper mapper)
    {
        _RecieptRepository = RecieptRepository;

        _mapper = mapper;
     

        ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

    }




    public async Task<Unit> Handle(UpdateRecieptCommand request, CancellationToken cancellationToken)
    {

        var validator = new RecieptDtoValidators();
        var validationResult = await validator.ValidateAsync(request.RecieptDto);
        if (validationResult.IsValid == false)
            throw new ValidationException(validationResult);

        var re = await _RecieptRepository.GetById(request.RecieptDto.Id);



        var add = _mapper.Map(request.RecieptDto, re);

        await _RecieptRepository.Update(add);
        return Unit.Value;
    }
}