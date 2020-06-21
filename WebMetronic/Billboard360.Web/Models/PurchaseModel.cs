using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.Web.Models
{
    public class PurchaseInputModel
    {
        public double SubTotal { get; set; }
        public double Diskon { get; set; }
        public double GrandTotal { get; set; }
        public Guid UserID { get; set; }
        public string[] BookID { get; set; }
        public double UangMuka { get; set; }
        public int PPNProsen { get; set; }
    }

    public class PurchaseOutputModel
    {
        public Guid IDTransaction { get; set; }
        public string InvoiceNo { get; set; }
    }

    public class PurchaseResponseModel: BaseResponseModel
    {
        public PurchaseOutputModel data { get; set; }
    }
}
