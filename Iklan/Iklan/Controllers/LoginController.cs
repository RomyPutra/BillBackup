﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Iklan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Web.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Iklan.Controllers
{
    public class LoginController : Controller
    {
        const string SessionKeyID = "_ID";
        const string SessionKeyName = "_NAME";
		const string SessionKeyFName = "_FNAME";
		const string SessionKeyLName = "_LNAME";
		const string SessionKeyRole = "_ROLE";
		const string SessionKeyError = "_Error";
		const string SessionKeyCurrent = "_Current";
		const string SessionKeyDomain = "_Domain";
		const string SessionKeyVID = "_VID";
		const string SessionKeyToken = "_Token";
		const string Loginfrom = "_LoginF";
		string BaseAPI = "";
		string Domain = "";
		private readonly IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
            BaseAPI = _config.GetValue<string>("APIBaseUrl");
			Domain = _config.GetValue<string>("Domain");
		}

		public IActionResult Logon()
        {
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else if (TempData["CustomInfo"] != null)
				{
					ModelState.AddModelError("Info", TempData["CustomInfo"].ToString());
				}
				return View();
			}
		}

		public IActionResult BuyerLogon()
		{
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else if (TempData["CustomInfo"] != null)
				{
					ModelState.AddModelError("Info", TempData["CustomInfo"].ToString());
				}
				HttpContext.Session.SetString(Loginfrom, "MDB");
				return View();
			}
		}

		public IActionResult OwnerLogon()
		{
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else if (TempData["CustomInfo"] != null)
				{
					ModelState.AddModelError("Info", TempData["CustomInfo"].ToString());
				}
				HttpContext.Session.SetString(Loginfrom, "MDO");
				return View();
			}
		}

		public IActionResult AdminLogon()
		{
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else if (TempData["CustomInfo"] != null)
				{
					ModelState.AddModelError("Info", TempData["CustomInfo"].ToString());
				}
				HttpContext.Session.SetString(Loginfrom, "ADM/SPV");
				return View();
			}
		}

		public IActionResult Forgot()
		{
			if (TempData["CustomError"] != null)
			{
				ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
			}
			return View();
		}

		public IActionResult Reset()
		{
			if (TempData["CustomError"] != null)
			{
				ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
			}
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginInputModel data)
        {
			LoginInputModel login = new LoginInputModel();
			LoginOutputModel OutPutData = new LoginOutputModel();
			try
			{
				login.UserName = data.UserName;
				login.Password = data.Password;
				JsonConvert.SerializeObject(login);
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Profile/");
					//HTTP POST
					var postTask = client.PostAsJsonAsync<LoginInputModel>("Login", login);
					postTask.Wait();

					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						LoginResponseModel logincontent = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponseModel>(content.Result);
						if (logincontent.data != null && logincontent.data.UserID.ToString() != "" && logincontent.data.IsActive != false)
						{
							//if (logincontent.data.RoleName.ToString() == HttpContext.Session.GetString(Loginfrom) || HttpContext.Session.GetString(Loginfrom).Contains(logincontent.data.RoleName.ToString()))
							//{

							//}
							//else
							//{
							//	ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
							//}
							HttpContext.Session.SetString(SessionKeyID, logincontent.data.UserID.ToString());
							HttpContext.Session.SetString(SessionKeyName, logincontent.data.UserName.ToString());
							HttpContext.Session.SetString(SessionKeyFName, logincontent.data.FirstName.ToString());
							HttpContext.Session.SetString(SessionKeyLName, logincontent.data.LastName.ToString());
							HttpContext.Session.SetString(SessionKeyRole, logincontent.data.RoleName.ToString());
							HttpContext.Session.SetString(SessionKeyDomain, Domain);
							OutPutData = logincontent.data;
						}
						else
						{
							ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
						}
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
					}
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
			}
			return Json(OutPutData);
		}

		[HttpPost]
		public IActionResult ForgotPass(string email)
		{
			ForgotPasswordMobileInputModel data = new ForgotPasswordMobileInputModel();
			data.UserName = email;
			JsonConvert.SerializeObject(data);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Profile/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<ForgotPasswordMobileInputModel>("ForgotPassword", data);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					ForgotPasswordResponseModel forgot = Newtonsoft.Json.JsonConvert.DeserializeObject<ForgotPasswordResponseModel>(content.Result);
					if (forgot.data != null && forgot.data.ID.ToString() != "")
					{
						TempData["CustomInfo"] = "Email notification sended. Please check your email.";
						return RedirectToAction("Index", "Home");
					}
					else
					{
						TempData["CustomError"] = "Fail to update data. Please contact administrator.";
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					TempData["CustomError"] = "Terjadi kesalahan server. Hubungi admin.";
					return RedirectToAction("Index", "Home");
				}
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
		public IActionResult Regist(RegisterInputModel data)
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

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "MediaBuyer/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<RegisterInputModel>("Register", dataRegister);
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
						return RedirectToAction("Index", "Home");
					}
					else
					{
						HttpContext.Session.SetString(SessionKeyCurrent, "true");
						HttpContext.Session.SetString(SessionKeyError, "Gagal Daftar.");
						return RedirectToAction("ChangePassword", "Profile");
					}
				}
			}

			#region Old
			//if (conpassword == data.Password)
			//{
			//	if (registype == "MO")
			//	{
			//		using (var client = new HttpClient())
			//		{
			//			client.BaseAddress = new Uri(BaseAPI + "MediaOwner/");
			//			//HTTP POST
			//			var postTask = client.PostAsJsonAsync<RegisterInputModel>("Register", dataRegister);
			//			postTask.Wait();

			//			var result = postTask.Result;
			//			if (result.IsSuccessStatusCode)
			//			{
			//				var content = result.Content.ReadAsStringAsync();
			//				RegisterResponseModel logincontent = Newtonsoft.Json.JsonConvert.DeserializeObject<RegisterResponseModel>(content.Result);
			//				if (logincontent.data != null && logincontent.data.UserID.ToString() != "")
			//				{
			//					HttpContext.Session.SetString(SessionKeyCurrent, "");
			//					HttpContext.Session.SetString(SessionKeyError, "");
			//					return RedirectToAction("Index", "Home");
			//				}
			//				else
			//				{
			//					HttpContext.Session.SetString(SessionKeyCurrent, "true");
			//					HttpContext.Session.SetString(SessionKeyError, "Gagal Daftar.");
			//					return RedirectToAction("ChangePassword", "Profile");
			//				}
			//			}
			//		}
			//	}
			//	else if (registype == "MB")
			//	{
			//		using (var client = new HttpClient())
			//		{
			//			client.BaseAddress = new Uri(BaseAPI + "MediaBuyer/");
			//			//HTTP POST
			//			var postTask = client.PostAsJsonAsync<RegisterInputModel>("Register", dataRegister);
			//			postTask.Wait();

			//			var result = postTask.Result;
			//			if (result.IsSuccessStatusCode)
			//			{
			//				var content = result.Content.ReadAsStringAsync();
			//				RegisterResponseModel logincontent = Newtonsoft.Json.JsonConvert.DeserializeObject<RegisterResponseModel>(content.Result);
			//				if (logincontent.data != null && logincontent.data.UserID.ToString() != "")
			//				{
			//					HttpContext.Session.SetString(SessionKeyCurrent, "");
			//					HttpContext.Session.SetString(SessionKeyError, "");
			//					return RedirectToAction("Index", "Home");
			//				}
			//				else
			//				{
			//					HttpContext.Session.SetString(SessionKeyCurrent, "true");
			//					HttpContext.Session.SetString(SessionKeyError, "Gagal Daftar.");
			//					return RedirectToAction("ChangePassword", "Profile");
			//				}
			//			}
			//		}
			//	}
			//}
			#endregion

			HttpContext.Session.SetString(SessionKeyCurrent, "true");
			HttpContext.Session.SetString(SessionKeyError, "Terjadi kesalahan server. Hubungi admin.");
			return RedirectToAction("ChangePassword", "Profile");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		//public IActionResult ResetPassword(string token)
		//{
		//	if (token != "" && token != null)
		//	{
		//		ValidationForgotPasswordOutputModel Output = new ValidationForgotPasswordOutputModel();
		//		ValidationForgotPasswordInputModel data = new ValidationForgotPasswordInputModel();
		//		data.ForgotID = Guid.Parse(token);
		//		JsonConvert.SerializeObject(data);
		//		using (var client = new HttpClient())
		//		{
		//			client.BaseAddress = new Uri(BaseAPI + "Profile/");
		//			//HTTP POST
		//			var postTask = client.PostAsJsonAsync<ValidationForgotPasswordInputModel>("CheckValidIDForgotPassword", data);
		//			postTask.Wait();

		//			var result = postTask.Result;
		//			if (result.IsSuccessStatusCode && TempData["CustomError"] == null)
		//			{
		//				var content = result.Content.ReadAsStringAsync();
		//				ValidationForgotPasswodResponseModel resetContent = Newtonsoft.Json.JsonConvert.DeserializeObject<ValidationForgotPasswodResponseModel>(content.Result);
		//				Output = resetContent.data;
		//				if (Output == null)
		//				{
		//					return RedirectToAction("ErrorHandling", "Home");
		//				}
		//				else
		//				{
		//					HttpContext.Session.SetString(SessionKeyVID, Output.UserID.ToString());
		//					HttpContext.Session.SetString(SessionKeyToken, token);
		//				}
		//			}
		//			else if (TempData["CustomError"] == null)
		//			{
		//				TempData["CustomError"] = "Terjadi kesalahan server. Hubungi admin.";
		//			}
		//		}
		//		if (TempData["CustomError"] != null)
		//		{
		//			ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
		//		}
		//		return View();
		//	}
		//	else
		//	{
		//		return RedirectToAction("ErrorHandling", "Home");
		//	}
		//}

		//[HttpPost]
		//public ActionResult Change(string newpassword, string renewpassword)
		//{
		//	if (renewpassword == newpassword)
		//	{
		//		string userID = HttpContext.Session.GetString(SessionKeyVID);
		//		ChangePasswordForgotInputModel data = new ChangePasswordForgotInputModel();
		//		data.UserID = Guid.Parse(userID);
		//		data.NewPassword = renewpassword;
		//		JsonConvert.SerializeObject(data);
		//		using (var client = new HttpClient())
		//		{
		//			client.BaseAddress = new Uri(BaseAPI + "Profile/");
		//			//HTTP POST
		//			var postTask = client.PostAsJsonAsync<ChangePasswordForgotInputModel>("ChangePasswordForgot", data);
		//			postTask.Wait();

		//			var result = postTask.Result;
		//			if (result.IsSuccessStatusCode)
		//			{
		//				var datas = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result);
		//				if (datas.ToString().Contains("Gagal Ganti Password"))
		//				{
		//					//HttpContext.Session.SetString(SessionKeyCurrent, "true");
		//					//HttpContext.Session.SetString(SessionKeyError, "Gagal ganti password.");
		//					TempData["CustomError"] = "Gagal ganti password.";
		//					return RedirectToAction("ResetPassword", "Login", new { @token = HttpContext.Session.GetString(SessionKeyToken) });
		//				}
		//				else
		//				{
		//					HttpContext.Session.SetString(SessionKeyVID, "");
		//					HttpContext.Session.SetString(SessionKeyToken, "");
		//					if (HttpContext.Session.GetString(Loginfrom) == "ADM/SPV")
		//					{
		//						return RedirectToAction("AdminLogon", "Login");
		//					}
		//					else if (HttpContext.Session.GetString(Loginfrom) == "MDO")
		//					{
		//						return RedirectToAction("OwnerLogon", "Login");
		//					}
		//					else
		//					{
		//						return RedirectToAction("BuyerLogon", "Login");
		//					}
		//					//return RedirectToAction("Logon", "Login");
		//				}
		//			}
		//		}
		//		TempData["CustomError"] = "Terjadi kesalahan server. Hubungi admin.";
		//		return RedirectToAction("ResetPassword", "Login", new { @token = HttpContext.Session.GetString(SessionKeyToken) });
		//	}
		//	else
		//	{
		//		TempData["CustomError"] = "Password and Confirm passowrd not correct.";
		//		return RedirectToAction("ResetPassword", "Login", new { @token = HttpContext.Session.GetString(SessionKeyToken) });
		//	}
		//}

	}
}
