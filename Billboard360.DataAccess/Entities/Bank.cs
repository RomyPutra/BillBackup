using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class Bank: Base
    {
        public string Kode { get; set; }
        public string BankName { get; set; }
        public string LogoBank { get; set; }
        public bool IsActive { get; set; }
    }
}
