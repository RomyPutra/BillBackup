using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Billboard360.API.Models;
using Billboard360.API.BussinessLogic;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;

namespace Billboard360.API.Models
{

    public class AddBillboardTypeInputModel
    {
        public string Kode { get; set; }
        public string Type { get; set; }
    }

    public class AddBillboardTypeOutputModel
    {
        public Guid ID { get; set; }
    }

    public class AddBillboardTypeResponseModel: BaseResponseModel
    {
        public AddBillboardTypeOutputModel data { get; set; }
    }

    public class BillboardTypeListOutputModel
    {
        public Guid ID { get; set; }
        public string Kode { get; set; }
        public string Type { get; set; }
    }

    public class BillboardTypeListResponseModel: BaseResponseModel
    {
        public List<BillboardTypeListOutputModel> data { get; set; }
    }
}
