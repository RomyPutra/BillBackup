using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class Company: Base
    {
        public Guid UserID { get; set; }
        public string Alamat { get; set; }
        public string Alias { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Kategory { get; set; }
        public bool isMain { get; set; }
        public string NPWP { get; set; }
        public string Note { get; set; }
        public string Website { get; set; }

        public virtual User User { get; set; }

    }
}
