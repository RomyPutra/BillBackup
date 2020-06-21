using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iklan.Models
{
    public class RegisterInputModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string NamaPerusahaan { get; set; }
        public string EmailPerusahaan { get; set; }
        public string NoTelp { get; set; }
        public string Kategori { get; set; }
        public string NPWP { get; set; }
        public string Website { get; set; }
        public string Alamat { get; set; }
        public string Catatan { get; set; }
        public string Password { get; set; }
    }

    public class RegisterOutputModel
    {

        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterResponseModel: BaseResponseModel
    {
        public RegisterOutputModel data { get; set; }
    }

}
