using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.API.Models
{

    public class DeleteProvinceInputModel
    {
        public Guid UserID { get; set; }
        public Guid ProvinsiID { get; set; }
    }

    public class DeleteProviceOutputModel
    {
        public string Message { get; set; }
    }

    public class DeleteProvinceResponseModel : BaseResponseModel
    {
        public DeleteProviceOutputModel data { get; set; }
    }

    public class EditProviceInputModel
    {
        public Guid UserID { get; set; }
        public Guid ProvinsiID { get; set; }
        public int Kode { get; set; }
        public string Provinsi { get; set; }
    }

    public class EditProviceOutputModel
    {
        public Guid UserID { get; set; }
        public Guid ProvinsiID { get; set; }
        public int Kode { get; set; }
        public string Provinsi { get; set; }
    }

    public class EditProvinceResponseModel : BaseResponseModel
    {
        public EditProviceOutputModel data { get; set; }
    }

    public class AddProviceInputModel
    {
        public Guid UserID { get; set; }
        public int Kode { get; set; }
        public string Provinsi { get; set; }
    }

    public class AddProvinceOutputModel
    {
        public Guid ProvinsiID { get; set; }
    }

    public class AddProvinceResponseModel : BaseResponseModel
    {
        public AddProvinceOutputModel data { get; set; }
    }

    public class ProviceOutputModel
    {
        public Guid ProvinceID { get; set; }
        public int Kode { get; set; }
        public string Provinsi { get; set; }
    }

    public class ProvinceResponseModel : BaseResponseModel
    {
        public List<ProviceOutputModel> data { get; set; }
    }

    public class ProvinceDetailResponseModel : BaseResponseModel
    {
        public ProviceOutputModel data { get; set; }
    }

    public class SyncProvinceAPIResultModel
    {
        public string id { get; set; }
        public string nama { get; set; }
    }

    public class SyncProvinceAPIResponseModel
    {
        public bool error { get; set; }
        public string message { get; set; }
        public List<SyncProvinceAPIResultModel> provinsi { get; set; }
    }

}
