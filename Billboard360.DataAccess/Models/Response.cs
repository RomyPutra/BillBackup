using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Models
{
    public class Response
    {
        public Guid ID { get; set; }
        public string Message { get; set; }
        public bool Result { get; set; }
    }
}
