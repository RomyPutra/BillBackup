using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.API.Models
{
    public class ChartModel
    {
        public double Januari { get; set; }
        public double Februari { get; set; }
        public double Maret { get; set; }
        public double April { get; set; }
        public double Mei { get; set; }
        public double Juni { get; set; }
        public double Juli { get; set; }
        public double Agustus { get; set; }
        public double September { get; set; }
        public double Oktober { get; set; }
        public double November { get; set; }
        public double Desember { get; set; }
    }

    public class chartResponse: BaseResponseModel
    {
        public ChartModel data { get; set; }
    }
}
