using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Billboard360.Web.Models;
using System.Net.Http;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Billboard360.Web.Controllers
{
    public class DashboardController : Controller
    {
		const string SessionKeyID = "_ID";
		const string SessionKeyName = "_NAME";
		const string SessionKeyFName = "_FNAME";
		const string SessionKeyLName = "_LNAME";
		const string SessionKeyRole = "_ROLE";
		const string SessionKeyProv = "_PROV";
		const string SessionKeyBank = "_BANK";
		const string SessionKeyType = "_TYPE";
		const string SessionKeyCity = "_CITY";
		const string Loginfrom = "_LoginF";
		string BaseAPI = "";
		string Domain = "";
		private readonly IConfiguration _config;
        public DashboardController(IConfiguration config)
        {
            _config = config;
            BaseAPI = _config.GetValue<string>("APIBaseUrl");
			Domain = _config.GetValue<string>("Domain");
		}

		// GET: /<controller>/
		public IActionResult Index()
        {
			int PageSize = 1000;
			int PageNumber = 1;
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				CustomDashboard CustomOutPut = new CustomDashboard();
				CustomOutPut.KotakEmpat = new RekapKotakEmpatOutputModel();
				CustomOutPut.PurchaseAndBook = new List<ReportPurchaseAndBookOutputModel>();
				CustomOutPut.Chard = new ChardModel();
				CustomOutPut.SiteProblem = new List<ReportProblemListOutputModel>();

				ReportKotaEmpatInputModel filter1 = new ReportKotaEmpatInputModel();
				ReportPurchaseAndBookInputModel filter2 = new ReportPurchaseAndBookInputModel();
				ReportProblemListInputModel filter3 = new ReportProblemListInputModel();
				if (HttpContext.Session.GetString(SessionKeyRole) != "ADM" && HttpContext.Session.GetString(SessionKeyRole) != "SPV")
				{
					filter1.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
					filter2.UserName = HttpContext.Session.GetString(SessionKeyName);
				}
				filter2.PageSize = PageSize;
				filter2.PageNumber = PageNumber;
				filter3.isToSPV = true;
				filter3.PageSize = PageSize;
				filter3.PageNumber = PageNumber;
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Dashboard/");
					var responseTask = client.PostAsJsonAsync<ReportKotaEmpatInputModel>("ReportRekapBox", filter1);
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						RekapKotakEmpatResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<RekapKotakEmpatResponseModel>(content.Result);
						if (resutl.data != null)
						{
							CustomOutPut.KotakEmpat.TotalUseSite = resutl.data.TotalUseSite;
							CustomOutPut.KotakEmpat.TotalSite = resutl.data.TotalSite;
							CustomOutPut.KotakEmpat.Revenue = resutl.data.Revenue;
							CustomOutPut.KotakEmpat.TotalRevenue = resutl.data.TotalRevenue;
							CustomOutPut.KotakEmpat.TotalBooking = resutl.data.TotalBooking;
							CustomOutPut.KotakEmpat.TotalOrder = resutl.data.TotalOrder;
						}
					}
					else //web api sent error response 
					{
						//log response status here..
						ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
					}
				}
				using (var client1 = new HttpClient())
				{
					client1.BaseAddress = new Uri(BaseAPI + "Dashboard/");
					var responseTask1 = client1.PostAsJsonAsync<ReportPurchaseAndBookInputModel>("ReportPurchaseAndBook", filter2);
					responseTask1.Wait();

					var result1 = responseTask1.Result;
					if (result1.IsSuccessStatusCode)
					{
						var content = result1.Content.ReadAsStringAsync();
						ReportPurchaseAndModelResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportPurchaseAndModelResponseModel>(content.Result);
						CustomOutPut.PurchaseAndBook = resutl.data;
					}
					else //web api sent error response 
					{
						//log response status here..
						ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
					}
				}
				using (var client2 = new HttpClient())
				{
					client2.BaseAddress = new Uri(BaseAPI + "Dashboard/");
					var responseTask1 = client2.PostAsync("ReportChartBookPerMonth", null);
					responseTask1.Wait();

					var result1 = responseTask1.Result;
					if (result1.IsSuccessStatusCode)
					{
						var content = result1.Content.ReadAsStringAsync();
						ChardResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ChardResponseModel>(content.Result);
						CustomOutPut.Chard = resutl.data;
					}
					else //web api sent error response 
					{
						//log response status here..
						ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
					}
				}
				using (var client3 = new HttpClient())
				{
					client3.BaseAddress = new Uri(BaseAPI + "Admin/");
					var responseTask1 = client3.PostAsJsonAsync<ReportProblemListInputModel>("ViewReportProblemSiteList", filter3);
					responseTask1.Wait();

					var result1 = responseTask1.Result;
					if (result1.IsSuccessStatusCode)
					{
						var content = result1.Content.ReadAsStringAsync();
						ReportProblemListResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportProblemListResponseModel>(content.Result);
						CustomOutPut.SiteProblem = resutl.data;
					}
					else //web api sent error response 
					{
						//log response status here..
						ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
					}
				}

				return View(CustomOutPut);
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

		public ActionResult Update(string ID)
		{
			ApprovalBookOutputModel OutPutData = new ApprovalBookOutputModel();
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				string userID = HttpContext.Session.GetString(SessionKeyID);
				ApprovalBookInputModel data = new ApprovalBookInputModel();
				data.Value = int.Parse(ID.Split("&")[0]);
				data.UserID = Guid.Parse(userID);
				data.BookDetailID = Guid.Parse(ID.Split("&")[1]);
				JsonConvert.SerializeObject(data);
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Site/");
					var postTask = client.PostAsJsonAsync<ApprovalBookInputModel>("ApprovalBooked", data);
					postTask.Wait();

					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						ApprovalBookResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ApprovalBookResponseModel>(content.Result);
						OutPutData = resutl.data;
					}
					else //web api sent error response 
					{
						//log response status here..
						ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
					}
				}
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
			}
			return Json(OutPutData);
		}

		#region Dropdown
		public IActionResult GetBank()
		{
			IEnumerable<InfoBankDetailModel> banks = null;

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Bank/");
				HttpContext.Session.SetString(SessionKeyBank, client.BaseAddress.ToString());
				FilterBank filter = new FilterBank();
				filter.BankName = "";
				filter.BankAccountName = "";
				filter.UserName = "";
				filter.PageNumber = 1;
				filter.PageSize = 1000;
				var responseTask = client.PostAsJsonAsync<FilterBank>("GetListBank", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					ListBankResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ListBankResponseModel>(content.Result);
					banks = resutl.data;
				}
				else //web api sent error response 
				{
					//log response status here..
					banks = Enumerable.Empty<InfoBankDetailModel>();
					ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
				}
			}
			return Json(banks);
		}
		public IActionResult GetType()
		{
			List<BillboardTypeListOutputModel> billboardtype = new List<BillboardTypeListOutputModel>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Base/");
				HttpContext.Session.SetString(SessionKeyType, client.BaseAddress.ToString());
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
			return Json(billboardtype);
		}
		public IActionResult GetProvince()
		{
			List<ProvinceOutputModel> province = new List<ProvinceOutputModel>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Base/");
				HttpContext.Session.SetString(SessionKeyCity, client.BaseAddress.ToString());
				var provTask = client.PostAsync("GetProvince", null);
				provTask.Wait();
				var results = provTask.Result;
				if (results.IsSuccessStatusCode)
				{
					var content = results.Content.ReadAsStringAsync();
					ProvinceResponseModel provinceResponse = JsonConvert.DeserializeObject<ProvinceResponseModel>(content.Result);
					province = provinceResponse.data.OrderBy(x => x.Provinsi).ToList();
				}
				else
				{
					province = null;
					ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
				}
			}
			return Json(province);
		}
		public IActionResult GetCity(int kodeprov)
		{
			List<CityOutputModel> city = new List<CityOutputModel>();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Base/");
				CityInputModel param = new CityInputModel();
				param.KodeProv = kodeprov;
				JsonConvert.SerializeObject(param);
				var cityTask = client.PostAsJsonAsync<CityInputModel>("GetCity", param);
				cityTask.Wait();
				var results = cityTask.Result;
				if (results.IsSuccessStatusCode)
				{
					var content = results.Content.ReadAsStringAsync();
					CityResponseModel provinceResponse = JsonConvert.DeserializeObject<CityResponseModel>(content.Result);
					city = provinceResponse.data.OrderBy(x => x.Kota).ToList();
				}
				else
				{
					city = null;
					ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
				}
			}
			return Json(city);
		}
		#endregion
	}
}
