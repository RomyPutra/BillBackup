using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class Compare:Base
    {
        public Guid SiteID { get; set; }
        public Guid SiteItemID { get; set; }
        public Guid UserID { get; set; }

        public virtual TitikLokasi Site { get; set; }
    }
}
