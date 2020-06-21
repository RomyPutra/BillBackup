using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class City : Base
    {
        public int Kode {get; set;}
        public Guid ProvinceID { get; set;}
        public int KodeProvinsi { get; set; }
        public string CityName { get; set; }

        public virtual Province Province { get; set; }

    }
}
