using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class TitikLokasiPrice: Base
    {
        public Guid TitikLokasiID { get; set; }
        public Guid TitikLokasiDetailID { get; set;}
        public string Dasar { get; set; }
        public double Kelipatan { get; set; }
        public double HargaAwal { get; set; }
        public double HargaAkhir { get; set; }
        public string MinimDasar { get; set; }
        public int MinimOrder { get; set; }

        public virtual TitikLokasiDetail TitikLokasiDetail { get; set; }

    }
}
