using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billboard360.DataAccess.Models.Enums;

namespace Billboard360.API.Models
{
    public class MidTransConfigModel
    {
        public string SERVER_KEY { get; set; }
        public string AUTH_KEY { get; set; }
        public string AUTH_STRING { get; set; }
    }

    public class MidTransTransactionResponseModel
    {
        public Guid token { get; set; }
        public string redirect_url { get; set; }
    }

    public class MidTransChargeInputModel
    {
        public CustomerDetails customer_details { get; set; }
        public TransactionDetails transaction_details { get; set; }
        public string user_id { get; set; }
        public string[] item_details { get; set; }
        
    }

    public class TransactionDetails
    {
        public string currency { get; set; }
        public string order_id { get; set; }
        public double gross_amount { get; set; }
    }

    public class CustomerDetails
    {
        public BillingAddress billing_address { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string phone { get; set; }
        public ShippingAddress shipping_address { get; set; }
    }

    public class BillingAddress
    {
        public string address { get; set; }
        public string city { get; set; }
        public string country_code { get; set; }
        public string first_name { get; set; }
        public string phone { get; set; }
        public string postal_code { get; set; }
    }

    public class ShippingAddress
    {
        public string address { get; set; }
        public string city { get; set; }
        public string country_code { get; set; }
        public string first_name { get; set; }
        public string phone { get; set; }
        public string postal_code { get; set; }
    }

    public class UpdatePaymentStatusInputModel:BaseModel
    {
        public string InvoiceNumber { get; set; }
        public MidtransStatusEnum Status { get; set; }
        public Guid UserID { get; set; }
        public string VANumber { get; set; }
        public string BankName { get; set; }        
        public string MidTransPaymentType { get; set; }
    }

    public class UpdatePaymentStatusOutputModel
    {
        
         public string Message { get; set; }

    }

    public class UpdatePaymentStatusResponseModel : BaseResponseModel
    {
        public UpdatePaymentStatusOutputModel data { get; set; }
    }

}
