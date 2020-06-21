using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetronicTop.Models
{
    public class ProvinceModel
    {
    }

	public class ProvinceOutputModel
	{
		public Guid ProvinceID { get; set; }
		public int Kode { get; set; }
		public string Provinsi { get; set; }
	}

	public class ProvinceResponseModel : BaseResponseModel
	{
		public List<ProvinceOutputModel> data { get; set; }
	}

	public class ProvinceDetailResponseModel : BaseResponseModel
	{
		public ProvinceOutputModel data { get; set; }
	}
}
