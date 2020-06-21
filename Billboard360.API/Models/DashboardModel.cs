using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.API.Models
{
    public class ReportKotaEmpatInputModel
    {
        public Guid? UserID { get; set; }
    }

    public class RekapKotakEmpatOutputModel
    {
        public double TotalRevenue { get; set; }
        public double Revenue { get; set; }
        public int TotalOrder { get; set; }
        public int TotalBooking { get; set; }
        public int TotalUseSite { get; set; }
        public int TotalSite { get; set; }

    }

    public class Revenue
    {
        public double TotalRevenue { get; set; }
        public double PartRevenue { get; set; }
        public Guid UserID { get; set; }
    }

    public class RekapKotakEmpatResponseModel:BaseResponseModel
    {
        public RekapKotakEmpatOutputModel data { get; set; }
    }
}
