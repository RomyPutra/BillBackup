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
    public class BankAccountController : Controller
    {
        const string SessionKeyID = "_ID";
        const string SessionKeyName = "_NAME";
		const string SessionKeyRole = "_ROLE";
		const string SessionKeyEdit = "_EDIT";
		const string Loginfrom = "_LoginF";
		string BaseAPI = "";
		string Domain = "";
        private readonly IConfiguration _config;
        public BankAccountController(IConfiguration config)
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
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
					return View();
				}
				else
				{
					IEnumerable<GetUserBankOutputModel> accounts = null;
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(BaseAPI + "Profile/");
						GetUserBankInputModel filter = new GetUserBankInputModel();
						filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID).ToString());
						var responseTask = client.PostAsJsonAsync<GetUserBankInputModel>("GetUserBankAccount", filter);
						responseTask.Wait();

						var result = responseTask.Result;
						if (result.IsSuccessStatusCode)
						{
							var content = result.Content.ReadAsStringAsync();
							GetUserBankResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<GetUserBankResponseModel>(content.Result);
							accounts = resutl.data;
						}
						else //web api sent error response 
						{
							//log response status here..
							accounts = Enumerable.Empty<GetUserBankOutputModel>();
							ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
						}
					}
					return View(accounts);
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
        public ActionResult AddBankAccount(AddBankAccountInputModel data)
        {
            string userID = HttpContext.Session.GetString(SessionKeyID);
            AddBankAccountInputModel dataBankAccount = new AddBankAccountInputModel();
            dataBankAccount.AccountNumber = data.AccountNumber;
            dataBankAccount.AccountName = data.AccountName;
            dataBankAccount.BankID = data.BankID;
            dataBankAccount.isSelected = true;
            dataBankAccount.UserID = Guid.Parse(userID);
            JsonConvert.SerializeObject(dataBankAccount);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAPI + "Profile/");
                //HTTP POST
                var postTask = client.PostAsJsonAsync<AddBankAccountInputModel>("AddBankAccount", dataBankAccount);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "BankAccount");
                }
				else
				{
					TempData["CustomError"] = "Fail to add bank account. Please contact administrator.";
				}
			}
			TempData["CustomError"] = "Terjadi kesalahan. Mohon hubungi admin.";
			return RedirectToAction("Create", "BankAccount");
        }

        public ActionResult Update(string id)
        {
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				HttpContext.Session.SetString(SessionKeyEdit, id);
				var ID = Guid.Parse(id);
				IEnumerable<GetUserBankOutputModel> accounts = null;
				EditBankAccount data = new EditBankAccount();
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else
				{
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(BaseAPI + "Profile/");
						GetUserBankInputModel filter = new GetUserBankInputModel();
						filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID).ToString());
						var responseTask = client.PostAsJsonAsync<GetUserBankInputModel>("GetUserBankAccount", filter);
						responseTask.Wait();

						var result = responseTask.Result;
						if (result.IsSuccessStatusCode)
						{
							var content = result.Content.ReadAsStringAsync();
							GetUserBankResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<GetUserBankResponseModel>(content.Result);
							accounts = resutl.data;
							data.item = accounts.Where(x => x.UserBankID == ID).First();
						}
						else //web api sent error response 
						{
							ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
						}
					}
				}
				return View(data);
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
        public ActionResult UpdateBank(EditBankAccount data)
        {
            string userID = HttpContext.Session.GetString(SessionKeyID);
            EditBankAccountInputModel dataBankAccount = new EditBankAccountInputModel();
            dataBankAccount.ID = data.input.ID;
            dataBankAccount.BankID = data.input.BankID;
            dataBankAccount.AccountNumber = data.input.AccountNumber;
            dataBankAccount.AccountName = data.input.AccountName;
            dataBankAccount.isSelected = true;
            dataBankAccount.UserID = Guid.Parse(userID);
            JsonConvert.SerializeObject(dataBankAccount);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAPI + "Profile/");
                //HTTP POST
                var postTask = client.PostAsJsonAsync<EditBankAccountInputModel>("EditBankAccount", dataBankAccount);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "BankAccount");
                }
				else
				{
					TempData["CustomError"] = "Gagal memperbarui data. Mohon hubungi admin.";
				}
			}
			TempData["CustomError"] = "Terjadi kesalahan. Mohon hubungi admin.";
			return RedirectToAction("Update", "BankAccount", new { @id = HttpContext.Session.GetString(SessionKeyEdit) });
        }

		[HttpPost]
		public ActionResult Delete(string id)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			DeleteBankAccountInputModel dataBank = new DeleteBankAccountInputModel();
			dataBank.UserBankID = Guid.Parse(id);
			dataBank.UserID = Guid.Parse(userID);
			JsonConvert.SerializeObject(dataBank);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Profile/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<DeleteBankAccountInputModel>("DeleteBankAccount", dataBank);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("Index", "BankAccount");
				}
			}
			TempData["CustomError"] = "Terjadi kesalahan. Mohon hubungi admin.";
			return RedirectToAction("Index", "BankAccount");
		}

	}
}
