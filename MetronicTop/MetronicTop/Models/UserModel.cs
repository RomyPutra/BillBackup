﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetronicTop.Models
{
    public class UserModel
    {
    }

    public class ListUserInputModel : BaseResponseModel
	{
		public string UserName { get; set; }
		public string CompanyName { get; set; }
		public string RoleName { get; set; }
	}

	public class ListUserOutputModel
	{
		public Guid UserID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserName { get; set; }
		public string CompanyName { get; set; }
		public string RoleName { get; set; }
		public Guid RoleID { get; set; }
		public bool IsActive { get; set; }
		public string Address { get; set; }
		public string NoTelp { get; set; }
		public string Website { get; set; }
		public string EmailPerusahaan { get; set; }
		public string Kategory { get; set; }
		public string NPWP { get; set; }
		public string Note { get; set; }
	}

	public class ListUserResponseModel : BaseResponseModel
	{
		public List<ListUserOutputModel> data { get; set; }
	}

}