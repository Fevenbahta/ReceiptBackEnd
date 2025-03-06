
using LIB.API.Application.Contracts.Persistent;
using LIB.API.Application.DTOs.Reciept;
using LIB.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.API.Application.Contracts.Persistent
{
    public interface IRecieptRepository : IGenericRepository<Reciepts>
    {
        Task<List<RecieptDto>> GetIfrsTransactionsByDateIntervalAsync(DateTime startDate, DateTime endDate);
        Task<List<RecieptDto>> GetRecieptsByDateIntervalAsync(DateTime startDate, DateTime endDate);
        Task<RecieptDto> GetRecieptByReferenceNoAsync(string referenceNo);
        Task<Reciepts> GetLastTransactionAsync();
        Task<List<RecieptDto>> GetRecieptManualAsync(DateTime startDate, DateTime endDate);
        Task<AccountInfos> GetUserDetailsByAccountNumberAsync(string accountNumber);
    }
}