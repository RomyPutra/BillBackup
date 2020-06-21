using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Billboard360.Web.Models;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Billboard360.Web.Controllers
{
    public class UsersController : Controller
    {
        const string SessionKeyID = "_ID";
        const string SessionKeyName = "_NAME";
		const string SessionKeyRole = "_ROLE";
		const string SessionKeyError = "_Error";
		const string SessionKeyCurrent = "_Current";
		const string Loginfrom = "_LoginF";
		string BaseAPI = "";
		string Domain = "";
		private readonly IConfiguration _config;
        public UsersController(IConfiguration config)
        {
            _config = config;
            BaseAPI = _config.GetValue<string>("APIBaseUrl");
			Domain = _config.GetValue<string>("Domain");
		}

		// GET: /<controller>/
		public IActionResult Index()
        {
            if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
            {
                IEnumerable<ListUserOutputModel> users = null;
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else
				{
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(BaseAPI + "Admin/");
						ListUserInputModel filter = new ListUserInputModel();
						filter.UserName = "";
						filter.CompanyName = "";
						filter.RoleName = "";
						filter.PageNumber = 1;
						filter.PageSize = 1000;
						var responseTask = client.PostAsJsonAsync<ListUserInputModel>("GetUserList", filter);
						responseTask.Wait();

						var result = responseTask.Result;
						if (result.IsSuccessStatusCode)
						{
							var content = result.Content.ReadAsStringAsync();
							ListUserResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ListUserResponseModel>(content.Result);
							users = resutl.data;
						}
						else //web api sent error response 
						{
							//log response status here..
							users = Enumerable.Empty<ListUserOutputModel>();
							ModelState.AddModelError(string.Empty, "Gagal mendapatkan data. Mohon hubungi admin.");
						}
					}
				}
				return View(users);
            }
            else
            {
				TempData["CustomError"] = "Silakan masuk sebelum menggunakan situs web.";
				//if (HttpContext.Session.GetString(Loginfrom) == "ADM/SPV")
				//{
				//	return RedirectToAction("AdminLogon", "Login");
				//}
				//else if (HttpContext.Session.GetString(Loginfrom) == "MDO")
				//{
				//	return RedirectToAction("OwnerLogon", "Login");
				//}
				//else
				//{
				//	return RedirectToAction("OwnerLogon", "Login");
				//}
				return RedirectToAction("Logon", "Login");
			}
		}

   //     public IActionResult Create()
   //     {
			//if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			//{
			//	return View();
			//}
			//else
			//{
			//	ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
			//	return RedirectToAction("Logon", "Login");
			//}
   //     }

   //     [HttpPost]
   //     public ActionResult AddBank(string kode, string nama, string logobank)
   //     {
   //         string userID = HttpContext.Session.GetString(SessionKeyID);
   //         AddBankInputModel dataUsers = new AddBankInputModel();
   //         dataUsers.Kode = kode;
   //         dataUsers.Nama = nama;
   //         dataUsers.LogoBank = logobank;
   //         dataUsers.IsActive = true;
   //         dataUsers.CreateByUserID = Guid.Parse(userID);
   //         JsonConvert.SerializeObject(dataUsers);
   //         using (var client = new HttpClient())
   //         {
   //             client.BaseAddress = new Uri(BaseAPI + "Admin/");
   //             //HTTP POST
   //             var postTask = client.PostAsJsonAsync<AddBankInputModel>("AddMasterBank", dataUsers);
   //             postTask.Wait();

   //             var result = postTask.Result;
   //             if (result.IsSuccessStatusCode)
   //             {
   //                 return RedirectToAction("Index", "Users");
   //             }
   //         }

   //         ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");

   //         return RedirectToAction("Logon", "Login");
   //     }

        public ActionResult Update(string ID)
        {
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				string userID = HttpContext.Session.GetString(SessionKeyID);
				UserEnableDisabledInputModel dataUsers = new UserEnableDisabledInputModel();
				dataUsers.CurrentUserIDLogin = Guid.Parse(userID);
				dataUsers.UserID = Guid.Parse(ID.Split("&")[0]);
				dataUsers.IsActive = bool.Parse(ID.Split("&")[1]);
				JsonConvert.SerializeObject(dataUsers);
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Admin/");
					var postTask = client.PostAsJsonAsync<UserEnableDisabledInputModel>("EnableDisabledUser", dataUsers);
					postTask.Wait();

					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						return RedirectToAction("Index", "Users");
					}
					else
					{
						TempData["CustomError"] = "Gagal memperbarui data. Mohon hubungi admin.";
						return RedirectToAction("Index", "Users");
					}
				}
			}
			else
			{
				TempData["CustomError"] = "Silakan masuk sebelum menggunakan situs web.";
				//if (HttpContext.Session.GetString(Loginfrom) == "ADM/SPV")
				//{
				//	return RedirectToAction("AdminLogon", "Login");
				//}
				//else if (HttpContext.Session.GetString(Loginfrom) == "MDO")
				//{
				//	return RedirectToAction("OwnerLogon", "Login");
				//}
				//else
				//{
				//	return RedirectToAction("OwnerLogon", "Login");
				//}
				return RedirectToAction("Logon", "Login");
			}
		}

		//     [HttpPost]
		//     public ActionResult UpdateUsers(string usersID, string kode, string nama, string logobank)
		//     {
		//         string userID = HttpContext.Session.GetString(SessionKeyID);
		//UserEnableDisabledInputModel dataUsers = new UserEnableDisabledInputModel();
		//dataUsers.CurrentUserIDLogin = Guid.Parse(userID);
		//dataUsers.UserID = Guid.Parse(usersID);
		//         dataUsers.IsActive = true;
		//         JsonConvert.SerializeObject(dataUsers);
		//         using (var client = new HttpClient())
		//         {
		//             client.BaseAddress = new Uri(BaseAPI + "Admin/");
		//             //HTTP POST
		//             var postTask = client.PostAsJsonAsync<UserEnableDisabledInputModel>("EnableDisabledUser", dataUsers);
		//             postTask.Wait();

		//             var result = postTask.Result;
		//             if (result.IsSuccessStatusCode)
		//             {
		//                 return RedirectToAction("Index", "Users");
		//             }
		//         }

		//         ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");

		//         return RedirectToAction("Logon", "Login");
		//     }

		//[HttpPost]
		//public ActionResult Delete(string id)
		//{
		//	string userID = HttpContext.Session.GetString(SessionKeyID);
		//	DeleteBankInputModel dataUsers = new DeleteBankInputModel();
		//	dataUsers.BankID = Guid.Parse(id);
		//	dataUsers.UserID = Guid.Parse(userID);
		//	JsonConvert.SerializeObject(dataUsers);
		//	using (var client = new HttpClient())
		//	{
		//		client.BaseAddress = new Uri(BaseAPI + "Admin/");
		//		//HTTP POST
		//		var postTask = client.PostAsJsonAsync<DeleteBankInputModel>("DeleteBank", dataUsers);
		//		postTask.Wait();

		//		var result = postTask.Result;
		//		if (result.IsSuccessStatusCode)
		//		{
		//			return RedirectToAction("Index", "Users");
		//		}
		//	}

		//	ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");

		//	return RedirectToAction("Logon", "Login");
		//}

		public IActionResult Register()
		{
			if (HttpContext.Session.GetString(SessionKeyCurrent) != null && HttpContext.Session.GetString(SessionKeyCurrent) != "")
			{
				if (HttpContext.Session.GetString(SessionKeyError) != null && HttpContext.Session.GetString(SessionKeyError) != " ")
				{
					ModelState.AddModelError("Error", HttpContext.Session.GetString(SessionKeyError));
				}
				else
				{
					ModelState.AddModelError("Error", " ");
				}
				HttpContext.Session.SetString(SessionKeyCurrent, "");
				HttpContext.Session.SetString(SessionKeyError, "");
			}
			else
			{
				HttpContext.Session.SetString(SessionKeyError, "");
				ModelState.AddModelError("Error", " ");
			}
			return View();
		}

		[HttpPost]
		public IActionResult Regist(RegisterInputModel data, string registype, string conpassword)
		{
			RegisterInputModel dataRegister = new RegisterInputModel();
			dataRegister.FirstName = data.FirstName;
			dataRegister.LastName = data.LastName;
			dataRegister.Username = data.Username;
			dataRegister.NamaPerusahaan = data.NamaPerusahaan;
			dataRegister.EmailPerusahaan = data.EmailPerusahaan;
			dataRegister.NoTelp = data.NoTelp;
			dataRegister.Kategori = data.Kategori;
			dataRegister.NPWP = data.NPWP;
			dataRegister.Website = data.Website;
			dataRegister.Alamat = data.Alamat;
			dataRegister.Catatan = data.Catatan;
			dataRegister.Password = data.Password;
			JsonConvert.SerializeObject(dataRegister);

			if (conpassword == data.Password)
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Admin/");
					//HTTP POST
					var postTask = client.PostAsJsonAsync<RegisterInputModel>("AddSupervisor", dataRegister);
					postTask.Wait();

					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						RegisterResponseModel logincontent = Newtonsoft.Json.JsonConvert.DeserializeObject<RegisterResponseModel>(content.Result);
						if (logincontent.data != null && logincontent.data.UserID.ToString() != "")
						{
							HttpContext.Session.SetString(SessionKeyCurrent, "");
							HttpContext.Session.SetString(SessionKeyError, "");
							return RedirectToAction("Index", "Users");
						}
						else
						{
							HttpContext.Session.SetString(SessionKeyCurrent, "true");
							HttpContext.Session.SetString(SessionKeyError, "Gagal mendaftarkan.");
							return RedirectToAction("Register", "Users");
						}
					}
				}
			}

			HttpContext.Session.SetString(SessionKeyCurrent, "true");
			HttpContext.Session.SetString(SessionKeyError, "Terjadi kesalahan. Mohon hubungi admin.");
			return RedirectToAction("Register", "Users");
		}

	}
}
