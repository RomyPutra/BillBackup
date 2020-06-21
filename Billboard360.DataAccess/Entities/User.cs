using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class User: Base
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberConfirmed { get; set; } //Tanyakan kenapa ini string bukan boolean?
        public bool TwoFactorEnabled { get; set; } 
        public DateTime LockOutEnd { get; set; } //Tanyakan boleh null atau tidak?
        public bool LockOutEnabled { get; set; } 
        public int AccessFailedCount { get; set; }
        public bool SignInToMobile { get; set; }
        public bool IsActive { get; set; }
    }
}
