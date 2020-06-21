using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.API.Models
{
    public class LoggerInputModel
    {
        public string KodeLogger { get; set; }
        public string Message { get; set; }
    }

    public class LoggerOutputModel
    {
        public string KodeLogger { get; set; }
    }

    public class LoggerResponseModel:BaseResponseModel
    {
        public LoggerResponseModel data { get; set; }
    }
}
