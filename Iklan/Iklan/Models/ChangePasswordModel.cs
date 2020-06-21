using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iklan.Models
{
    public class ChangePasswordInputModel
    {
        public Guid UserID { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }

    }

    public class ChangePasswordOutputModel
    {
        public Guid UserID { get; set; }

    }

    public class ChangePasswordResponseModel:BaseResponseModel
    {
        public ChangePasswordOutputModel data { get; set; }

    }
}
