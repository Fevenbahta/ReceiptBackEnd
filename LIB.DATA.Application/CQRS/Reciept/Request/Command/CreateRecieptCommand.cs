
using LIB.API.Application.DTOs.Reciept;
using LIB.API.Domain;
using LIBPROPERTY.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.API.Application.CQRS.Reciept.Request.Command
{
    public class CreateRecieptCommand : IRequest<Reciepts>
    {
        public RecieptDto RecieptDto { get; set; }
    }
}
