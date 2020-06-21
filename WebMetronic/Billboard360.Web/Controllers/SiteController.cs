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
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.AspNetCore.Routing;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Billboard360.Web.Controllers
{
	public class SiteController : Controller
	{
		const string SessionKeyID = "_ID";
		const string SessionKeyName = "_NAME";
		const string SessionKeyRole = "_ROLE";
		const string SessionKeySite = "_SITE";
		const string SessionKeyEdit = "_EDIT";
		const string SessionKeyPrice = "_PRICE";
		const string SessionKeyProv = "_PROV";
		const string SessionKeyDomain = "_Domain";
		const string Loginfrom = "_LoginF";
		string BaseAPI = "";
		string Domain = "";
		private readonly IConfiguration _config;
		public SiteController(IConfiguration config)
		{
			_config = config;
			BaseAPI = _config.GetValue<string>("APIBaseUrl");
			Domain = _config.GetValue<string>("Domain");
		}

		#region Detail
		public IActionResult Index()
		{
			HttpContext.Session.SetString(SessionKeySite, "");
			HttpContext.Session.SetString(SessionKeyEdit, "");
			HttpContext.Session.SetString(SessionKeyPrice, "");
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				List<SiteListOutputModel> sites = new List<SiteListOutputModel>();
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else
				{
					//using (var client = new HttpClient())
					//{
					//	client.BaseAddress = new Uri(BaseAPI + "Site/");
					//	FilterBillboard filter = new FilterBillboard();
					//	if (HttpContext.Session.GetString(SessionKeyRole) == "ADM")
					//	{
					//		filter.UserID = Guid.Empty;
					//	}
					//	else
					//	{
					//		filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
					//	}
					//	filter.City = "";
					//	filter.Province = "";
					//	filter.Latitude = "";
					//	filter.Longitude = "";
					//	filter.TypeBillboard = "";
					//	filter.PageNumber = 1;
					//	filter.PageSize = 1;
					//	var responseTask = client.PostAsJsonAsync<FilterBillboard>("GetListSite", filter);
					//	responseTask.Wait();

					//	var result = responseTask.Result;
					//	if (result.IsSuccessStatusCode)
					//	{
					//		var content = result.Content.ReadAsStringAsync();
					//		SiteListResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteListResponseModel>(content.Result);

					//		filter.PageSize = 1000;// resutl.TotalPages;
					//		var reresponseTask = client.PostAsJsonAsync<FilterBillboard>("GetListSite", filter);
					//		reresponseTask.Wait();

					//		var reresult = reresponseTask.Result;
					//		if (reresult.IsSuccessStatusCode)
					//		{
					//			var recontent = reresult.Content.ReadAsStringAsync();
					//			SiteListResponseModel reresutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteListResponseModel>(recontent.Result);
					//			sites = reresutl.data;
					//		}
					//	}
					//	else //web api sent error response 
					//	{
					//		ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
					//	}
					//}
				}
				return View(sites);
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

		public ActionResult lstSite(JsonModels param)
		{
			HttpContext.Session.SetString(SessionKeySite, "");
			HttpContext.Session.SetString(SessionKeyEdit, "");
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				int Draw = 10;
				int totalResultsCount = 0;
				int filteredResultsCount = 0;
				JsonModel jsonResut = new JsonModel();
				List<SiteListOutputModel> sites = new List<SiteListOutputModel>();
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else
				{
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(BaseAPI + "Site/");
						FilterBillboard filter = new FilterBillboard();
						if (HttpContext.Session.GetString(SessionKeyRole) == "ADM")
						{
							filter.UserID = Guid.Empty;
						}
						else
						{
							filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
						}

						if (param.sSearch != null && param.sSearch != "")
						{
							filter.isFilterDataTable = true;
							filter.FilterDataTableValue = param.sSearch;
						} 
						else
						{
							filter.Latitude = "";
							filter.Longitude = "";
						}
						filter.showWithDisabledSite = true;
						filter.PageSize = param.iDisplayLength;
						filter.PageNumber = (param.iDisplayLength+param.iDisplayStart)/param.iDisplayLength;
						var responseTask = client.PostAsJsonAsync<FilterBillboard>("GetListSite", filter);
						responseTask.Wait();

						var result = responseTask.Result;
						if (result.IsSuccessStatusCode)
						{
							var content = result.Content.ReadAsStringAsync();
							SiteListResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteListResponseModel>(content.Result);
							sites = resutl.data;
							foreach(SiteListOutputModel data in sites)
							{
								data.Status = data.Status == "Active" ? "Disable" : "Enable";
							}
							totalResultsCount = resutl.TotalData;
							filteredResultsCount = resutl.TotalData;

							//filter.PageSize = 10;// resutl.TotalPages;
							//var reresponseTask = client.PostAsJsonAsync<FilterBillboard>("GetListSite", filter);
							//reresponseTask.Wait();

							//var reresult = reresponseTask.Result;
							//if (reresult.IsSuccessStatusCode)
							//{
							//	var recontent = reresult.Content.ReadAsStringAsync();
							//	SiteListResponseModel reresutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteListResponseModel>(recontent.Result);
							//	sites = reresutl.data;
							//	totalResultsCount = reresutl.TotalData;
							//	filteredResultsCount = reresutl.TotalData;
							//}
						}
						else //web api sent error response 
						{
							ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
						}
					}
				}
				return Json(new
				{
					param.sEcho,
					iTotalRecords = totalResultsCount,
					iTotalDisplayRecords = filteredResultsCount,
					aaData = sites
				});
				//return Json(new
				//{
				//	draw = Draw,
				//	recordsTotal = totalResultsCount,
				//	recordsFiltered = filteredResultsCount,
				//	data = sites
				//});
				//return Json(sites);
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
		public ActionResult UpdateSiteStatus(string siteID, string Mode)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			ChangeStatusInputModel dataSite = new ChangeStatusInputModel();
			dataSite.UserID = Guid.Parse(userID);
			dataSite.SiteID = Guid.Parse(siteID);
			dataSite.Value = Mode == "Disable" ? 0 : 1;
			JsonConvert.SerializeObject(dataSite);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<ChangeStatusInputModel>("ChangeStatusSite", dataSite);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("Index", "Site");
				}
				else
				{
					TempData["CustomError"] = "Gagal memperbarui data. Mohon hubungi admin.";
				}
			}
			TempData["CustomError"] = "Terjadi kesalahan. Mohon hubungi admin.";
			return RedirectToAction("Index", "Site");
		}

		[HttpPost]
		public ActionResult AddSite(AddSiteResultDetailInputModel Data, string lstprovince, string lstcity, string link)
		{
			string province = "";
			string harga = "0";
			string userID = HttpContext.Session.GetString(SessionKeyID);
			//Regex Reg = new Regex("@(.*),(.*),");
			//var links = Reg.Matches(link);
			var links = new string[2];
			if (link.Split("@").Length > 1 && link.Split("@")[1].Split("z").Length > 1 && link.Split("@")[1].Split("z")[0].Split(",").Length > 1)
			{
				links = link.Split("@")[1].Split("z")[0].Split(",");
			}
			else
			{
				TempData["CustomError"] = "Silakan gunakan tautan maps.google.com.";
				return RedirectToAction("Create", "Site");
			}
			AddSiteResultDetailInputModel dataSite = new AddSiteResultDetailInputModel();
			AddSiteDetailInputModel Detail = new AddSiteDetailInputModel();
			dataSite.UserID = Guid.Parse(userID);
			Detail.NoBillboard = Data.Detail.NoBillboard;
			Detail.Address = Data.Detail.Address;
			Detail.Pulau = Data.Detail.Pulau;
			Detail.Kota = Data.Detail.Kota;
			Detail.Provinsi = Data.Detail.Provinsi.Split("|")[1].ToString();
			Detail.Cabang = Data.Detail.Cabang;
			Detail.HorV = Data.Detail.HorV;
			Detail.Tipe = Data.Detail.Tipe;
			if (links != null)
			{
				Detail.Latitude = links[0].ToString();
				Detail.Longitude = links[1].ToString();
				//Detail.Latitude = double.Parse(links[0].ToString());
				//Detail.Longitude = double.Parse(links[1].ToString());
			}
			//if (Data.Detail.Harga.ToString().Length > 2)
			//{
			//	harga = Data.Detail.Harga.ToString().Replace(",", "");
			//}
			//Detail.Harga = double.Parse(harga);
			dataSite.Detail = Detail;
			JsonConvert.SerializeObject(dataSite);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<AddSiteResultDetailInputModel>("AddSiteDetail", dataSite);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("Index", "Site");
				}
				else
				{
					TempData["CustomError"] = "Gagal menambah data. Mohon hubungi admin.";
				}
			}
			TempData["CustomError"] = "Terjadi kesalahan. Mohon hubungi admin.";
			return RedirectToAction("Create", "Site");
		}

		public ActionResult Update(string id)
		{
			HttpContext.Session.SetString(SessionKeyEdit, id);
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				var ID = Guid.Parse(id);
				EditSite site = new EditSite();
				SelectListItem prov = new SelectListItem();
				List<SelectListItem> provs = new List<SelectListItem>();
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else
				{
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(BaseAPI + "Site/");
						//HTTP POST
						SiteDetailInputModel filter = new SiteDetailInputModel();
						filter.SiteID = ID;
						var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
						postTask.Wait();

						var result = postTask.Result;
						if (result.IsSuccessStatusCode)
						{
							var content = result.Content.ReadAsStringAsync();
							SiteDetailResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);
							site.val = resutl.data;
							List<ProvinceOutputModel> provlist = new List<ProvinceOutputModel>();
							if (HttpContext.Session.GetObjectFromJson<List<ProvinceOutputModel>>(SessionKeyProv) != null)
							{
								provlist = HttpContext.Session.GetObjectFromJson<List<ProvinceOutputModel>>(SessionKeyProv);
								site.Options = provlist.Select(x => new SelectListItem()
								{
									Text = x.Provinsi,
									Value = x.Kode + "|" + x.Provinsi
								}).ToList();
							}
						}
						else //web api sent error response 
						{
							//log response status here..
							site.val = null;
							ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
						}
					}
				}
				return View(site);
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
		public ActionResult UpdateSite(EditSite Data)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			EditSiteResultInputModel dataSite = new EditSiteResultInputModel();
			dataSite.UserID = Guid.Parse(userID);
			Data.input.Provinsi = Data.input.Provinsi.Split("|")[1].ToString();
			dataSite.Detail = Data.input;
			JsonConvert.SerializeObject(dataSite);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<EditSiteResultInputModel>("UpdateSite", dataSite);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("Index", "Site");
				}
				else
				{
					TempData["CustomError"] = "Gagal memperbarui data. Mohon hubungi admin.";
				}
			}
			TempData["CustomError"] = "Terjadi kesalahan. Mohon hubungi admin.";
			return RedirectToAction("Update", "Site", new { @id = HttpContext.Session.GetString(SessionKeyEdit) });
		}

		[HttpPost]
		public ActionResult Delete(string id)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			DeleteSiteInputModel dataSite = new DeleteSiteInputModel();
			dataSite.SiteID = Guid.Parse(id);
			dataSite.UserID = Guid.Parse(userID);
			JsonConvert.SerializeObject(dataSite);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<DeleteSiteInputModel>("DeleteSite", dataSite);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("Index", "Site");
				}
				else
				{
					TempData["CustomError"] = "Gagal menghapus data. Mohon hubungi admin.";
				}
			}
			TempData["CustomError"] = "Terjadi kesalahan. Mohon hubungi admin.";
			return RedirectToAction("Index", "Site");
		}
		#endregion

		#region OriItem
		// public IActionResult IndexItem(string id)
		// {
		// HttpContext.Session.SetString(SessionKeySite, id);
		// if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
		// {
		// var ID = Guid.Parse(id);
		// EditSite site = new EditSite();
		// SelectListItem prov = new SelectListItem();
		// List<SelectListItem> provs = new List<SelectListItem>();
		// List<SiteDetailItemModel> Items = new List<SiteDetailItemModel>();
		// if (TempData["CustomError"] != null)
		// {
		// ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
		// }
		// else
		// {
		// using (var client = new HttpClient())
		// {
		// client.BaseAddress = new Uri(BaseAPI + "Site/");
		// //HTTP POST
		// SiteDetailInputModel filter = new SiteDetailInputModel();
		// filter.SiteID = ID;
		// var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
		// postTask.Wait();

		// var result = postTask.Result;
		// if (result.IsSuccessStatusCode)
		// {
		// var content = result.Content.ReadAsStringAsync();
		// SiteDetailResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);
		// site.val = resutl.data;
		// if (site.val != null && site.val.Item != null && site.val.Item.Count > 0)
		// {
		// Items = site.val.Item.Where(x => x.SiteID == ID).ToList();
		// }
		// }
		// else //web api sent error response 
		// {
		// //log response status here..
		// ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
		// }
		// }
		// }
		// return View(Items);
		// }
		// else
		// {
		// TempData["CustomError"] = "Silakan masuk sebelum menggunakan situs web.";
		// if (HttpContext.Session.GetString(Loginfrom) == "ADM/SPV")
		// {
		// return RedirectToAction("AdminLogon", "Login");
		// }
		// else if (HttpContext.Session.GetString(Loginfrom) == "MDO")
		// {
		// return RedirectToAction("OwnerLogon", "Login");
		// }
		// else
		// {
		// return RedirectToAction("OwnerLogon", "Login");
		// }
		// //return RedirectToAction("Logon", "Login");
		// }
		// }

		// public IActionResult CreateItem()
		// {
		// if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
		// {
		// if (TempData["CustomError"] != null)
		// {
		// ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
		// }
		// return View();
		// }
		// else
		// {
		// TempData["CustomError"] = "Silakan masuk sebelum menggunakan situs web.";
		// if (HttpContext.Session.GetString(Loginfrom) == "ADM/SPV")
		// {
		// return RedirectToAction("AdminLogon", "Login");
		// }
		// else if (HttpContext.Session.GetString(Loginfrom) == "MDO")
		// {
		// return RedirectToAction("OwnerLogon", "Login");
		// }
		// else
		// {
		// return RedirectToAction("OwnerLogon", "Login");
		// }
		// //return RedirectToAction("Logon", "Login");
		// }
		// }

		// [HttpPost]
		// public ActionResult AddSiteItem(AddSiteItemDetailInputModel Data)
		// {
		// string userID = HttpContext.Session.GetString(SessionKeyID);
		// AddSiteItemDetailInputModel dataSite = new AddSiteItemDetailInputModel();
		// dataSite.SiteID = Guid.Parse(HttpContext.Session.GetString(SessionKeySite));
		// dataSite.KodeArahLokasi = Data.KodeArahLokasi;
		// dataSite.ArahLokasi = Data.ArahLokasi;
		// dataSite.Panjang = Data.Panjang;
		// dataSite.Lebar = Data.Lebar;
		// JsonConvert.SerializeObject(dataSite);
		// using (var client = new HttpClient())
		// {
		// client.BaseAddress = new Uri(BaseAPI + "Site/");
		// //HTTP POST
		// var postTask = client.PostAsJsonAsync<AddSiteItemDetailInputModel>("AddSiteItemDetail", dataSite);
		// postTask.Wait();

		// var result = postTask.Result;
		// if (result.IsSuccessStatusCode)
		// {
		// return RedirectToAction("IndexItem", new RouteValueDictionary(
		// new {
		// controller = "Site", action = "IndexItem", Id = HttpContext.Session.GetString(SessionKeySite)
		// }
		// ));
		// }
		// else
		// {
		// TempData["CustomError"] = "Gagal menambah data. Mohon hubungi admin.";
		// }
		// }
		// TempData["CustomError"] = "Terjadi kesalahan. Mohon hubungi admin.";
		// return RedirectToAction("CreateItem", "Site");
		// }

		// public ActionResult UpdateItem(string id)
		// {
		// HttpContext.Session.SetString(SessionKeyEdit, id);
		// if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
		// {
		// var ID = Guid.Parse(HttpContext.Session.GetString(SessionKeySite));
		// EditItem item = new EditItem();
		// if (TempData["CustomError"] != null)
		// {
		// ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
		// }
		// using (var client = new HttpClient())
		// {
		// client.BaseAddress = new Uri(BaseAPI + "Site/");
		// //HTTP POST
		// SiteDetailInputModel filter = new SiteDetailInputModel();
		// filter.SiteID = ID;
		// filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
		// var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
		// postTask.Wait();

		// var result = postTask.Result;
		// if (result.IsSuccessStatusCode)
		// {
		// var content = result.Content.ReadAsStringAsync();
		// SiteDetailResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);
		// item.val = resutl.data.Item.FirstOrDefault();
		// }
		// else //web api sent error response 
		// {
		// //log response status here..
		// item.val = null;
		// ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
		// }
		// }
		// return View(item);
		// }
		// else
		// {
		// TempData["CustomError"] = "Silakan masuk sebelum menggunakan situs web.";
		// if (HttpContext.Session.GetString(Loginfrom) == "ADM/SPV")
		// {
		// return RedirectToAction("AdminLogon", "Login");
		// }
		// else if (HttpContext.Session.GetString(Loginfrom) == "MDO")
		// {
		// return RedirectToAction("OwnerLogon", "Login");
		// }
		// else
		// {
		// return RedirectToAction("OwnerLogon", "Login");
		// }
		// //return RedirectToAction("Logon", "Login");
		// }
		// }

		// [HttpPost]
		// public ActionResult UpdateItm(EditItem Data)
		// {
		// #region GetSite
		// var ID = Guid.Parse(HttpContext.Session.GetString(SessionKeySite));
		// var userID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
		// SiteDetailResultOutputModel existData = new SiteDetailResultOutputModel();
		// using (var client = new HttpClient())
		// {
		// client.BaseAddress = new Uri(BaseAPI + "Site/");
		// //HTTP POST
		// SiteDetailInputModel filter = new SiteDetailInputModel();
		// filter.SiteID = ID;
		// filter.UserID = userID;
		// var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
		// postTask.Wait();

		// var result = postTask.Result;
		// if (result.IsSuccessStatusCode)
		// {
		// var content = result.Content.ReadAsStringAsync();
		// SiteDetailResponseModel results = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);
		// existData = results.data;
		// }
		// else
		// {
		// TempData["CustomError"] = "Error when get site. Please contact administrator.";
		// return RedirectToAction("UpdateItem", "Site", new { @id = HttpContext.Session.GetString(SessionKeyEdit) });
		// }
		// }
		// #endregion
		// AddSitePriceInputModel Price = new AddSitePriceInputModel();
		// EditSiteResultInputModel dataSite = new EditSiteResultInputModel();
		// dataSite.UserID = userID;
		// #region DataDetail
		// EditSiteDetailInputModel Detail = new EditSiteDetailInputModel();
		// Detail.SiteID = ID;
		// Detail.NoBillboard = existData.Detail.NoBillboard;
		// Detail.Pulau = existData.Detail.Pulau;
		// Detail.Kota = existData.Detail.Kota;
		// Detail.Provinsi = existData.Detail.Provinsi;
		// Detail.Cabang = existData.Detail.Cabang;
		// Detail.HorV = existData.Detail.HorV;
		// Detail.Tipe = existData.Detail.Tipe;
		// Detail.Longitude = existData.Detail.Longitude;
		// Detail.Latitude = existData.Detail.Latitude;
		// #endregion
		// dataSite.Detail = Detail;
		// #region DataItem
		// if (existData.Item != null)
		// {
		// EditSiteItemInputModel item = new EditSiteItemInputModel();
		// List<EditSiteItemInputModel> items = new List<EditSiteItemInputModel>();
		// foreach (SiteDetailItemModel existitem in existData.Item)
		// {
		// if (existitem.SiteItemID == Data.input.SiteItemID)
		// {
		// item.SiteID = ID;
		// item.SiteItemID = Data.input.SiteItemID;
		// item.KodeArahLokasi = Data.input.KodeArahLokasi;
		// item.ArahLokasi = Data.input.ArahLokasi;
		// item.Panjang = Data.input.Panjang;
		// item.Lebar = Data.input.Lebar;
		// }
		// else
		// {
		// item.SiteID = existitem.SiteID;
		// item.SiteItemID = existitem.SiteItemID;
		// item.KodeArahLokasi = existitem.ArahLokasi;
		// item.ArahLokasi = existitem.ArahLokasi;
		// item.Panjang = existitem.Panjang;
		// item.Lebar = existitem.Lebar;
		// }
		// items.Add(item);
		// #region DataPrice
		// if (existitem.Price != null)
		// {
		// List<EditSitePriceInputModel> listdata = new List<EditSitePriceInputModel>();
		// foreach (var p in existitem.Price)
		// {
		// EditSitePriceInputModel datas = new EditSitePriceInputModel();
		// datas.Dasar = p.Dasar;
		// datas.HargaAwal = p.HargaAwal;
		// datas.HargaAkhir = p.HargaAkhir;
		// datas.SitePriceID = p.SitePriceID;
		// datas.Kelipatan = p.Kelipatan;
		// datas.MinimDasar = p.MinimDasar;
		// datas.MinimOrder = p.MinimOrder;
		// listdata.Add(datas);
		// }
		// dataSite.Price = listdata;
		// }
		// #endregion
		// #region DataImage
		// if (existitem.Image != null)
		// {
		// List<EditSiteImageInputModel> listdatas = new List<EditSiteImageInputModel>();
		// foreach (var i in existitem.Image)
		// {
		// EditSiteImageInputModel datas = new EditSiteImageInputModel();
		// datas.SiteImageID = i.SiteImageID;
		// datas.Image = i.Image;
		// listdatas.Add(datas);
		// }
		// dataSite.Image = listdatas;
		// }
		// #endregion
		// }
		// dataSite.Item = items;
		// }
		// else
		// {
		// TempData["CustomError"] = "Error when get site. Please contact administrator.";
		// return RedirectToAction("UpdateItem", "Site", new { @id = HttpContext.Session.GetString(SessionKeyEdit) });
		// }
		// #endregion

		// JsonConvert.SerializeObject(dataSite);
		// using (var client = new HttpClient())
		// {
		// client.BaseAddress = new Uri(BaseAPI + "Site/");
		// //HTTP POST
		// var postTask = client.PostAsJsonAsync<EditSiteResultInputModel>("UpdateSite", dataSite);
		// postTask.Wait();

		// var result = postTask.Result;
		// if (result.IsSuccessStatusCode)
		// {
		// HttpContext.Session.SetString(SessionKeySite, "");
		// return RedirectToAction("Index", "Site");
		// }
		// else
		// {
		// TempData["CustomError"] = "Gagal memperbarui data. Mohon hubungi admin.";
		// }
		// }
		// TempData["CustomError"] = "Terjadi kesalahan. Mohon hubungi admin.";
		// return RedirectToAction("UpdateItem", "Site", new { @id = HttpContext.Session.GetString(SessionKeyEdit) });
		// }

		#endregion
		#region CustomItem
		public IActionResult IndexItem(string id)
		{
			HttpContext.Session.SetString(SessionKeySite, id);
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				var ID = Guid.Parse(id);
				EditSite site = new EditSite();
				SelectListItem prov = new SelectListItem();
				List<SelectListItem> provs = new List<SelectListItem>();
				List<SiteDetailItemModel> Items = new List<SiteDetailItemModel>();
				List<CustomSiteDetailItemModel> CItems = new List<CustomSiteDetailItemModel>();
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else
				{
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(BaseAPI + "Site/");
						//HTTP POST
						SiteDetailInputModel filter = new SiteDetailInputModel();
						filter.SiteID = ID;
						var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
						postTask.Wait();

						var result = postTask.Result;
						if (result.IsSuccessStatusCode)
						{
							var content = result.Content.ReadAsStringAsync();
							SiteDetailResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);
							site.val = resutl.data;
							if (site.val != null && site.val.Item != null && site.val.Item.Count > 0)
							{
								Items = site.val.Item.Where(x => x.SiteID == ID).ToList();
								int itemNo = 0;
								foreach (SiteDetailItemModel item in Items)
								{
									itemNo = itemNo + 1;
									if (item.Price != null && item.Price.Count > 0)
									{
										foreach (var dtl in item.Price)
										{
											CustomSiteDetailItemModel Citem = new CustomSiteDetailItemModel();
											Citem.SiteID = item.SiteID;
											Citem.SiteItemID = item.SiteItemID;
											Citem.WishlistID = item.Wishlist != null ? item.Wishlist.WishlistID : Guid.Empty;
											Citem.SitePriceID = dtl.SitePriceID;
											Citem.SiteImageID = item.Image != null && item.Image.Count > 0 ? item.Image[0].SiteImageID : Guid.Empty;
											Citem.ItemName = "Item " + itemNo;
											Citem.ArahLokasi = item.ArahLokasi;
											Citem.KodeLokasi = item.KodeLokasi;
											Citem.Panjang = item.Panjang;
											Citem.Lebar = item.Lebar;
											Citem.isWishlist = item.Wishlist != null ? item.Wishlist.isWishlist : false;
											Citem.Dasar = dtl.Dasar;
											Citem.Kelipatan = dtl.Kelipatan;
											Citem.HargaAwal = dtl.HargaAwal;
											Citem.HargaAkhir = dtl.HargaAkhir;
											Citem.MinimDasar = dtl.MinimOrder + dtl.MinimDasar;
											Citem.MinimOrder = dtl.MinimOrder;
											Citem.Image = item.Image != null && item.Image.Count > 0 ? item.Image[0].Image : "";
											CItems.Add(Citem);
										}
									}
									else
									{
										CustomSiteDetailItemModel Citem = new CustomSiteDetailItemModel();
										Citem.SiteID = item.SiteID;
										Citem.SiteItemID = item.SiteItemID;
										Citem.WishlistID = item.Wishlist != null ? item.Wishlist.WishlistID : Guid.Empty;
										Citem.SitePriceID = Guid.Empty;
										Citem.SiteImageID = item.Image != null && item.Image.Count > 0 ? item.Image[0].SiteImageID : Guid.Empty;
										Citem.ItemName = "Item " + itemNo;
										Citem.ArahLokasi = item.ArahLokasi;
										Citem.KodeLokasi = item.KodeLokasi;
										Citem.Panjang = item.Panjang;
										Citem.Lebar = item.Lebar;
										Citem.isWishlist = item.Wishlist != null ? item.Wishlist.isWishlist : false;
										Citem.Dasar = "";
										Citem.Kelipatan = 0;
										Citem.HargaAwal = 0;
										Citem.HargaAkhir = 0;
										Citem.MinimDasar = "";
										Citem.MinimOrder = 0;
										Citem.Image = item.Image != null && item.Image.Count > 0 ? item.Image[0].Image : "";
										CItems.Add(Citem);
									}
								}
							}
						}
						else //web api sent error response 
						{
							//log response status here..
							ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
						}
					}
				}
				return View(CItems);
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

		public IActionResult CreateItem()
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
		public ActionResult AddSiteItem(CustomSiteDetailItemModel Data)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			AddSiteItemDetailInputModel dataSite = new AddSiteItemDetailInputModel();
			dataSite.SiteID = Guid.Parse(HttpContext.Session.GetString(SessionKeySite));
			dataSite.KodeArahLokasi = Data.KodeLokasi;
			dataSite.ArahLokasi = Data.ArahLokasi;
			dataSite.Panjang = Data.Panjang;
			dataSite.Lebar = Data.Lebar;
			JsonConvert.SerializeObject(dataSite);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<AddSiteItemDetailInputModel>("AddSiteItemDetail", dataSite);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					AddSiteItemDetailResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<AddSiteItemDetailResponseModel>(content.Result);
					var SiteItemID = resutl.data.SiteItemDetailID;
					#region addPrice
					AddSiteResultPriceInputModel Price = new AddSiteResultPriceInputModel();
					AddSitePriceInputModel data = new AddSitePriceInputModel();
					List<AddSitePriceInputModel> lstPrice = new List<AddSitePriceInputModel>();
					data.Dasar = Data.MinimDasar;
					data.HargaAwal = Data.HargaAwal;
					data.HargaAkhir = 0;
					data.Kelipatan = Data.MinimOrder;
					lstPrice.Add(data);

					Price.UserID = Guid.Parse(userID);
					Price.SiteID = Guid.Parse(HttpContext.Session.GetString(SessionKeySite));
					Price.SiteItemID = SiteItemID;
					Price.Price = lstPrice;
					Price.MinimDasar = lstPrice[0].Dasar;
					Price.MinimOrder = Convert.ToInt32(lstPrice[0].Kelipatan);

					JsonConvert.SerializeObject(Price);
					using (var client1 = new HttpClient())
					{
						client1.BaseAddress = new Uri(BaseAPI + "Site/");
						//HTTP POST
						var postTask1 = client.PostAsJsonAsync<AddSiteResultPriceInputModel>("AddSitePrice", Price);
						postTask1.Wait();

						var result1 = postTask.Result;
						if (result1.IsSuccessStatusCode)
						{
							return RedirectToAction("IndexItem", new RouteValueDictionary(
									new
									{
										controller = "Site",
										action = "IndexItem",
										Id = HttpContext.Session.GetString(SessionKeySite)
									}
								));
						}
						else
						{
							TempData["CustomError"] = "Gagal menambah data. Mohon hubungi admin.";
						}
					}
					#endregion
					//return RedirectToAction("IndexItem", new RouteValueDictionary(
					//		new {
					//			controller = "Site", action = "IndexItem", Id = HttpContext.Session.GetString(SessionKeySite)
					//		}
					//	));
				}
				else
				{
					TempData["CustomError"] = "Gagal menambah data. Mohon hubungi admin.";
				}
			}
			TempData["CustomError"] = "Terjadi kesalahan. Mohon hubungi admin.";
			return RedirectToAction("CreateItem", "Site");
		}

		public ActionResult UpdateItem(string itemid, string priceid)
		{
			HttpContext.Session.SetString(SessionKeyEdit, itemid);
			HttpContext.Session.SetString(SessionKeyPrice, priceid);
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				var ID = Guid.Parse(HttpContext.Session.GetString(SessionKeySite));
				EditDetail itemdtl = new EditDetail();
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Site/");
					//HTTP POST
					SiteDetailInputModel filter = new SiteDetailInputModel();
					filter.SiteID = ID;
					filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
					var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
					postTask.Wait();

					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						SiteDetailResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);
						List<SiteDetailItemModel> Items = resutl.data.Item.Where(x => x.SiteID == ID).ToList();
						int itemNo = 0;
						foreach (SiteDetailItemModel item in Items)
						{
							itemNo = itemNo + 1;
							if (resutl.data.Item.Where(x => x.SiteItemID == Guid.Parse(itemid)).ToList().Count > 0)
							{
								CustomSiteDetailItemModel Citem = new CustomSiteDetailItemModel();
								Citem.SiteID = item.SiteID;
								Citem.SiteItemID = item.SiteItemID;
								Citem.WishlistID = item.Wishlist != null ? item.Wishlist.WishlistID : Guid.Empty;
								Citem.SitePriceID = Guid.Empty;
								Citem.SiteImageID = item.Image != null && item.Image.Count > 0 ? item.Image[0].SiteImageID : Guid.Empty;
								Citem.ItemName = "Item " + itemNo;
								Citem.ArahLokasi = item.ArahLokasi;
								Citem.KodeLokasi = item.KodeLokasi;
								Citem.Panjang = item.Panjang;
								Citem.Lebar = item.Lebar;
								Citem.isWishlist = item.Wishlist != null ? item.Wishlist.isWishlist : false;
								Citem.Dasar = item.Price != null && item.Price.Count > 0 && item.Price.Where(x => x.SitePriceID == Guid.Parse(priceid)).ToList().Count > 0 ? item.Price.Where(x => x.SitePriceID == Guid.Parse(priceid)).Select(x => x.Dasar).FirstOrDefault().ToString() : "";
								Citem.Kelipatan = item.Price != null && item.Price.Count > 0 && item.Price.Where(x => x.SitePriceID == Guid.Parse(priceid)).ToList().Count > 0 ? double.Parse(item.Price.Where(x => x.SitePriceID == Guid.Parse(priceid)).Select(x => x.Kelipatan).FirstOrDefault().ToString()) : 0;
								Citem.HargaAwal = item.Price != null && item.Price.Count > 0 && item.Price.Where(x => x.SitePriceID == Guid.Parse(priceid)).ToList().Count > 0 ? double.Parse(item.Price.Where(x => x.SitePriceID == Guid.Parse(priceid)).Select(x => x.HargaAwal).FirstOrDefault().ToString()) : 0;
								Citem.HargaAkhir = item.Price != null && item.Price.Count > 0 && item.Price.Where(x => x.SitePriceID == Guid.Parse(priceid)).ToList().Count > 0 ? double.Parse(item.Price.Where(x => x.SitePriceID == Guid.Parse(priceid)).Select(x => x.HargaAkhir).FirstOrDefault().ToString()) : 0;
								Citem.MinimDasar = item.Price != null && item.Price.Count > 0 && item.Price.Where(x => x.SitePriceID == Guid.Parse(priceid)).ToList().Count > 0 ? item.Price.Where(x => x.SitePriceID == Guid.Parse(priceid)).Select(x => x.MinimDasar).FirstOrDefault().ToString() : "";
								Citem.MinimOrder = item.Price != null && item.Price.Count > 0 && item.Price.Where(x => x.SitePriceID == Guid.Parse(priceid)).ToList().Count > 0 ? int.Parse(item.Price.Where(x => x.SitePriceID == Guid.Parse(priceid)).Select(x => x.MinimOrder).FirstOrDefault().ToString()) : 0;
								Citem.Image = item.Image != null && item.Image.Count > 0 ? item.Image[0].Image : "";
								itemdtl.val = Citem;
							}
						}
					}
					else //web api sent error response 
					{
						//log response status here..
						itemdtl.val = null;
						ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
					}
				}
				return View(itemdtl);
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
		public ActionResult UpdateItm(EditDetail Data)
		{
			#region GetSite
			var ID = Guid.Parse(HttpContext.Session.GetString(SessionKeySite));
			var userID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			SiteDetailResultOutputModel existData = new SiteDetailResultOutputModel();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				//HTTP POST
				SiteDetailInputModel filter = new SiteDetailInputModel();
				filter.SiteID = ID;
				filter.UserID = userID;
				var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					SiteDetailResponseModel results = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);
					existData = results.data;
				}
				else
				{
					TempData["CustomError"] = "Error when get site. Please contact administrator.";
					return RedirectToAction("UpdateItem", "Site", new { @itemid = HttpContext.Session.GetString(SessionKeyEdit), @priceid = HttpContext.Session.GetString(SessionKeyPrice) });
				}
			}
			#endregion
			AddSitePriceInputModel Price = new AddSitePriceInputModel();
			EditSiteResultInputModel dataSite = new EditSiteResultInputModel();
			dataSite.UserID = userID;
			#region DataDetail
			EditSiteDetailInputModel Detail = new EditSiteDetailInputModel();
			Detail.SiteID = ID;
			Detail.NoBillboard = existData.Detail.NoBillboard;
			Detail.Address = existData.Detail.Alamat;
			Detail.Pulau = existData.Detail.Pulau;
			Detail.Kota = existData.Detail.Kota;
			Detail.Provinsi = existData.Detail.Provinsi;
			Detail.Cabang = existData.Detail.Cabang;
			Detail.HorV = existData.Detail.HorV;
			Detail.Tipe = existData.Detail.Tipe;
			Detail.Longitude = existData.Detail.Longitude;
			Detail.Latitude = existData.Detail.Latitude;
			#endregion
			dataSite.Detail = Detail;
			#region DataItem
			if (existData.Item != null)
			{
				EditSiteItemInputModel item = new EditSiteItemInputModel();
				List<EditSiteItemInputModel> items = new List<EditSiteItemInputModel>();
				foreach (SiteDetailItemModel existitem in existData.Item)
				{
					if (existitem.SiteItemID == Data.input.SiteItemID)
					{
						item.SiteID = ID;
						item.SiteItemID = Data.input.SiteItemID;
						item.KodeArahLokasi = Data.input.KodeLokasi;
						item.ArahLokasi = Data.input.ArahLokasi;
						item.Panjang = Data.input.Panjang;
						item.Lebar = Data.input.Lebar;
					}
					else
					{
						item.SiteID = existitem.SiteID;
						item.SiteItemID = existitem.SiteItemID;
						item.KodeArahLokasi = existitem.ArahLokasi;
						item.ArahLokasi = existitem.ArahLokasi;
						item.Panjang = existitem.Panjang;
						item.Lebar = existitem.Lebar;
					}
					items.Add(item);
					#region DataPrice
					if (existitem.Price != null)
					{
						List<EditSitePriceInputModel> listdata = new List<EditSitePriceInputModel>();
						foreach (var p in existitem.Price)
						{
							EditSitePriceInputModel datas = new EditSitePriceInputModel();
							if (p.SitePriceID == Data.input.SitePriceID)
							{
								datas.Dasar = Data.input.MinimDasar;// isinya sama dengan minDasar
								datas.HargaAwal = Data.input.HargaAwal;
								datas.HargaAkhir = p.HargaAkhir;
								datas.SitePriceID = Data.input.SitePriceID;
								datas.Kelipatan = Data.input.MinimOrder;// isinya sama dengan minOrder
								datas.MinimDasar = Data.input.MinimDasar;
								datas.MinimOrder = Convert.ToInt32(Data.input.MinimOrder);
							}
							else
							{
								datas.Dasar = p.Dasar;
								datas.HargaAwal = p.HargaAwal;
								datas.HargaAkhir = p.HargaAkhir;
								datas.SitePriceID = p.SitePriceID;
								datas.Kelipatan = p.Kelipatan;
								datas.MinimDasar = p.MinimDasar;
								datas.MinimOrder = p.MinimOrder;
							}
							listdata.Add(datas);
						}
						dataSite.Price = listdata;
					}
					#endregion
					#region DataImage
					if (existitem.Image != null)
					{
						List<EditSiteImageInputModel> listdatas = new List<EditSiteImageInputModel>();
						foreach (var i in existitem.Image)
						{
							EditSiteImageInputModel datas = new EditSiteImageInputModel();
							datas.SiteImageID = i.SiteImageID;
							datas.Image = i.Image;
							listdatas.Add(datas);
						}
						dataSite.Image = listdatas;
					}
					#endregion
				}
				dataSite.Item = items;
			}
			else
			{
				TempData["CustomError"] = "Error when get site. Please contact administrator.";
				return RedirectToAction("UpdateItem", "Site", new { @itemid = HttpContext.Session.GetString(SessionKeyEdit), @priceid = HttpContext.Session.GetString(SessionKeyPrice) });
			}
			#endregion

			JsonConvert.SerializeObject(dataSite);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<EditSiteResultInputModel>("UpdateSite", dataSite);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("IndexItem", new RouteValueDictionary(
							new
							{
								controller = "Site",
								action = "IndexItem",
								Id = HttpContext.Session.GetString(SessionKeySite)
							}
						));
				}
				else
				{
					TempData["CustomError"] = "Gagal memperbarui data. Mohon hubungi admin.";
				}
			}
			TempData["CustomError"] = "Terjadi kesalahan. Mohon hubungi admin.";
			return RedirectToAction("UpdateItem", "Site", new { @itemid = HttpContext.Session.GetString(SessionKeyEdit), @priceid = HttpContext.Session.GetString(SessionKeyPrice) });
		}

		#endregion

		#region Price
		public IActionResult IndexPrice(string id)
		{
			HttpContext.Session.SetString(SessionKeyEdit, id);
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				var ID = Guid.Parse(id);
				var SiteID = Guid.Parse(HttpContext.Session.GetString(SessionKeySite));
				EditSite site = new EditSite();
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else
				{
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(BaseAPI + "Site/");
						//HTTP POST
						SiteDetailInputModel filter = new SiteDetailInputModel();
						filter.SiteID = SiteID;
						var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
						postTask.Wait();

						var result = postTask.Result;
						if (result.IsSuccessStatusCode)
						{
							var content = result.Content.ReadAsStringAsync();
							SiteDetailResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);
							site.val = resutl.data;
							site.val.Item = site.val.Item.Where(x => x.SiteItemID == ID).ToList();
						}
						else //web api sent error response 
						{
							//log response status here..
							site.val = null;
							ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
						}
					}
				}
				return View(site);
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

		public IActionResult CreatePrice()
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
		public ActionResult AddPrice(AddSitePriceInputModel Data)
		{
			var userID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			var siteID = Guid.Parse(HttpContext.Session.GetString(SessionKeySite));
			var siteItemID = Guid.Parse(HttpContext.Session.GetString(SessionKeyEdit));
			AddSiteResultPriceInputModel Price = new AddSiteResultPriceInputModel();
			AddSitePriceInputModel data = new AddSitePriceInputModel();
			List<AddSitePriceInputModel> lstPrice = new List<AddSitePriceInputModel>();
			data.Dasar = Data.Dasar;
			data.HargaAwal = Data.HargaAwal;
			data.HargaAkhir = Data.HargaAkhir;
			data.Kelipatan = Data.Kelipatan;
			lstPrice.Add(data);

			Price.UserID = userID;
			Price.SiteID = siteID;
			Price.SiteItemID = siteItemID;
			Price.Price = lstPrice;
			Price.MinimDasar = lstPrice[0].Dasar;
			Price.MinimOrder = Convert.ToInt32(lstPrice[0].Kelipatan);

			JsonConvert.SerializeObject(Price);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<AddSiteResultPriceInputModel>("AddSitePrice", Price);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return RedirectToAction("IndexPrice", new RouteValueDictionary(
							new
							{
								controller = "Site",
								action = "IndexPrice",
								Id = HttpContext.Session.GetString(SessionKeyEdit)
							}
						));
				}
				else
				{
					TempData["CustomError"] = "Gagal menambah data. Mohon hubungi admin.";
				}
			}
			TempData["CustomError"] = "Terjadi kesalahan. Mohon hubungi admin.";
			return RedirectToAction("CreatePrice", "Site");
		}

		public ActionResult UpdatePrice(string id)
		{
			HttpContext.Session.SetString(SessionKeyEdit, id);
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				var ID = Guid.Parse(HttpContext.Session.GetString(SessionKeySite));
				EditPrice price = new EditPrice();
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Site/");
					//HTTP POST
					SiteDetailInputModel filter = new SiteDetailInputModel();
					filter.SiteID = ID;
					filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
					var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
					postTask.Wait();

					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						SiteDetailResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);
						price.val = resutl.data.Item.Select(x => x.Price.Where(y => y.SitePriceID == Guid.Parse(id)).FirstOrDefault()).FirstOrDefault();
					}
					else //web api sent error response 
					{
						//log response status here..
						price.val = null;

						ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
					}
				}
				return View(price);
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
		public ActionResult UpdateP(EditPrice Data)
		{
			#region GetSite
			var ID = Guid.Parse(HttpContext.Session.GetString(SessionKeySite));
			var userID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			SiteDetailResultOutputModel existData = new SiteDetailResultOutputModel();
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				//HTTP POST
				SiteDetailInputModel filter = new SiteDetailInputModel();
				filter.SiteID = ID;
				filter.UserID = userID;
				var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					SiteDetailResponseModel results = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);
					existData = results.data;
				}
				else
				{
					TempData["CustomError"] = "Error when get site. Please contact administrator.";
					return RedirectToAction("UpdatePrice", "Site", new { @id = HttpContext.Session.GetString(SessionKeyEdit) });
				}
			}
			#endregion
			AddSitePriceInputModel Price = new AddSitePriceInputModel();
			EditSiteResultInputModel dataSite = new EditSiteResultInputModel();
			dataSite.UserID = userID;
			#region DataDetail
			EditSiteDetailInputModel Detail = new EditSiteDetailInputModel();
			Detail.SiteID = ID;
			Detail.NoBillboard = existData.Detail.NoBillboard;
			Detail.Pulau = existData.Detail.Pulau;
			Detail.Kota = existData.Detail.Kota;
			Detail.Provinsi = existData.Detail.Provinsi;
			Detail.Cabang = existData.Detail.Cabang;
			Detail.HorV = existData.Detail.HorV;
			Detail.Tipe = existData.Detail.Tipe;
			Detail.Longitude = existData.Detail.Longitude;
			Detail.Latitude = existData.Detail.Latitude;
			#endregion
			dataSite.Detail = Detail;
			#region DataItem
			if (existData.Item != null)
			{
				EditSiteItemInputModel item = new EditSiteItemInputModel();
				List<EditSiteItemInputModel> items = new List<EditSiteItemInputModel>();
				foreach (SiteDetailItemModel existitem in existData.Item)
				{
					item.SiteID = ID;
					item.SiteItemID = existData.Item[0].SiteItemID;
					item.KodeArahLokasi = existData.Item[0].KodeLokasi;
					item.ArahLokasi = existData.Item[0].ArahLokasi;
					item.Panjang = existData.Item[0].Panjang;
					item.Lebar = existData.Item[0].Lebar;
					items.Add(item);
					#region DataPrice
					if (existData.Item[0].Price != null)
					{
						List<EditSitePriceInputModel> listdata = new List<EditSitePriceInputModel>();
						foreach (var p in existData.Item[0].Price)
						{
							EditSitePriceInputModel datas = new EditSitePriceInputModel();
							if (p.SitePriceID == Data.input.SitePriceID)
							{
								datas.Dasar = Data.input.Dasar;
								datas.HargaAwal = Data.input.HargaAwal;
								datas.HargaAkhir = Data.input.HargaAkhir;
								datas.SitePriceID = Data.input.SitePriceID;
								datas.Kelipatan = Data.input.Kelipatan;
								datas.MinimDasar = Data.input.Dasar;
								datas.MinimOrder = Convert.ToInt32(Data.input.Kelipatan);
							}
							else
							{
								datas.Dasar = p.Dasar;
								datas.HargaAwal = p.HargaAwal;
								datas.HargaAkhir = p.HargaAkhir;
								datas.SitePriceID = p.SitePriceID;
								datas.Kelipatan = p.Kelipatan;
								datas.MinimDasar = p.MinimDasar;
								datas.MinimOrder = p.MinimOrder;
							}
							listdata.Add(datas);
						}
						dataSite.Price = listdata;
					}
					#endregion
					#region DataImage
					if (existData.Item[0].Image != null)
					{
						List<EditSiteImageInputModel> listdatas = new List<EditSiteImageInputModel>();
						foreach (var i in existData.Item[0].Image)
						{
							EditSiteImageInputModel datas = new EditSiteImageInputModel();
							datas.SiteImageID = i.SiteImageID;
							datas.Image = i.Image;
							listdatas.Add(datas);
						}
						dataSite.Image = listdatas;
					}
					#endregion
				}
				dataSite.Item = items;
			}
			else
			{
				TempData["CustomError"] = "Error when get site. Please contact administrator.";
				return RedirectToAction("UpdatePrice", "Site", new { @id = HttpContext.Session.GetString(SessionKeyEdit) });
			}
			#endregion

			JsonConvert.SerializeObject(dataSite);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<EditSiteResultInputModel>("UpdateSite", dataSite);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					HttpContext.Session.SetString(SessionKeySite, "");
					return RedirectToAction("Index", "Site");
				}
				else
				{
					TempData["CustomError"] = "Gagal memperbarui data. Mohon hubungi admin.";
				}
			}
			TempData["CustomError"] = "Terjadi kesalahan. Mohon hubungi admin.";
			return RedirectToAction("UpdatePrice", "Site", new { @id = HttpContext.Session.GetString(SessionKeyEdit) });
		}

		[HttpPost]
		public ActionResult DeletePrice(string id)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			//DeleteBankInputModel dataSite = new DeleteBankInputModel();
			//dataSite.BankID = Guid.Parse(id);
			//dataSite.UserID = Guid.Parse(userID);
			//JsonConvert.SerializeObject(dataSite);
			using (var client = new HttpClient())
			{
				//client.BaseAddress = new Uri(BaseAPI + "Admin/");
				////HTTP POST
				//var postTask = client.PostAsJsonAsync<DeleteBankInputModel>("DeleteBank", dataSite);
				//postTask.Wait();

				//var result = postTask.Result;
				//if (result.IsSuccessStatusCode)
				//{
				return RedirectToAction("IndexPrice", "Site");
				//}
			}

			ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");

			return RedirectToAction("Logon", "Login");
		}
		#endregion

		#region Image
		public IActionResult IndexImage(string id)
		{
			HttpContext.Session.SetString(SessionKeyEdit, id);
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				var ID = Guid.Parse(id);
				var SiteID = Guid.Parse(HttpContext.Session.GetString(SessionKeySite));
				EditSite site = new EditSite();
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else
				{
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(BaseAPI + "Site/");
						SiteDetailInputModel filter = new SiteDetailInputModel();
						filter.SiteID = SiteID;
						var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
						postTask.Wait();

						var result = postTask.Result;
						if (result.IsSuccessStatusCode)
						{
							var content = result.Content.ReadAsStringAsync();
							SiteDetailResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);
							site.val = resutl.data;
							site.val.Item = site.val.Item.Where(x => x.SiteItemID == ID).ToList();
						}
						else //web api sent error response 
						{
							//log response status here..
							site.val = null;
							ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
						}
					}
				}
				return View(site);
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

		public IActionResult CreateImage()
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
		//public async Task<ActionResult> AddImageAsync(IFormFile x)
		//public ActionResult AddImageAsync(IFormFile files)
		public async Task<ActionResult> AddImage()
		{
			Guid userID = Guid.Empty;
			Guid siteID = Guid.Empty;
			Guid siteItemID = Guid.Empty;
			if (Guid.TryParse(HttpContext.Session.GetString(SessionKeyID), out Guid guid))
			{
				userID = guid;
			}
			if (Guid.TryParse(HttpContext.Session.GetString(SessionKeySite), out Guid guids))
			{
				siteID = guids;
			}
			if (Guid.TryParse(HttpContext.Session.GetString(SessionKeyEdit), out Guid guidi))
			{
				siteItemID = guidi;
			}
			AddSiteResultImageInputModel Image = new AddSiteResultImageInputModel();
			AddSiteImageInputModel data = new AddSiteImageInputModel();
			List<AddSiteImageInputModel> lstImage = new List<AddSiteImageInputModel>();
			#region DataImage
			IFormFile file = Request.Form.Files[0];
			string folderName = "Upload\\Image";
			string folderroot = "wwwroot";
			string webRootPath = "";
			if (HttpContext.Session.GetString(SessionKeyDomain) != null && HttpContext.Session.GetString(SessionKeyDomain) != "")
			{
				webRootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			}
			else
			{
				webRootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin\\")));
			}
			string newPath = Path.Combine(webRootPath, folderroot, folderName);
			if (!Directory.Exists(newPath))
			{
				Directory.CreateDirectory(newPath);
			}
			string newFileName = Guid.NewGuid() + ".png";
			string fullPath = Path.Combine(newPath, newFileName);
			using (var stream = new FileStream(fullPath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			if (HttpContext.Session.GetString(SessionKeyDomain) != null && HttpContext.Session.GetString(SessionKeyDomain) != "")
			{
				data.Image = "/" + Domain + "/Upload/Image/" + newFileName;
			}
			else
			{
				data.Image = "/Upload/Image/" + newFileName;
			}
			lstImage.Add(data);
			#endregion

			Image.UserID = userID;
			Image.SiteID = siteID;
			Image.SiteitemID = siteItemID;
			Image.Image = lstImage;

			JsonConvert.SerializeObject(Image);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				//HTTP POST
				var postTask = client.PostAsJsonAsync<AddSiteResultImageInputModel>("AddSiteImage", Image);
				postTask.Wait();

				var result = postTask.Result;
				if (result.IsSuccessStatusCode)
				{
					return Json(new { Message = string.Empty });
					//return RedirectToAction("Index", "Site");
				}
			}

			ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");

			//return RedirectToAction("Logon", "Login");
			return Json(new { Message = string.Empty });
		}

		public ActionResult UpdateImage(string id)
		{
			HttpContext.Session.SetString(SessionKeyEdit, id);
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				var ID = Guid.Parse(HttpContext.Session.GetString(SessionKeySite));
				EditImage Image = new EditImage();
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Site/");
					//HTTP POST
					SiteDetailInputModel filter = new SiteDetailInputModel();
					filter.SiteID = ID;
					var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
					postTask.Wait();

					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						SiteDetailResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);
						Image.val = resutl.data.Item.Select(x => x.Image.Where(y => y.SiteImageID == Guid.Parse(id)).FirstOrDefault()).FirstOrDefault();
						var ids = resutl.data.Item.SelectMany(x => x.Image);//.Where(x => x.SiteImageID == Guid.Parse(id))
					}
					else //web api sent error response 
					{
						//log response status here..
						Image.val = null;

						ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
					}
				}
				return View(Image);
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

		//[HttpPost]
		//public ActionResult UpdateI()
		//{
		//	#region GetSite
		//	var ID = Guid.Parse(HttpContext.Session.GetString(SessionKeySite));
		//	SiteDetailInputModel existData = new SiteDetailInputModel();
		//	using (var client = new HttpClient())
		//	{
		//		client.BaseAddress = new Uri(BaseAPI + "Site/");
		//		//HTTP POST
		//		SiteDetailInputModel filter = new SiteDetailInputModel();
		//		filter.SiteID = ID;
		//		var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
		//		postTask.Wait();

		//		var result = postTask.Result;
		//		if (result.IsSuccessStatusCode)
		//		{
		//			var content = result.Content.ReadAsStringAsync();
		//			SiteDetailResponseAdmin results = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseAdmin>(content.Result);
		//			existData = results.data.Item;
		//		}
		//	}
		//	#endregion
		//	string userID = HttpContext.Session.GetString(SessionKeyID);
		//	AddSiteImageInputModel Image = new AddSiteImageInputModel();
		//	EditSiteResultInputModel dataSite = new EditSiteResultInputModel();
		//	dataSite.UserID = Guid.Parse(userID);
		//	#region DataDetail
		//	EditSiteDetailInputModel Detail = new EditSiteDetailInputModel();
		//	Detail.SiteID = Guid.Parse(HttpContext.Session.GetString(SessionKeySite));
		//	Detail.NoBillboard = existData.Detail.NoBillboard;
		//	Detail.Pulau = existData.Detail.Pulau;
		//	Detail.Kota = existData.Detail.Kota;
		//	Detail.Provinsi = existData.Detail.Provinsi;
		//	Detail.Cabang = existData.Detail.Cabang;
		//	Detail.Panjang = existData.Detail.Panjang;
		//	Detail.Lebar = existData.Detail.Lebar;
		//	Detail.HorV = existData.Detail.HorV;
		//	Detail.Tipe = existData.Detail.Tipe;
		//	Detail.Longitude = existData.Detail.Longitude;
		//	Detail.Latitude = existData.Detail.Latitude;
		//	#endregion
		//	dataSite.Detail = Detail;
		//	#region DataPrice
		//	if (existData.Price != null)
		//	{
		//		List<EditSitePriceInputModel> listdata = new List<EditSitePriceInputModel>();
		//		foreach (var p in existData.Price)
		//		{
		//			EditSitePriceInputModel datas = new EditSitePriceInputModel();
		//			datas.Dasar = p.Dasar;
		//			datas.HargaAwal = p.HargaAwal;
		//			datas.HargaAkhir = p.HargaAkhir;
		//			datas.SitePriceID = p.SitePriceID;
		//			datas.Kelipatan = p.Kelipatan;
		//			listdata.Add(datas);
		//		}
		//		dataSite.Price = listdata;
		//	}
		//	#endregion
		//	#region DataImage
		//	IFormFile file = Request.Form.Files[0];
		//	string folderName = "Upload\\Image";
		//	string folderroot = "wwwroot";
			//string webRootPath = "";
			//if (HttpContext.Session.GetString(SessionKeyDomain) != null && HttpContext.Session.GetString(SessionKeyDomain) != "")
			//{
			//	webRootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			//}
			//else
			//{
			//	webRootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin\\")));
			//}
		//	string newPath = Path.Combine(webRootPath, folderroot, folderName);
		//	if (!Directory.Exists(newPath))
		//	{
		//		Directory.CreateDirectory(newPath);
		//	}
		//	string fullPath = Path.Combine(newPath, file.FileName);
		//	using (var stream = new FileStream(fullPath, FileMode.Create))
		//	{
		//		file.CopyTo(stream);
		//	}
		//	if (existData.Image != null)
		//	{
		//		List<EditSiteImageInputModel> listdatas = new List<EditSiteImageInputModel>();
		//		foreach (var i in existData.Image)
		//		{
		//			EditSiteImageInputModel datas = new EditSiteImageInputModel();
		//			if (i.SiteImageID == Guid.Parse(HttpContext.Session.GetString(SessionKeyEdit)))
		//			{
		//				datas.SiteImageID = i.SiteImageID;
		//				datas.Image = "/MediaWeb/Upload/Image/" + file.FileName;
		//			}
		//			else
		//			{
		//				datas.SiteImageID = i.SiteImageID;
		//				datas.Image = i.Image;
		//			}
		//			listdatas.Add(datas);
		//		}
		//		dataSite.Image = listdatas;
		//	}
		//	#endregion

		//	JsonConvert.SerializeObject(dataSite);
		//	using (var client = new HttpClient())
		//	{
		//		client.BaseAddress = new Uri(BaseAPI + "Site/");
		//		//HTTP POST
		//		var postTask = client.PostAsJsonAsync<EditSiteResultInputModel>("UpdateSite", dataSite);
		//		postTask.Wait();

		//		var result = postTask.Result;
		//		if (result.IsSuccessStatusCode)
		//		{
		//			HttpContext.Session.SetString(SessionKeySite, "");
		//			return RedirectToAction("Index", "Site");
		//		}
		//	}
		//	ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
		//	return RedirectToAction("Logon", "Login");
		//}

		[HttpPost]
		public ActionResult DeleteImage(string id)
		{
			string userID = HttpContext.Session.GetString(SessionKeyID);
			//DeleteBankInputModel dataSite = new DeleteBankInputModel();
			//dataSite.BankID = Guid.Parse(id);
			//dataSite.UserID = Guid.Parse(userID);
			//JsonConvert.SerializeObject(dataSite);
			using (var client = new HttpClient())
			{
				//client.BaseAddress = new Uri(BaseAPI + "Admin/");
				////HTTP POST
				//var postTask = client.PostAsJsonAsync<DeleteBankInputModel>("DeleteBank", dataSite);
				//postTask.Wait();

				//var result = postTask.Result;
				//if (result.IsSuccessStatusCode)
				//{
				return RedirectToAction("IndexImage", "Site");
				//}
			}

			ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");

			return RedirectToAction("Logon", "Login");
		}
		#endregion

		#region ViewDetail
		public IActionResult ViewDetail(string id)
		{
			HttpContext.Session.SetString(SessionKeyEdit, id);
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				var ID = Guid.Parse(id);
				ForDetail site = new ForDetail();
				var cities = new List<City>();
				var BillBoards = new SiteDetailResultOutputModel();
				if (TempData["CustomError"] != null)
				{
					ModelState.AddModelError(string.Empty, TempData["CustomError"].ToString());
				}
				else
				{
					using (var client = new HttpClient())
					{
						client.BaseAddress = new Uri(BaseAPI + "Site/");
						//HTTP POST
						SiteDetailInputModel filter = new SiteDetailInputModel();
						filter.SiteID = ID;
						filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
						var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
						postTask.Wait();

						var result = postTask.Result;
						if (result.IsSuccessStatusCode)
						{
							var content = result.Content.ReadAsStringAsync();
							SiteDetailResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);
							if (resutl.data != null)
							{
								BillBoards = resutl.data;
								var city = new City();

								city.Title = BillBoards.Detail.NoBillboard;
								city.Lat = double.Parse(BillBoards.Detail.Latitude, System.Globalization.CultureInfo.InvariantCulture);
								city.Lng = double.Parse(BillBoards.Detail.Longitude, System.Globalization.CultureInfo.InvariantCulture);
								city.Kota = BillBoards.Detail.Kota;
								city.Prov = BillBoards.Detail.Provinsi;
								city.HorV = BillBoards.Detail.HorV;
								city.Tipe = BillBoards.Detail.Tipe;
								city.Rate = BillBoards.Detail.RateScoreAverage;
								city.Price = "0";
								city.Image = "../image/NoImage.jpg";

								if (BillBoards.Detail.ImageHeader != null && BillBoards.Detail.ImageHeader.Contains("/Upload")) {
									city.Image = BillBoards.Detail.ImageHeader;
								}

								if (BillBoards.Item != null && BillBoards.Item.Count > 0)
								{
									if (BillBoards.Item[0].Price != null && BillBoards.Item[0].Price.Count > 0)
									{
										city.Price = BillBoards.Item[0].Price[0].HargaAwal.ToString("N");
									}
									//if (BillBoards.Item[0].Image != null && BillBoards.Item[0].Image.Count > 0)
									//{
									//	city.Image = BillBoards.Item[0].Image[0].Image;
									//}
								}
								cities.Add(city);

								site.Site = BillBoards;
								site.Cities = cities;
							}
						}
						else //web api sent error response 
						{
							//log response status here..
							site = null;
							ModelState.AddModelError(string.Empty, "Terjadi kesalahan. Mohon hubungi admin.");
						}
					}
				}
				return View(site);
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
		#endregion

		#region CartPopUp
		[HttpPost]
		public ActionResult AddtoCart(string siteID, string sitePriceID, string siteItemID, string price, string durasi, string totalprice, string startDate, string endDate)
		{
			AddBookOutputModel OutPutData = new AddBookOutputModel();
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				AddBookInputModel dataSite = new AddBookInputModel();
				dataSite.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
				dataSite.SiteID = Guid.Parse(siteID);
				dataSite.SiteItemID = Guid.Parse(siteItemID);
				dataSite.SitePriceID = Guid.Parse(sitePriceID);
				dataSite.StartDate = DateTime.Parse(startDate);
				dataSite.EndDate = DateTime.Parse(endDate);
				dataSite.Price = double.Parse(price);
				dataSite.Qty = int.Parse(durasi);
				dataSite.TotalPerItem = double.Parse(totalprice);
				JsonConvert.SerializeObject(dataSite);

				CheckDateAvailableInputModel checkDate = new CheckDateAvailableInputModel();
				checkDate.SiteID = Guid.Parse(siteID);
				checkDate.SiteItemID = Guid.Parse(siteItemID);
				checkDate.StartDate = DateTime.Parse(startDate);
				checkDate.EndDate = DateTime.Parse(endDate);
				using (var clientCheck = new HttpClient())
				{
					clientCheck.BaseAddress = new Uri(BaseAPI + "Site/");
					//HTTP POST
					var postTaskCheck = clientCheck.PostAsJsonAsync<CheckDateAvailableInputModel>("CheckAvailableSite", checkDate);
					postTaskCheck.Wait();

					var resultCheck = postTaskCheck.Result;
					if (resultCheck.IsSuccessStatusCode)
					{
						var contentcheck = resultCheck.Content.ReadAsStringAsync();
						CheckDateAvailableResponseModel resutlcheck = Newtonsoft.Json.JsonConvert.DeserializeObject<CheckDateAvailableResponseModel>(contentcheck.Result);
						if (resutlcheck.data.CanBook == true)
						{
							using (var client = new HttpClient())
							{
								client.BaseAddress = new Uri(BaseAPI + "Site/");
								//HTTP POST
								var postTask = client.PostAsJsonAsync<AddBookInputModel>("AddToCart", dataSite);
								postTask.Wait();

								var result = postTask.Result;
								if (result.IsSuccessStatusCode)
								{
									var content = result.Content.ReadAsStringAsync();
									AddBookResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<AddBookResponseModel>(content.Result);
									OutPutData = resutl.data;
								}
								else
								{
									TempData["CustomError"] = "Gagal menambah data. Mohon hubungi admin.";
								}
							}
						}
						else
						{
							TempData["CustomError"] = "Gagal menambah data, tanggal tidak terserdia. Mohon hubungi admin.";
						}
					}
					else
					{
						TempData["CustomError"] = "Gagal menambah data. Mohon hubungi admin.";
					}
				}
				return Json(OutPutData);
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
		#endregion
	}
}
