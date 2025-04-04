using AutoMapper;
using FluentValidation;
using LIB.API.Application.Contracts.Persistent;
using LIB.API.Application.CQRS.Reciept.Request.Command;
using LIB.API.Application.DTOs.Reciept;
using LIB.API.Application.DTOs.Reciept.Validators;
using LIB.API.Domain;
using LIBPROPERTY.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.API.Application.CQRS.Reciept.Handler.Command
{
    public class CreateRecieptCommandHandler : IRequestHandler<CreateRecieptCommand, Reciepts>
    {
        BaseCommandResponse response;
        private IRecieptRepository _RecieptRepository;
        private IMapper _mapper;
        public CreateRecieptCommandHandler(IRecieptRepository RecieptRepository, IMapper mapper)
        {
            _RecieptRepository = RecieptRepository;
            _mapper = mapper;
        }

  
        /*        public async Task<BaseCommandResponse> Handle(CreateRecieptCommand request, CancellationToken cancellationToken)
                {
                    response = new BaseCommandResponse();
                    var validator = new RecieptDtoValidators();
                    var validationResult = await validator.ValidateAsync(request.RecieptDto);
                    try
                    {
                        if (validationResult.IsValid == false)
                        {
                            response.Success = false;
                            response.Message = "Creation Faild";
                            response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                        }

                        var nextId = await GenerateNextId();

                        // Map the DTO to your entity and assign the new ID
                        var Reciept = _mapper.Map<Reciepts>(request.RecieptDto);
                        Reciept.RecieptNo = nextId; // Set the generated ID
                        Reciept.Date = DateTime.UtcNow;  // Set the current UTC time for the receipt

                        var data = await _RecieptRepository.Add(Reciept);
                        response.Success = true;
                        response.Message = "Creation Successfull";

                    }
                    catch (Exception ex)
                    {
                        response.Success = false;
                        response.Message = "Creation Failed due to an unexpected error";
                        response.Errors = new List<string> { ex.Message };
                    }
                        return response;
                }*/



        public async Task<Reciepts> Handle(CreateRecieptCommand request, CancellationToken cancellationToken)
        {
            var validator = new RecieptDtoValidators();
            var validationResult = await validator.ValidateAsync(request.RecieptDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException("Validation failed", validationResult.Errors);
            }


            var reciept = _mapper.Map<Reciepts>(request.RecieptDto);
            reciept.Refno = "Manual";
            if(!(reciept.ApprovedBy== "BackDate"))
            {
                reciept.Transaction_Date = DateTime.UtcNow;


            }
           
            var createdReciept = await _RecieptRepository.Add(reciept);

            return createdReciept;  // Return the created receipt
        }

    }
}
