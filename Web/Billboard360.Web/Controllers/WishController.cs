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
	public class WishController : Controller
	{
		const string SessionKeyID = "_ID";
		const string SessionKeyName = "_NAME";
		const string SessionKeyFName = "_FNAME";
		const string SessionKeyLName = "_LNAME";
		const string Loginfrom = "_LoginF";
		string BaseAPI = "";
		string Domain = "";
		private readonly IConfiguration _config;
		public WishController(IConfiguration config)
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
				IEnumerable<WishListOutputModel> OutPutData = null;
				WishListInputModel filter = new WishListInputModel();
				filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
				filter.PageSize = 1000;
				filter.PageNumber = 1;
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Site/");
					var responseTask = client.PostAsJsonAsync<WishListInputModel>("GetWishList", filter);
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						WishListResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<WishListResponseModel>(content.Result);
						OutPutData = resutl.data;
					}
					else //web api sent error response 
					{
						//log response status here..
						OutPutData = Enumerable.Empty<WishListOutputModel>();
						ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
					}
				}
				return View(OutPutData);
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

		public IActionResult AddToWishList(string site, string item)
		{
			AddToWishListOutputModel OutPutData = new AddToWishListOutputModel();
			AddToWishListInputModel filter = new AddToWishListInputModel();
			filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			filter.SiteID = Guid.Parse(site);
			filter.SiteItemID = Guid.Parse(item);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				var responseTask = client.PostAsJsonAsync<AddToWishListInputModel>("AddToWishList", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					AddToWishListResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<AddToWishListResponseModel>(content.Result);
					OutPutData = resutl.data;
				}
				else //web api sent error response 
				{
					//log response status here..
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return Json(OutPutData);
		}

		public IActionResult DeleteFromWishList(string ID)
		{
			DeleteFromWishListOutputModel OutPutData = new DeleteFromWishListOutputModel();
			DeleteFromWishListInputModel filter = new DeleteFromWishListInputModel();
			filter.WishListID = Guid.Parse(ID);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				var responseTask = client.PostAsJsonAsync<DeleteFromWishListInputModel>("DeleteFromWishList", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					DeleteFromWishListResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<DeleteFromWishListResponseModel>(content.Result);
					OutPutData = resutl.data;
					return RedirectToAction("Index", "Wish");
				}
				else //web api sent error response 
				{
					//log response status here..
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return RedirectToAction("Index", "Home");
		}

		public IActionResult Keranjang()
		{
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				List<ViewResultCartModel> OutPutData = new List<ViewResultCartModel>();
				ViewCartInputModel filter = new ViewCartInputModel();
				filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Site/");
					var responseTask = client.PostAsJsonAsync<ViewCartInputModel>("ViewCart", filter);
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						ViewArrayCartResponse resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewArrayCartResponse>(content.Result);
						OutPutData = resutl.Data;
					}
					else //web api sent error response 
					{
						//log response status here..
						ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
					}
				}
				return View(OutPutData);
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

		public IActionResult DeleteFromKeranjang(string ID)
		{
			List<Guid> BookIDs = new List<Guid>();
			BookIDs.Add(Guid.Parse(ID));
			DeleteBookOutputModel OutPutData = new DeleteBookOutputModel();
			DeleteBookInputModel filter = new DeleteBookInputModel();
			filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			filter.BookID = BookIDs;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				var responseTask = client.PostAsJsonAsync<DeleteBookInputModel>("DeleteFromCart", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					DeleteBookResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<DeleteBookResponseModel>(content.Result);
					OutPutData = resutl.data;
					return RedirectToAction("Keranjang", "Wish");
				}
				else //web api sent error response 
				{
					//log response status here..
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return RedirectToAction("Index", "Home");
		}

	}
}
