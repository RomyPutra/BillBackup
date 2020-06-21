using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Billboard360.Web.Models
{
	public class EditProfile
	{
		public ProfileOutputModel val { get; set; }
		public EditProfileInputModel input { get; set; }
	}

	public class ProfileInputModel
	{
		public Guid UserID { get; set; }
	}

	public class ProfileOutputModel
	{
		public Guid UserID { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhotoUrl { get; set; }
		public string EmailPerusahaan { get; set; }
		public string EmailPIC { get; set; }
		public string NoTelp { get; set; }
		public string NamaPerusahaan { get; set; }
		public string NPWP { get; set; }
		public string Kategori { get; set; }
		public string Website { get; set; }
		public string Alamat { get; set; }
		public string Catatan { get; set; }
		public int CountCart { get; set; }
		public bool IsHaveUserBank { get; set; }
		public string PPNDefault { get; set; }
	}

	public class ProfileResponseModel : BaseResponseModel
	{
		public ProfileOutputModel data { get; set; }
	}

	public class EditProfileInputModel
	{
		public Guid UserID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string EmailPIC { get; set; }
		public string NamaPerusahaan { get; set; }
		public string EmailPerusahaan { get; set; }
		public string NoTelp { get; set; }
		public string Kategori { get; set; }
		public string NPWP { get; set; }
		public string Website { get; set; }
		public string Alamat { get; set; }
		public string Catatan { get; set; }
	}

	public class EditProfileOutputModel
	{
		public Guid UserID { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhotoUrl { get; set; }
		public string EmailPerusahaan { get; set; }
		public string EmailPIC { get; set; }
		public string NoTelp { get; set; }
		public string NamaPerusahaan { get; set; }
		public string NPWP { get; set; }
		public string Kategori { get; set; }
		public string Website { get; set; }
		public string Alamat { get; set; }
		public string Catatan { get; set; }
	}

	public class EditProfileResponseModel : BaseResponseModel
	{
		public EditProfileOutputModel data { get; set; }
	}
}
