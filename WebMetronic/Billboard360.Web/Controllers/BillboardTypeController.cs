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
	public class BillboardTypeController : Controller
	{
		const string SessionKeyID = "_ID";
		const string SessionKeyName = "_NAME";
		const string SessionKeyRole = "_ROLE";
		const string Loginfrom = "_LoginF";
		string BaseAPI = "";
		string Domain = "";
		private readonly IConfiguration _config;
		public BillboardTypeController(IConfiguration config)
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
				List<BillboardTypeListOutputModel> billboardtype = new List<BillboardTypeListOutputModel>();
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else
				{
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(BaseAPI + "Base/");
						var typeTank = client.GetAsync("GetBillboardType");
						typeTank.Wait();
						var results = typeTank.Result;
						if (results.IsSuccessStatusCode)
						{
							var content = results.Content.ReadAsStringAsync();
							BillboardTypeListResponseModel billboardtypeResponse = JsonConvert.DeserializeObject<BillboardTypeListResponseModel>(content.Result);
							billboardtype = billboardtypeResponse.data.OrderBy(x => x.Kode).ToList();
						}
						else
						{
							billboardtype = null;
							ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
						}
					}
				}
				return View(billboardtype);
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

		[HttpPost]
		public ActionResult Addbillboardtype(string kode, string nama)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			AddBillboardTypeInputModel data = new AddBillboardTypeInputModel();
			data.Kode = kode;
			data.Type = nama;
			JsonConvert.SerializeObject(data);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Base/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<AddBillboardTypeInputModel>("AddBillboardType", data);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("Index", "BillboardType");
				}
				else
				{
					TempData["CustomError"] = "Gagal menambah data. Mohon hubungi admin.";
				}
			}
			TempData["CustomError"] = "Terjadi kesalahan. Mohon hubungi admin.";
			return RedirectToAction("Create", "BillboardType");
		}

	}
}
