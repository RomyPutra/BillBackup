using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Iklan.Models;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Iklan.Controllers
{
	public class ProfileController : Controller
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
		public ProfileController(IConfiguration config)
		{
			_config = config;
			BaseAPI = _config.GetValue<string>("APIBaseUrl");
			Domain = _config.GetValue<string>("Domain");
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Update()
		{
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				EditProfile data = new EditProfile();
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Profile/");
					ProfileInputModel filter = new ProfileInputModel();
					filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID).ToString());
					var responseTask = client.PostAsJsonAsync<ProfileInputModel>("GetProfile", filter);
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						ProfileResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ProfileResponseModel>(content.Result);
						data.val = resutl.data;
					}
					else //web api sent error response 
					{
						//log response status here..
						ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
					}
				}
				return View(data);
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public ActionResult UpdateProfile(EditProfileInputModel data)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			EditProfileInputModel dataProfile = new EditProfileInputModel();
			dataProfile = data;
			dataProfile.EmailPIC = dataProfile.UserName;
			dataProfile.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID).ToString());
			JsonConvert.SerializeObject(dataProfile);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Profile/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<EditProfileInputModel>("EditProfile", dataProfile);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("Index", "Home");
				}
			}
			ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
			return RedirectToAction("Update", "Profile");
		}

		// GET: /<controller>/
		public IActionResult ChangePassword()
        {
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
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
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public ActionResult Change(ChangePasswordInputModel inputData)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			ChangePasswordInputModel data = new ChangePasswordInputModel();
			try
			{
				data = inputData;
				data.UserID = Guid.Parse(userID);
				JsonConvert.SerializeObject(data);
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Profile/");
					//HTTP POST
					var postTask = client.PostAsJsonAsync<ChangePasswordInputModel>("ChangePassword", data);
					postTask.Wait();

					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var datas = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result);
						if (datas.ToString().Contains("Gagal ganti password"))
						{
							HttpContext.Session.SetString(SessionKeyCurrent, "true");
							HttpContext.Session.SetString(SessionKeyError, "Gagal ganti password.");
							return RedirectToAction("ChangePassword", "Profile");
						}
						else
						{
							HttpContext.Session.SetString(SessionKeyID, "");
							HttpContext.Session.SetString(SessionKeyName, "");
							HttpContext.Session.SetString(SessionKeyRole, "");
							return RedirectToAction("Index", "Home");
						}
					}
				}
				HttpContext.Session.SetString(SessionKeyCurrent, "true");
				HttpContext.Session.SetString(SessionKeyError, "Terjadi kesalahan server. Hubungi admin.");
				return RedirectToAction("ChangePassword", "Profile");
			}
			catch (Exception e)
			{
				ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
				return RedirectToAction("ChangePassword", "Profile");
			}
		}

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
							HttpContext.Session.SetString(SessionKeyError, "Gagal Daftar.");
							return RedirectToAction("ChangePassword", "Profile");
						}
					}
				}
			}

			HttpContext.Session.SetString(SessionKeyCurrent, "true");
			HttpContext.Session.SetString(SessionKeyError, "Terjadi kesalahan server. Hubungi admin.");
			return RedirectToAction("ChangePassword", "Profile");
		}

		public IActionResult LogOut()
        {
            HttpContext.Session.SetString(SessionKeyID, "");
            HttpContext.Session.SetString(SessionKeyName, "");
			HttpContext.Session.SetString(SessionKeyRole, "");
			return RedirectToAction("Index", "Home");
		}
	}
}
