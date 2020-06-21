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
	public class CityController : Controller
	{
		const string SessionKeyID = "_ID";
		const string SessionKeyName = "_NAME";
		const string SessionKeyRole = "_ROLE";
		const string SessionKeyEdit = "_EDIT";
		const string Loginfrom = "_LoginF";
		string BaseAPI = "";
		string Domain = "";
		private readonly IConfiguration _config;
		public CityController(IConfiguration config)
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
				List<CityOutputModel> City = new List<CityOutputModel>();
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else
				{
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(BaseAPI + "Base/");
						var provTask = client.PostAsync("GetAllCity", null);
						provTask.Wait();
						var results = provTask.Result;
						if (results.IsSuccessStatusCode)
						{
							var content = results.Content.ReadAsStringAsync();
							CityResponseModel CityResponse = JsonConvert.DeserializeObject<CityResponseModel>(content.Result);
							City = CityResponse.data.OrderBy(x => x.Kode).ToList();
						}
						else
						{
							City = null;
							ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
						}
					}
				}
				return View(City);
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
		public ActionResult AddCity(string kode, string nama, string lstprovince)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			AddCityInputModel data = new AddCityInputModel();
			data.UserID = Guid.Parse(userID);
			data.Kode = kode;
			data.CityName = nama;
			data.KodeProvinsi = lstprovince.Split("|")[0].ToString();
			JsonConvert.SerializeObject(data);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Base/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<AddCityInputModel>("AddCity", data);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("Index", "City");
				}
				else
				{
					TempData["CustomError"] = "Fail to add data. Please contact administrator.";
				}
			}
			TempData["CustomError"] = "Server Error. Please contact administrator.";
			return RedirectToAction("Create", "City");
		}

		public ActionResult Update(string id)
		{
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				HttpContext.Session.SetString(SessionKeyEdit, id);
				var ID = Guid.Parse(id);
				CityOutputModel City = new CityOutputModel();
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
						var postTask = client.GetAsync("GetCityDetail?CityID=" + ID);
						postTask.Wait();

						var result = postTask.Result;
						if (result.IsSuccessStatusCode)
						{
							var content = result.Content.ReadAsStringAsync();
							CityDetailResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<CityDetailResponseModel>(content.Result);
							City = resutl.data;
						}
						else
						{
							//log response status here..
							City = null;
							TempData["CustomError"] = "Fail to get data. Please contact administrator.";
						}
					}
				}
				return View(City);
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
		public ActionResult UpdateCity(string CityID, string kode, string nama, string lstprovince)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			EditCityInputModel dataCity = new EditCityInputModel();
			dataCity.UserID = Guid.Parse(userID);
			dataCity.CityID = Guid.Parse(CityID);
			dataCity.Kode = kode;
			dataCity.CityName = nama;
			dataCity.KodeProvinsi = lstprovince.Split("|")[0].ToString();
			JsonConvert.SerializeObject(dataCity);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Base/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<EditCityInputModel>("EditCity", dataCity);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("Index", "City");
				}
				else
				{
					TempData["CustomError"] = "Fail to update data. Please contact administrator.";
				}
			}
			TempData["CustomError"] = "Server Error. Please contact administrator.";
			return RedirectToAction("Update", "City", new { @id = HttpContext.Session.GetString(SessionKeyEdit) });
		}

		[HttpPost]
		public ActionResult Delete(string id)
		{
			//string userID = HttpContext.Session.GetString(SessionKeyID);
			//DeleteCityInputModel dataCity = new DeleteCityInputModel();
			//dataCity.ProvinsiID = Guid.Parse(id);
			//dataCity.UserID = Guid.Parse(userID);
			//JsonConvert.SerializeObject(dataCity);
			//using (var client = new HttpClient())
			//{
			//	client.BaseAddress = new Uri(BaseAPI + "Base/");
			//	//HTTP POST
			//	var postTask = client.PostAsJsonAsync<DeleteCityInputModel>("DeleteCity", dataCity);
			//	postTask.Wait();

			//	var result = postTask.Result;
			//	if (result.IsSuccessStatusCode)
			//	{
			//		return RedirectToAction("Index", "City");
			//	}
			//}

			ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

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
}
