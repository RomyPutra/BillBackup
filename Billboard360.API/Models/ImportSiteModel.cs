using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.API.Models
{
    public class ImportSiteModel
    {
        public string No { get; set; }
        public string CP { get; set; }
        public string KodeFile { get; set; }
        public string KodeLokasi { get; set; }
        public string KodeArahLokasi { get; set; }
        public string ArahLokasi { get; set; }
        public string Pulau { get; set; }
        public string KodePulau { get; set; }
        public string Provinsi { get; set; }
        public string KodeProvinsi { get; set; }
        public string Kota { get; set; }
        public string NamaCabang { get; set; }
        public string Alamat { get; set; }
        public double Panjang { get; set; }
        public double Lebar { get; set; }
        public double Luas { get; set; }
        public string HorV { get; set; }
        public string Type { get; set; }
        public string Lampu { get; set; }
        public double Qty { get; set; }
        public string Satuan { get; set; }
        public double HargaAwal { get; set; }
        public double HargaAkhir { get; set; }
        public string Cabang { get; set; }
        public string WW { get; set; }
        public string BID { get; set; }
        public string NonKop { get; set; }
        public string Note { get; set; }
        public string PIC { get; set; }
        public string Bendera { get; set; }
        public string ContactAgen { get; set; }
        public string Telepon { get; set; }
        public double Scrore { get; set; }
        public string Level { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string Keterangan { get; set; }
        public string LinkFolder { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string MinimDasar { get; set; }
        public int MinimOrder { get; set; }
        public string KelasJalan { get; set; }

    }

    public class ImportSiteInputModel
    {
        public Guid UserID { get; set; }
        public List<ImportSiteModel> Input { get; set; }
    }

    public class ImportSiteOutputModel
    {
        public DateTime ImportDate { get; set; }
    }

    public class ImportSiteResponseModel: BaseResponseModel
    {
        public ImportSiteOutputModel data { get; set; }
    }
}
