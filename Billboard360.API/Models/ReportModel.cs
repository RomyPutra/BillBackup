using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.API.Models
{
    public class AddReportProblemInputModel
    {
        public Guid SiteID { get; set; }
        public Guid UserID { get; set; }
        public string Desc { get; set; }
        public bool isToSPV { get; set; }
    }

    public class AddReportProblemOutputModel
    {
        public Guid ReportID { get; set; }
        public string ReportNo { get; set; }
    }

    public class AddReportProblemResponseModel : BaseResponseModel
    {
        public AddReportProblemOutputModel data { get; set; }
    }

    public class ReportProblemListInputModel:BaseModel
    {
        public bool IsToSPV { get; set; }
    }

    public class ReportProblemListOutputModel
    {
        public Guid ReportProblemID { get; set; }
        public string ReportNo { get; set; }
        public string NoBillboard { get; set; }
        public string Desc { get; set; }
        public string From { get; set; }
        public bool IsToSPV { get; set; }
    }

    public class ReportProblemListResponseModel :BaseResponseModel
    {
        public List<ReportProblemListOutputModel> data { get; set; }
    }

    public class ReportProblemListResultModel: BaseResponseModel
    {
        public List<ReportProblemListOutputModel> data { get; set; }
        
    }
}
