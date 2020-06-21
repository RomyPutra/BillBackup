using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class LogForgotPassword: Base
    {
        public string UserName { get; set; }
        public DateTime DateRequest { get; set; }
        public DateTime DateExpired { get; set; }
        public bool IsUsed { get; set; }
    }
}
