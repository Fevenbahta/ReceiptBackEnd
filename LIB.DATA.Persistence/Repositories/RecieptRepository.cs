
using AutoMapper;
using LIB.API.Application.Contracts.Persistent;
using LIB.API.Application.CQRS.Reciept.Request.Command;
using LIB.API.Application.DTOs.Reciept;
using LIB.API.Domain;
using LIB.API.Persistence;
using LIBPROPERTY.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LIB.API.Persistence.Repositories
{
    public class RecieptRepository : GenericRepository<Reciepts>, IRecieptRepository
    {
        private readonly LIBAPIDbSQLContext _context;
        private readonly LIBAPIDbContext _context2;
        private readonly IMapper _mapper;
   
        private readonly IMediator _mediator;

        public RecieptRepository(LIBAPIDbSQLContext context, LIBAPIDbContext context2, IMapper mapper, IMediator mediator) : base(context)
        {
            _context = context;
            _context2 = context2;
            _mapper = mapper;
             _mediator = mediator;
        }


        public async Task<List<RecieptDto>> GetRecieptsByDateIntervalAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.IfrsTransactions
                .Where(t => t.Transaction_Date >= startDate && t.Transaction_Date <= endDate)
                .Select(t => new RecieptDto
                {
                    //Id = t.Id,  // Assuming `Id` is the same
                    //RecieptNo = t.RecieptNo,  // Replace `RecieptNo` from the old structure
                    //Date = t.Date,  // Mapping DateTime field
                    //CustomerName = t.CustomerName,  // Assuming `CustomerName` maps to the existing data
                    //Account = t.Account,  // Assuming `Account` maps to the existing data
                    //Amount = t.Amount,  // Assuming `Amount` maps to the existing data
                    //ServiceFee = t.ServiceFee,  // Assuming `ServiceFee` maps to the existing data
                    //Vat = t.Vat,  // Assuming `Vat` maps to the existing data
                    //Branch = t.Branch,  // Mapping `Branch` directly
                    //ServiceType = t.ServiceType,  // Assuming `ServiceType` is present in the existing data
                    //Reason = t.Reason,  // Mapping `Reason` directly
                    //CreatedBy = t.CreatedBy,  // Assuming `CreatedBy` is in the data
                    //TinNo = t.TinNo,  // Assuming `TinNo` is present
                    //Address = t.Address,  // Nullable `Address` mapping
                    //PhoneNo = t.PhoneNo,  // Nullable `PhoneNo` mapping

                    //Status = t.Status,
                    //UpdatedDate = t.UpdatedDate,
                    //UpdatedBy = t.UpdatedBy,


                    Id = t.Id,
                    Transaction_Date = t.Transaction_Date,
                    Inputing_Branch = t.Inputing_Branch,
                    Branch = t.Branch,
                    Account_No = t.Account_No,
                    Refno = t.Refno,
                    Amount1 = t.Amount1,
                    Address = t.Address,
                    Debitor_Name = t.Debitor_Name,
                    Phone_No = t.Phone_No,
                    TinNo = t.TinNo,
                    PaymentMethod = t.PaymentMethod,
                    Status = t.Status,
                    UpdatedDate = t.UpdatedDate,
                    UpdatedBy = t.UpdatedBy,
                    CAccountNo = t.CAccountNo,
                    ApprovedBy = t.ApprovedBy,
                    CreatedBy = t.CreatedBy,
                    PaymentType = t.PaymentType,
                    ServiceFee = t.ServiceFee,
                    ServiceFee2 = t.ServiceFee2,
                    Vat = t.Vat,
                    Vat2 = t.Vat2,
                    MesssageNo = t.MesssageNo,
                    PaymentNo = t.PaymentNo

                })
                .ToListAsync();
        }

        public async Task<RecieptDto> GetRecieptByReferenceNoAsync(string id)
        {
            var t = await _context.IfrsTransactions
                .FirstOrDefaultAsync(t => t.Id.ToString() == id);

            if (t == null) return null;

            return new RecieptDto
            {
                //    Id = t.Id,  // Assuming `Id` is the same
                //    RecieptNo = t.RecieptNo,  // Replace `RecieptNo` from the old structure
                //    Date = t.Date,  // Mapping DateTime field
                //    CustomerName = t.CustomerName,  // Assuming `CustomerName` maps to the existing data
                //    Account = t.Account,  // Assuming `Account` maps to the existing data
                //    Amount = t.Amount,  // Assuming `Amount` maps to the existing data
                //    ServiceFee = t.ServiceFee,  // Assuming `ServiceFee` maps to the existing data
                //    Vat = t.Vat,  // Assuming `Vat` maps to the existing data
                //    Branch = t.Branch,  // Mapping `Branch` directly
                //    ServiceType = t.ServiceType,  // Assuming `ServiceType` is present in the existing data
                //    Reason = t.Reason,  // Mapping `Reason` directly
                //    CreatedBy = t.CreatedBy,  // Assuming `CreatedBy` is in the data
                //    TinNo = t.TinNo,  // Assuming `TinNo` is present
                //    Address = t.Address,  // Nullable `Address` mapping
                //    PhoneNo = t.PhoneNo,  // Nullable `PhoneNo` mapping

                //        Status = t.Status,
                //    UpdatedDate = t.UpdatedDate,
                //    UpdatedBy = t.UpdatedBy,


                Id = t.Id,
                Transaction_Date = t.Transaction_Date,
                Inputing_Branch = t.Inputing_Branch,
                Branch = t.Branch,
                Account_No = t.Account_No,
                Refno = t.Refno,
                Amount1 = t.Amount1,
                Address = t.Address,
                Debitor_Name = t.Debitor_Name,
                Phone_No = t.Phone_No,
                TinNo = t.TinNo,
                PaymentMethod = t.PaymentMethod,
                Status = t.Status,
                UpdatedDate = t.UpdatedDate,
                UpdatedBy = t.UpdatedBy,
                CAccountNo = t.CAccountNo,
                ApprovedBy = t.ApprovedBy,
                CreatedBy = t.CreatedBy,
                PaymentType = t.PaymentType,
                ServiceFee = t.ServiceFee,
                ServiceFee2 = t.ServiceFee2,
                Vat = t.Vat,
                Vat2 = t.Vat2,
                MesssageNo = t.MesssageNo,
                PaymentNo = t.PaymentNo

            };
        }


        public async Task<List<RecieptDto>> GetRecieptManualAsync(DateTime startDate, DateTime endDate)
        {
            var transactions = await _context.IfrsTransactions
                .Where(t => t.PaymentType == "Manual" && t.Transaction_Date.Date >= startDate.Date && t.Transaction_Date.Date <= endDate.Date)
                .ToListAsync();

            if (transactions == null || transactions.Count == 0)
            {
                return null;  // No transactions found
            }

            var receiptDtos = transactions.Select(t => new RecieptDto
            {
                Id = t.Id,
                Transaction_Date = t.Transaction_Date,
                Inputing_Branch = t.Inputing_Branch,
                Branch = t.Branch,
                Account_No = t.Account_No,
                Refno = t.Refno,
                Amount1 = t.Amount1,
                Address = t.Address,
                Debitor_Name = t.Debitor_Name,
                Phone_No = t.Phone_No,
                TinNo = t.TinNo,
                PaymentMethod = t.PaymentMethod,
                Status = t.Status,
                UpdatedDate = t.UpdatedDate,
                UpdatedBy = t.UpdatedBy,
                CAccountNo = t.CAccountNo,
                ApprovedBy = t.ApprovedBy,
                CreatedBy = t.CreatedBy,
                PaymentType = t.PaymentType,
                ServiceFee = t.ServiceFee,
                ServiceFee2 = t.ServiceFee2,
                Vat = t.Vat,
                Vat2 = t.Vat2,
                MesssageNo = t.MesssageNo,
                PaymentNo=t.PaymentNo
            }).ToList();

            return receiptDtos;
        }


        public async Task<List<RecieptDto>> GetIfrsTransactionsByDateIntervalAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.IfrsTransactions
                .Where(t => t.Transaction_Date >= startDate && t.Transaction_Date <= endDate)
                .Select(t => new RecieptDto
                {
                    Id = t.Id,
                    Transaction_Date = t.Transaction_Date,
                    Inputing_Branch = t.Inputing_Branch,
                    Branch = t.Branch,
                    Account_No = t.Account_No,
                    Refno = t.Refno,
                    Amount1 = t.Amount1,
                    Address = t.Address,
                    Debitor_Name = t.Debitor_Name,
                    Phone_No = t.Phone_No,
                    TinNo = t.TinNo,
                    PaymentMethod = t.PaymentMethod,
                    Status = t.Status,
                    UpdatedDate = t.UpdatedDate,
                    UpdatedBy = t.UpdatedBy,
                    CAccountNo = t.CAccountNo,
                    ApprovedBy = t.ApprovedBy,
                    CreatedBy = t.CreatedBy,
                    PaymentType = t.PaymentType,
                    ServiceFee = t.ServiceFee,
                    ServiceFee2 = t.ServiceFee2,
                    Vat = t.Vat,
                    Vat2 = t.Vat2
                })
                .ToListAsync();
        }
        public async Task<Reciepts> GetLastTransactionAsync()
        {
            return await _context.IfrsTransactions
                .OrderByDescending(t => t.Id)  // Assuming Id is the primary key
                .FirstOrDefaultAsync();  // Get the latest transaction
        }




        public async Task<AccountInfos> GetUserDetailsByAccountNumberAsync(string accountNumber)
        {


            var query2 = @"
SELECT *
FROM anbesaprod.valid_accounts
WHERE ACCOUNTNUMBER = :accountNumber";

            var accountNumberParameter = new OracleParameter("accountNumber", accountNumber);
            var userDetails2 = await _context2.AccountInfos
                .FromSqlRaw(query2, accountNumberParameter)
                .FirstOrDefaultAsync();




            return (userDetails2);
        }

    }
}
