using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class Payment:Base
    {
        public string InvoiceNo { get; set; }
        public double SubTotalPrice { get; set; }
        public double Diskon { get; set; }
        public double TotalPrice { get; set; }
        public Guid UserID { get; set; }
        public virtual User User { get; set; }
        public double TotalPaid { get; set; }
        public bool isLunas { get; set; }
        public Guid BookID { get; set; }
        public int PaymentType { get; set; }
        public int PPNProsen { get; set; }
    }
}
