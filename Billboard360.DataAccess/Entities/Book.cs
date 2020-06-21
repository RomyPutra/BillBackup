using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class Book : Base
    {
        public Guid? PaymentID { get; set; }
        public Guid CompanyID { get; set; }
        public Guid UserID { get; set; }
        public bool isNotified { get; set; }
        public int StatusApproval { get; set; }
        public string BookNo { get; set; }
        public virtual TitikLokasiDetail SiteItem { get; set; }
        
        public virtual User User { get; set; }
    }
}
