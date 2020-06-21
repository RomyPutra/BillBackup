using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class LogRateSite: Base
    {
        public Guid SiteID { get; set; }
        public Guid SiteItemID { get; set; }
        public Guid UserID { get; set; }
        public double RateScore { get; set; }


    }
}
