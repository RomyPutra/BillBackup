using System;
using System.Collections.Generic;
using System.Text;

namespace Billboard360.DataAccess.Entities
{
    public class TitikLokasi: Base
    {
        public string NoBillboard { get; set; }
        public string Address { get; set; }
        public string AddressReal { get; set; }
        public string KelasJalan { get; set; }
        public string KodeFile { get; set; }
        public string PIC { get; set; }
        public string Kota { get; set; }
        public string Provinsi { get; set; }
        public string Pulau { get; set; }
        public string Type { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Cabang { get; set; }
        public string HorV { get; set; }
        public string Lampu { get; set; }
        public int Status { get; set; }
        public Guid AdvTypeID { get; set; }
        public bool AdaKontruksi { get; set; }
        public double Tinggi { get; set; }
        public int TotalView { get; set; }
        public double RateScoreTotal { get; set; }
        public int TransaksiCount { get; set; }
        public double RateScoreAverage { get; set; }
        public Guid OwnerByUserID { get; set; }
    }
}
