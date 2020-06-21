using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.Web.Models
{
	public class DeleteProvinceInputModel
	{
		public Guid UserID { get; set; }
		public Guid ProvinsiID { get; set; }
	}

	public class DeleteProvinceOutputModel
	{
		public string Message { get; set; }
	}

	public class DeleteProvinceResponseModel : BaseResponseModel
	{
		public DeleteProvinceOutputModel data { get; set; }
	}

	public class EditProvinceInputModel
	{
		public Guid UserID { get; set; }
		public Guid ProvinsiID { get; set; }
		public int Kode { get; set; }
		public string Provinsi { get; set; }
	}

	public class EditProvinceOutputModel
	{
		public Guid UserID { get; set; }
		public Guid ProvinsiID { get; set; }
		public int Kode { get; set; }
		public string Provinsi { get; set; }
	}

	public class EditProvinceResponseModel : BaseResponseModel
	{
		public EditProvinceOutputModel data { get; set; }
	}

	public class AddProvinceInputModel
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
