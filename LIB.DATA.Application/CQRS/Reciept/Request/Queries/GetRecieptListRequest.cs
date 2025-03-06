
using LIB.API.Application.DTOs.Reciept;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.API.Application.CQRS.Reciept.Request.Queries
{
    public class GetRecieptListRequest : IRequest<List<RecieptDto>>
    {

    }
}
