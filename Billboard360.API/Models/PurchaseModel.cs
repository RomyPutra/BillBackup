using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.API.Models
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

    
    public class ReportPurchaseAndBookInputModel : BaseModel
    {
        public bool IsPurchased { get; set; }
        public bool IsBook { get; set; }
        public string UserName { get; set; }
        public string NoBillboard { get; set; }
        public DateTime? TanggalPurchaseAwal { get; set; }
        public DateTime? TanggalPurchaseAkhir { get; set; }
    }

    public class ReportPurchaseAndBookOutputModel
    {
        public Guid BookID { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string NoBillBoard { get; set; }
        public int Qty { get; set; }
        public string Unit { get; set; }
        public double Harga { get; set; }
        public double HargaTotal { get; set; }
        public DateTime TglBook { get; set; }
        public string TglTrans { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BookNo { get; set; }
        public Guid OwnerSiteID { get; set; }

    }

    public class ReportPurchaseAndBookResultModel
    {
        public List<ReportPurchaseAndBookOutputModel> result { get; set; }
        public int TotalPages { get; set; }
        public int TotalData { get; set; }


    }

    public class ReportPurchaseAndModelResponseModel:BaseResponseModel
    {
        public List<ReportPurchaseAndBookOutputModel> data { get; set; }
    }

}
