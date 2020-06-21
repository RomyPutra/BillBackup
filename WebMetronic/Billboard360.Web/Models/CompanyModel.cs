using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.Web.Models
{
    public class CompanyModel
    {
        public Guid UserID { get; set; }
        public string Email { get; set; }
        public string Alamat { get; set; }
        public string NamaPerusahaan { get; set; }
        public string Kategori { get; set; }
        public string NPWP { get; set; }
        public string Note { get; set; }
        public Guid RekeninBankID { get; set; }
        public string Website { get; set; }
    }
}
