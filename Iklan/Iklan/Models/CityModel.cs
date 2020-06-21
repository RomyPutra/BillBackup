using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iklan.Models
{
	public class CityInputModel
	{
		public int KodeProv { get; set; }
	}

	public class CityOutputModel
	{
		public Guid ProvinceID { get; set; }
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
		public CityOutputModel data { get; set; }
	}

	public class AddCityInputModel
	{
		public Guid UserID { get; set; }
		public String CityName { get; set; }
		public String Kode { get; set; }
		public string KodeProvinsi { get; set; }
	}

	public class AddCityOutputModel
	{
		public Guid CityID { get; set; }
		public string CityName { get; set; }
	}

	public class AddCityResponseModel : BaseResponseModel
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
}
