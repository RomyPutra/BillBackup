using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iklan.Models
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

    public class CompanyCategoryOutputModel
    {
        public Guid CompanyCategoryID { get; set; }
        public int KodeKategori { get; set; }
        public string Nama { get; set; }
    }

    public class CompanyCategoryResponseModel : BaseModel
    {
        public List<CompanyCategoryOutputModel> Data { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
