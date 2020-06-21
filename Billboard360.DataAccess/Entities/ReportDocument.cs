using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class ReportDocument:Base
    {
        public Guid ReportID { get; set; }
        public string PathFile { get; set; }


    }
}
