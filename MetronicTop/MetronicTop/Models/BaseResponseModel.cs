using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetronicTop.Models
{
    public class BaseResponseModel
    {
        public bool Response { get; set; }
        public string Message { get; set; }

        public int TotalPages { get; set; }
        public int TotalData { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
