using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class BookDetail: Base
    {
        public Guid BookID { get; set; }
        public Guid PriceID { get; set; }
        public Guid SiteID { get; set; }
        public Guid SiteItemID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double FinalPrice { get; set; }
        public double TotalPerItem { get; set; }
        public int StatusApprovalPerBillboard { get; set; }
        public string Note { get; set; }
        public virtual Book Book { get; set; }
    }
}
