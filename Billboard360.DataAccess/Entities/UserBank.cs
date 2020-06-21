using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class UserBank: Base
    {
        public Guid UserID { get; set; }
        public Guid BankID { get; set; }
        public bool isSelected { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }

        public virtual User User { get; set; }
        public virtual Bank Bank { get; set; }

    }
}
