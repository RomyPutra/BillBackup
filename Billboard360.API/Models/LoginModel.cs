using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.API.Models
{
    public class LoginInputModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginOutputModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public Guid RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }

    public class LoginResponseModel: BaseResponseModel
    {
        public LoginOutputModel data { get; set; }
    }


    public class ChangePassInputModel
    {
        public string UserName { get; set; }
        public string NewPassword { get; set; }
        public bool useOldPassword { get; set; }
        public string OldPassword { get; set; }

    }

    public class ChangePassOutputModel
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public bool Response { get; set; }
        public string Message { get; set; }
    }

    
}
