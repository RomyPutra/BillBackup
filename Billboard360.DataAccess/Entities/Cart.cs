using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class Cart: Base
    {
        public Guid PriceID { get; set; }
        public Guid SiteID { get; set; }
        public Guid SiteItemID { get; set; }
        public Guid CompanyID { get; set; }
        public Guid UserID { get; set; }
        public Guid? BookID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double TotalPerItem { get; set; }
        public bool isNotified { get; set; }
        public int StatusApproval { get; set; }
        public virtual TitikLokasiDetail SiteItem { get; set; }

        public virtual User User { get; set; }
    }
}
