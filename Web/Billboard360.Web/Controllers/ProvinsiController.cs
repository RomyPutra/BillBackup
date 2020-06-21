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
	public class ProvinceController : Controller
	{
		const string SessionKeyID = "_ID";
		const string SessionKeyName = "_NAME";
		const string SessionKeyRole = "_ROLE";
		const string SessionKeyEdit = "_EDIT";
		const string Loginfrom = "_LoginF";
		string BaseAPI = "";
		string Domain = "";
		private readonly IConfiguration _config;
		public ProvinceController(IConfiguration config)
		{
			_config = config;
			BaseAPI = _config.GetValue<string>("APIBaseUrl");
			Domain = _config.GetValue<string>("Domain");
		}

		// GET: /<controller>/
		public IActionResult Index()
		{
			HttpContext.Session.SetString(SessionKeyEdit, "");
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				List<ProvinceOutputModel> province = new List<ProvinceOutputModel>();
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else
				{
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(BaseAPI + "Base/");
						var provTask = client.PostAsync("GetProvince", null);
						provTask.Wait();
						var results = provTask.Result;
						if (results.IsSuccessStatusCode)
						{
							var content = results.Content.ReadAsStringAsync();
							ProvinceResponseModel provinceResponse = JsonConvert.DeserializeObject<ProvinceResponseModel>(content.Result);
							province = provinceResponse.data.OrderBy(x => x.Kode).ToList();
						}
						else
						{
							province = null;
							ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
						}
					}
				}
				return View(province);
			}
			else
			{
				TempData["CustomError"] = "Please login before using the web.";
				if (HttpContext.Session.GetString(Loginfrom) == "ADM/SPV")
				{
					return RedirectToAction("AdminLogon", "Login");
				}
				else if (HttpContext.Session.GetString(Loginfrom) == "MDO")
				{
					return RedirectToAction("OwnerLogon", "Login");
				}
				else
				{
					return RedirectToAction("BuyerLogon", "Login");
				}
				//return RedirectToAction("Logon", "Login");
			}
		}

		public IActionResult Create()
		{
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				return View();
			}
			else
			{
				TempData["CustomError"] = "Please login before using the web.";
				if (HttpContext.Session.GetString(Loginfrom) == "ADM/SPV")
				{
					return RedirectToAction("AdminLogon", "Login");
				}
				else if (HttpContext.Session.GetString(Loginfrom) == "MDO")
				{
					return RedirectToAction("OwnerLogon", "Login");
				}
				else
				{
					return RedirectToAction("BuyerLogon", "Login");
				}
				//return RedirectToAction("Logon", "Login");
			}
		}

		[HttpPost]
		public ActionResult AddProvince(int kode, string nama)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			AddProvinceInputModel data = new AddProvinceInputModel();
			data.UserID = Guid.Parse(userID);
			data.Kode = kode;
			data.Provinsi = nama;
			JsonConvert.SerializeObject(data);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Base/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<AddProvinceInputModel>("AddProvince", data);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("Index", "Province");
				}
				else
				{
					TempData["CustomError"] = "Fail to add data. Please contact administrator.";
				}
			}
			TempData["CustomError"] = "Server Error. Please contact administrator.";
			return RedirectToAction("Create", "Province");
		}

		public ActionResult Update(string id)
		{
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				HttpContext.Session.SetString(SessionKeyEdit, id);
				var ID = Guid.Parse(id);
				ProvinceOutputModel province = new ProvinceOutputModel();
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else
				{
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(BaseAPI + "Base/");
						//HTTP POST
						var postTask = client.GetAsync("GetProvinceDetail?ProvinceID=" + ID);
						postTask.Wait();

						var result = postTask.Result;
						if (result.IsSuccessStatusCode)
						{
							var content = result.Content.ReadAsStringAsync();
							ProvinceDetailResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ProvinceDetailResponseModel>(content.Result);
							province = resutl.data;
							province.ProvinceID = ID;
						}
						else
						{
							//log response status here..
							province = null;
							ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
						}
					}
				}
				return View(province);
			}
			else
			{
				TempData["CustomError"] = "Please login before using the web.";
				if (HttpContext.Session.GetString(Loginfrom) == "ADM/SPV")
				{
					return RedirectToAction("AdminLogon", "Login");
				}
				else if (HttpContext.Session.GetString(Loginfrom) == "MDO")
				{
					return RedirectToAction("OwnerLogon", "Login");
				}
				else
				{
					return RedirectToAction("BuyerLogon", "Login");
				}
				//return RedirectToAction("Logon", "Login");
			}
		}

		[HttpPost]
		public ActionResult UpdateProvince(string ProvinceID, int kode, string nama)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			EditProvinceInputModel dataProvince = new EditProvinceInputModel();
			dataProvince.UserID = Guid.Parse(userID);
			dataProvince.ProvinsiID = Guid.Parse(ProvinceID);
			dataProvince.Kode = kode;
			dataProvince.Provinsi = nama;
			JsonConvert.SerializeObject(dataProvince);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Base/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<EditProvinceInputModel>("EditProvince", dataProvince);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("Index", "Province");
				}
				else
				{
					TempData["CustomError"] = "Fail to update data. Please contact administrator.";
				}
			}
			TempData["CustomError"] = "Server Error. Please contact administrator.";
			return RedirectToAction("Update", "Province", new { @id = HttpContext.Session.GetString(SessionKeyEdit) });
		}

		[HttpPost]
		public ActionResult Delete(string id)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			DeleteProvinceInputModel dataProvince = new DeleteProvinceInputModel();
			dataProvince.ProvinsiID = Guid.Parse(id);
			dataProvince.UserID = Guid.Parse(userID);
			JsonConvert.SerializeObject(dataProvince);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Base/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<DeleteProvinceInputModel>("DeleteProvince", dataProvince);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("Index", "Province");
				}
				else
				{
					TempData["CustomError"] = "Fail to delete data. Please contact administrator.";
				}
			}
			TempData["CustomError"] = "Server Error. Please contact administrator.";
			return RedirectToAction("Index", "Province");
		}

	}
}
