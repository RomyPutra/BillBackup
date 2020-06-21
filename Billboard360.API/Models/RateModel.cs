using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.API.Models
{
    public class AddRateInputModel
    {
        public double RateScore { get; set; }
        public Guid SiteID { get; set; }
        public Guid UserID { get; set; }
    }

    public class AddRateOutputModel
    {
        public Guid ID { get; set; }
    }

    public class AddRateResponseModel: BaseResponseModel
    {
        public AddRateOutputModel data { get; set; }
    }
}
