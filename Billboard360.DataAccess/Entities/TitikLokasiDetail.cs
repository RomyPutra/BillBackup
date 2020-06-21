using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class TitikLokasiDetail: Base
    {
        public Guid TitikLokasiID { get; set; }
        public string KodeArahLokasi { get; set; }
        public string ArahLokasi { get; set; }
        public double Panjang { get; set; }
        public double Lebar { get; set; }
        public string Keterangan { get; set; }

        public virtual TitikLokasi TitikLokasi { get; set; }

    }
}
