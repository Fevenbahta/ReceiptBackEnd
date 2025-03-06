using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIB.API.Application.DTOs.Common;

namespace LIB.API.Application.DTOs.Reciept
{
    public class RecieptDto : BaseDtos
    {


        public int Id { get; set; }
        public string Inputing_Branch { get; set; }
        public DateTime Transaction_Date { get; set; }
        public string Account_No { get; set; }
        public string Phone_No { get; set; }
        public string Address { get; set; }
        public string TinNo { get; set; }
        public string Debitor_Name { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount1 { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal ServiceFee2 { get; set; }
        public decimal Vat2 { get; set; }
        public decimal Vat { get; set; }
        public string Refno { get; set; }
        public string Branch { get; set; }
        public string CAccountNo { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string MesssageNo { get; set; }
        public string PaymentNo { get; set; }
        public string PaymentType { get; set; }
    }
}
