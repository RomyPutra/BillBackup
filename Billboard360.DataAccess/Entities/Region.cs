using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class Region : Base
    {
        public string KodeKota { get; set; }
        public string KotaOrKab { get; set; }
        public string KodeProvinsi { get; set; }
        public string Provinsi { get; set; }
        public string KodePulau { get; set; }
        public string Pulau { get; set; }
    }
}
