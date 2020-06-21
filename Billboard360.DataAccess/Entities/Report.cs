using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class Report: Base
    {
        public Guid SiteID { get; set; }
        public Guid SiteItemID { get; set; }
        public Guid UserID { get; set; }
        public string ReportNo { get; set; }
        public bool isToSPV { get; set; }
        public string Desc { get; set; }
        
    }
}
