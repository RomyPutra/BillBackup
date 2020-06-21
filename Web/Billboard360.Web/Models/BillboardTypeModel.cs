using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.Web.Models
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
