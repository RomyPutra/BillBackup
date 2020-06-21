using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class ContactPerson : Base
    {
        public Guid CompanyID { get; set; }
        public string Email { get; set; }
        public string Jabatan { get; set; }
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }

        public virtual Company Company { get; set; }


    }
}
