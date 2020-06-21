using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.API.Models
{
    public class ForgotPasswordMobileInputModel
    {
        public string UserName { get; set; }
    }

    public class ForgotPasswordMobileOutputModel
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string URL { get; set; }
    }

    public class ForgotPasswordResponseModel:BaseResponseModel
    {
        public ForgotPasswordMobileOutputModel data { get; set; }
    }

    public class ValidationForgotPasswordInputModel
    {
        public Guid ForgotID { get; set; }
    }

    public class ValidationForgotPasswordOutputModel
    {
        public bool IsStillUse { get; set; }
        public string UserName { get; set; }
        public Guid UserID { get; set; }
    }

    public class ValidationForgotPasswodResponseModel:BaseResponseModel
    {
        public ValidationForgotPasswordOutputModel data { get; set; }
    }
}
