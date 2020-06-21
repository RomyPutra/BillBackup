using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.API.Models
{
    public class ContactPersonModel
    {
        public Guid CompanyID { get; set; }
        public string Email { get; set; }
        public string Jabatan { get; set; }
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
    }
}
