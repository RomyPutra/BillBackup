using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetronicTop.Models
{
    public class CityModel
    {
    }

	public class CityOutputModel
	{
		public Guid CityID { get; set; }
		public int Kode { get; set; }
		public string Kota { get; set; }
	}

	public class CityResponseModel : BaseResponseModel
	{
		public List<CityOutputModel> data { get; set; }
	}

	public class CityDetailResponseModel : BaseResponseModel
	{
		public ProvinceOutputModel data { get; set; }
	}
}
