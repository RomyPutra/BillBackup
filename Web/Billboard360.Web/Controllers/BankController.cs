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
    public class BankController : Controller
    {
        const string SessionKeyID = "_ID";
        const string SessionKeyName = "_NAME";
		const string SessionKeyRole = "_ROLE";
		const string Loginfrom = "_LoginF";
		string BaseAPI = "";
		string Domain = "";
		private readonly IConfiguration _config;
        public BankController(IConfiguration config)
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
				IEnumerable<InfoBankDetailModel> banks = null;
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else
				{
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(BaseAPI + "Bank/");
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

							ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
						}
					}
				}
				return View(banks);
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
        public ActionResult AddBank(string kode, string nama, string logobank)
        {
            string userID = HttpContext.Session.GetString(SessionKeyID);
            AddBankInputModel dataBank = new AddBankInputModel();
            dataBank.Kode = kode;
            dataBank.Nama = nama;
            dataBank.LogoBank = logobank;
            dataBank.IsActive = true;
            dataBank.CreateByUserID = Guid.Parse(userID);
            JsonConvert.SerializeObject(dataBank);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAPI + "Bank/");
                //HTTP POST
                var postTask = client.PostAsJsonAsync<AddBankInputModel>("AddMasterBank", dataBank);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Bank");
                }
				else
				{
					TempData["CustomError"] = "Fail to add data. Please contact administrator.";
				}
			}
			TempData["CustomError"] = "Server Error. Please contact administrator.";
			return RedirectToAction("Create", "Bank");
        }

        public ActionResult Update(string id)
        {
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				var ID = Guid.Parse(id);
				InfoBankDetailModel bank = new InfoBankDetailModel();
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else
				{
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(BaseAPI + "Bank/");
						//HTTP POST
						var postTask = client.GetAsync("GetBank?BankID=" + ID);
						postTask.Wait();

						var result = postTask.Result;
						if (result.IsSuccessStatusCode)
						{
							var content = result.Content.ReadAsStringAsync();
							InfoBankResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<InfoBankResponseModel>(content.Result);
							bank = resutl.data;
						}
						else //web api sent error response 
						{
							//log response status here..
							bank = null;
							TempData["CustomError"] = "Server Error. Please contact administrator.";
						}
					}
				}
				return View(bank);
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
        public ActionResult UpdateBank(string bankID, string kode, string nama, string logobank)
        {
            string userID = HttpContext.Session.GetString(SessionKeyID);
            EditBankInputModel dataBank = new EditBankInputModel();
            dataBank.BankID = Guid.Parse(bankID);
            dataBank.Kode = kode;
            dataBank.Nama = nama;
            dataBank.LogoBank = logobank;
            dataBank.IsActive = true;
            dataBank.LastUpdateByUserID = Guid.Parse(userID);
            JsonConvert.SerializeObject(dataBank);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAPI + "Bank/");
                //HTTP POST
                var postTask = client.PostAsJsonAsync<EditBankInputModel>("EditMasterBank", dataBank);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Bank");
                }
				else
				{
					TempData["CustomError"] = "Fail to update data. Please contact administrator.";
				}
			}
			TempData["CustomError"] = "Server Error. Please contact administrator.";
			return RedirectToAction("Update", "Bank", new { @id = bankID });
        }

		[HttpPost]
		public ActionResult Delete(string id)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			DeleteBankInputModel dataBank = new DeleteBankInputModel();
			dataBank.BankID = Guid.Parse(id);
			dataBank.UserID = Guid.Parse(userID);
			JsonConvert.SerializeObject(dataBank);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Bank/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<DeleteBankInputModel>("DeleteBank", dataBank);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("Index", "Bank");
				}
				else
				{
					TempData["CustomError"] = "Fail to delete data. Please contact administrator.";
				}
			}
			TempData["CustomError"] = "Server Error. Please contact administrator.";
			return RedirectToAction("Index", "Bank");
		}

	}
}
