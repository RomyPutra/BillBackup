using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class MidTransLog:Base
    {
        public Guid PaymentID { get; set; }
        public int MidTransStatus { get; set; }
        public int ModeTransaction { get; set; }
        public DateTime Transaction_Time { get; set; }
        public string Transaction_Status { get; set; }
        public string Status_Message { get; set; }
        public string Status_Code { get; set; }
        public string Signature_Key { get; set; }
        public string Payment_Type { get; set; }
        public string Order_ID { get; set; }
        public string Merchant_ID { get; set; }
        public string Gross_Amount { get; set; }
        public string Currency { get; set; }
        public string Approval_Code { get; set; }
        public string Transaction_ID { get; set; }
        public string VANumber { get; set; }
        public string MidTransTransactionType { get; set; }
        public string BankName { get; set; }
    }
}
