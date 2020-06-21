using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class UserRole:Base
    {
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }

        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}
