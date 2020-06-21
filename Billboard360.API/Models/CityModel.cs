using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.API.Models
{
    public class CityInputModel
    {
        public int KodeProv { get; set; }
    }

    public class CityOuputModel
    {
        public Guid? ProvinceID { get; set; }
        public Guid CityID { get; set; }
        public int Kode { get; set; }
        public string Kota { get; set; }
    }

    public class CityResponseModel : BaseResponseModel
    {
        public List<CityOuputModel> data { get; set; }
    }

    public class CityDetailResponseModel : BaseResponseModel
    {
        public CityOuputModel data { get; set; }
    }

    public class AddCityInputModel
    {
        public Guid UserID { get; set;}
        public String CityName { get; set; }
        public String Kode { get; set; }
        public string KodeProvinsi { get; set; }
    }

    public class AddCityOutputModel
    {
        public Guid CityID { get; set; }
        public string CityName { get; set; }
    }

    public class AddCityResponseModel: BaseResponseModel
    {
        public AddCityOutputModel data { get; set; }
    }

    public class EditCityInputModel
    {
        public Guid UserID { get; set; }
        public Guid CityID { get; set; }
        public String CityName { get; set; }
        public String Kode { get; set; }
        public string KodeProvinsi { get; set; }
    }

    public class EditCityOutputModel
    {
        public Guid CityID { get; set; }
        public string CityName { get; set; }
    }

    public class EditCityResponseModel : BaseResponseModel
    {
        public EditCityOutputModel data { get; set; }
    }


    public class SyncCityResultModel
    {
        public string id { get; set; }
        public string id_prov { get; set; }
        public string nama { get; set; }

    }

    public class SyncCityResponseModel:SyncCityMessageResponseModel
    {
        
        public List<SyncCityResultModel> kabupatens { get; set; }
    }

    public class SyncCityMessageResponseModel
    {
        public bool error { get; set; }
        public string message { get; set; }
    }
}
