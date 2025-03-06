using AutoMapper;
using LIB.API.Application.Contracts.Persistent;
using LIB.API.Application.CQRS.Reciept.Request.Command;
using LIB.API.Application.CQRS.Reciept.Request.Queries;
using LIB.API.Application.DTOs.Reciept;
using LIB.API.Domain;
using LIB.API.Persistence;
using LIB.API.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LIBPROPERTY_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecieptController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly LIBAPIDbSQLContext _context;
 
        private readonly IRecieptRepository _RecieptRepository;
        private readonly UpdateLogService _updateLogService;

        public RecieptController(IMediator mediator, IMapper mapper, LIBAPIDbSQLContext context, IRecieptRepository RecieptRepository, UpdateLogService updateLogService)
        {
            _updateLogService = updateLogService;
            _mediator = mediator;
            _mapper = mapper;
            _context = context;
            _RecieptRepository = RecieptRepository;
          
        }

        // GET: api/Reciept
        [HttpGet]
        public async Task<ActionResult<List<RecieptDto>>> GetAllReciepts()
        {
            var Reciept = await _mediator.Send(new GetRecieptListRequest());
            return Ok(Reciept);
        }

        // GET: api/Reciept/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<List<RecieptDto>>> GetRecieptById(int id)
        {
            var Reciept = await _mediator.Send(new GetRecieptDetaileRequest { Id = id });
            return Ok(Reciept);
        }


        [HttpGet("manual")]
        public async Task<ActionResult<RecieptDto>> GetRecieptManual([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var reciept = await _RecieptRepository.GetRecieptManualAsync(startDate, endDate);

                if (reciept == null)
                {
                    return NotFound(new { Message = "Receipt not found." });
                }

                return Ok(reciept);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Details = ex.Message });
            }
        }


        // POST: api/Reciept
        /*      [HttpPost]
              public async Task<ActionResult> CreateReciept([FromBody] RecieptDto Reciept)
              {
                  var command = new CreateRecieptCommand { RecieptDto = Reciept };
                  await _mediator.Send(command);
                  return Ok(command);
              }*/
        [HttpPost]
        public async Task<ActionResult> CreateReciept([FromBody] RecieptDto Reciept)
        {
            var command = new CreateRecieptCommand { RecieptDto = Reciept };
            var createdReciept = await _mediator.Send(command);

            return Ok(createdReciept);  // Return the full receipt object
        }

        // PUT: api/Reciept/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReciept(int id, [FromBody] RecieptDto Reciept)
        {

            try
            {
 var command = new UpdateRecieptCommand { RecieptDto = Reciept };
            //_context.Entry(existingEvent).Property(x => x.ReferenceNumber).IsModified = false;
            await _mediator.Send(command);
            return NoContent();

            }
            catch (Exception ex)
            {
                // Log exception (not shown here)
                return StatusCode(500, $"{ex.Message}");
            }
        }

        // DELETE: api/Reciept/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReciept(int id)
        {
            var command = new DeleteRecieptCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("GetIfrsTransactionsByDateInterval")]
        public async Task<IActionResult> GetIfrsTransactionsByDateInterval([FromBody] DateIntervalRequest request)
        {
            if (request.StartDate > request.EndDate)
            {
                return BadRequest("Start date cannot be later than end date.");
            }

            try
            {
                // Get IfrsTransactions from repository
                var IfrsTransactions = await _RecieptRepository.GetIfrsTransactionsByDateIntervalAsync(request.StartDate, request.EndDate);

                if (IfrsTransactions == null || !IfrsTransactions.Any())
                {
                    return NotFound("No IfrsTransactions found for the given date interval.");
                }



                return Ok(IfrsTransactions);
            }
            catch (Exception ex)
            {
                // Log exception (not shown here)
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        [HttpGet("validate-account/{accountNo}")]
        public async Task<ActionResult<AccountInfos>> ValidateAccount(string accountNo)
        {
            try
            {
                var accountDetails = await _RecieptRepository.GetUserDetailsByAccountNumberAsync(accountNo);

                if (accountDetails == null)
                {
                    return Ok(null); // Return null instead of NotFound
                }


                return Ok(accountDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while validating the account.", Details = ex.Message });
            }
        }


    }


}
