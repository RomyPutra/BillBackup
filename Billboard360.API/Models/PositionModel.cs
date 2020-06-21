using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.API.Models
{
    public class PositionWebInputModel
    {
        public string Name { get; set; }
    }

    public class PositionResponse
    {        
            public Guid ID { get; set; }
            public string Message { get; set; }   
    }
}
