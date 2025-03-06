using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.API.Application.CQRS.Reciept.Request.Command
{
    public class DeleteRecieptCommand : IRequest
    {
        public int Id { get; set; }
    }
}
