using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Billboard360.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Billboard360.Web.Controllers
{
    public class HomeController : Controller
    {
		const string SessionKeyID = "_ID";
		const string SessionKeyName = "_NAME";
		const string SessionKeyRole = "_ROLE";
		const string SessionKeySite = "_SITE";
		const string SessionKeyDomain = "_Domain";
		const string Loginfrom = "_LoginF";
		string BaseAPI = "";
		string Domain = "";
		private ICompositeViewEngine _viewEngine;
		private readonly IConfiguration _config;
		private readonly ITempDataProvider _tempDataProvider;
		private readonly IServiceProvider _serviceProvider;
		public HomeController(IConfiguration config, ICompositeViewEngine viewEngine,
			ITempDataProvider tempDataProvider,
			IServiceProvider serviceProvider)
		{
			_config = config;
			BaseAPI = _config.GetValue<string>("APIBaseUrl");
			Domain = _config.GetValue<string>("Domain");
			_viewEngine = viewEngine;
			_tempDataProvider = tempDataProvider;
			_serviceProvider = serviceProvider;
		}

		public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Index(string tipeBillboard, string provinsi, string kota, double minharga = 0, double maxharga = 0)
        {
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				HttpContext.Session.SetString(SessionKeyDomain, Domain);
				var Mapsdata = new ForMaps();
				var cities = new List<City>();
				var BillBoards = new List<BillboardDetail>();
				//IEnumerable<SiteListOutputModel> sites = null;
				List<SiteListOutputModel> sites = new List<SiteListOutputModel>();

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Site/");
					FilterBillboard filter = new FilterBillboard();
					if (HttpContext.Session.GetString(SessionKeyRole) == null || HttpContext.Session.GetString(SessionKeyRole) == "" || HttpContext.Session.GetString(SessionKeyRole) == "ADM")
					{
						filter.UserID = Guid.Empty;
					}
					else
					{
						filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
					}

					filter.TypeBillboard = tipeBillboard != null ? tipeBillboard : "";
					filter.Province = provinsi != null && provinsi.Length > 0 ? provinsi.Split("|")[1] : "";
					filter.City = kota != null ? kota : "";
					filter.Latitude = "";
					filter.Longitude = "";
					filter.MinimumPrice = minharga;
					filter.MaximumPrice = maxharga;
					filter.showWithDisabledSite = false;
					filter.FilterDataTableValue = "";
					filter.PageNumber = 1;
					filter.PageSize = 250;
					var responseTask = client.PostAsJsonAsync<FilterBillboard>("GetListSite", filter);
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						SiteListResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteListResponseModel>(content.Result);

						sites = resutl.data;
						foreach (var p in sites)
						{
							var city = new City();
							var BillBoard = new BillboardDetail();
							if (p.NoBillboard != null && p.Latitude != null && p.Longitude != null && p.NoBillboard != "" && p.Latitude != "" && p.Longitude != "")
							{
								city.Title = p.NoBillboard;
								city.Lat = double.Parse(p.Latitude, System.Globalization.CultureInfo.InvariantCulture);
								city.Lng = double.Parse(p.Longitude, System.Globalization.CultureInfo.InvariantCulture);
								city.Kota = p.Kota;
								city.Prov = p.Provinsi;
								city.HorV = p.HorV;
								city.Tipe = p.Tipe;
								city.Rate = p.RateScoreAverage;
								city.Price = p.HargaPerhari.ToString("N");
								city.Image = "../image/NoImage.jpg";

								BillBoard.SiteID = p.SiteID.ToString();
								BillBoard.NoBillBoard = p.NoBillboard;
								BillBoard.Kota = p.Kota;
								BillBoard.Cab = p.Cabang;
								BillBoard.HorV = p.HorV;
								BillBoard.Tipe = p.Tipe;
								BillBoard.Lat = double.Parse(p.Latitude, System.Globalization.CultureInfo.InvariantCulture);
								BillBoard.Lng = double.Parse(p.Longitude, System.Globalization.CultureInfo.InvariantCulture);
								BillBoard.Pjg = 0;
								BillBoard.Lbr = 0;
								BillBoard.Hawl = 0;
								BillBoard.Hahr = 0;
								BillBoard.Img = "-";
								BillBoard.RateScoreAvg = p.RateScoreAverage;
								BillBoard.Alamat = p.Alamat;

								if (p.ImageHeader != null && p.ImageHeader.Contains("/Upload")) {
									BillBoard.Img = p.ImageHeader;
									city.Image = p.ImageHeader;
								}

								if (p.SiteItem != null && p.SiteItem.Count > 0)
								{
									BillBoard.SiteItemID = p.SiteItem[0].SiteItemID.ToString();
									BillBoard.Pjg = p.SiteItem[0].Panjang;
									BillBoard.Lbr = p.SiteItem[0].Lebar;
									if (p.SiteItem[0].Price != null && p.SiteItem[0].Price.Count > 0)
									{
										BillBoard.Hawl = p.SiteItem[0].Price[0].HargaAwal;
										BillBoard.Hahr = p.SiteItem[0].Price[0].HargaAkhir;
									}
									//if (p.SiteItem[0].Image != null && p.SiteItem[0].Image.Count > 0)
									//{
									//	BillBoard.Img = p.SiteItem[0].Image[0].Image;
									//	city.Image = p.SiteItem[0].Image[0].Image;
									//}
								}
							}

							cities.Add(city);
							BillBoards.Add(BillBoard);
						}
						Mapsdata.Cities = cities;
						Mapsdata.BillBoard = BillBoards;

						#region Old
						//filter.PageSize = resutl.TotalPages;
						//var reresponse = client.PostAsJsonAsync<FilterBillboard>("GetListSite", filter);
						//reresponse.Wait();
						//var reresult = reresponse.Result;
						//if (reresult.IsSuccessStatusCode)
						//{
						//	var recontent = reresult.Content.ReadAsStringAsync();
						//	SiteListResponseModel fnlresult = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteListResponseModel>(recontent.Result);

						//	sites = fnlresult.data;
						//	foreach (var p in sites)
						//	{
						//		var city = new City();
						//		var BillBoard = new BillboardDetail();
						//		if (p.NoBillboard != null && p.Latitude != null && p.Longitude != null && p.NoBillboard != "" && p.Latitude != "" && p.Longitude != "")
						//		{
						//			city.Title = p.NoBillboard;
						//			city.Lat = double.Parse(p.Latitude, System.Globalization.CultureInfo.InvariantCulture);
						//			city.Lng = double.Parse(p.Longitude, System.Globalization.CultureInfo.InvariantCulture);

						//			BillBoard.SiteID = p.SiteID.ToString();
						//			BillBoard.NoBillBoard = p.NoBillboard;
						//			BillBoard.Kota = p.Kota;
						//			BillBoard.Cab = p.Cabang;
						//			BillBoard.HorV = p.HorV;
						//			BillBoard.Tipe = p.Tipe;
						//			BillBoard.Lat = double.Parse(p.Latitude, System.Globalization.CultureInfo.InvariantCulture);
						//			BillBoard.Lng = double.Parse(p.Longitude, System.Globalization.CultureInfo.InvariantCulture);
						//			BillBoard.Pjg = 0;
						//			BillBoard.Lbr = 0;
						//			BillBoard.Hawl = 0;
						//			BillBoard.Hahr = 0;
						//			BillBoard.Img = "-";
						//			BillBoard.RateScoreAvg = p.RateScoreAverage;
						//			BillBoard.Alamat = p.Alamat;

						//			if (p.SiteItem != null && p.SiteItem.Count > 0)
						//			{
						//				BillBoard.SiteItemID = p.SiteItem[0].SiteItemID.ToString();
						//				BillBoard.Pjg = p.SiteItem[0].Panjang;
						//				BillBoard.Lbr = p.SiteItem[0].Lebar;
						//				if (p.SiteItem[0].Price != null && p.SiteItem[0].Price.Count > 0)
						//				{
						//					BillBoard.Hawl = p.SiteItem[0].Price[0].HargaAwal;
						//					BillBoard.Hahr = p.SiteItem[0].Price[0].HargaAkhir;
						//				}
						//				if (p.SiteItem[0].Image != null && p.SiteItem[0].Image.Count > 0)
						//				{
						//					BillBoard.Img = p.SiteItem[0].Image[0].Image;
						//				}
						//			}
						//		}

						//		cities.Add(city);
						//		BillBoards.Add(BillBoard);
						//	}
						//	Mapsdata.Cities = cities;
						//	Mapsdata.BillBoard = BillBoards;
						//}
						#endregion
					}
					else //web api sent error response 
					{
						//cities.Add(new City() { Title = "Paris", Lat = 48.855901, Lng = 2.349272 });
						//cities.Add(new City() { Title = "Berlin", Lat = 52.520413, Lng = 13.402794 });
						//cities.Add(new City() { Title = "Rome", Lat = 41.907074, Lng = 12.498474 });
						cities.Add(new City() { Title = "Paris", Lat = 48.855901, Lng = 2.349272, Kota = "", Prov = "", HorV = "", Tipe = "", Rate = 0, Price = "0", Image = "~/image/NoImage.jpg" });
						cities.Add(new City() { Title = "Berlin", Lat = 52.520413, Lng = 13.402794, Kota = "", Prov = "", HorV = "", Tipe = "", Rate = 0, Price = "0", Image = "~/image/NoImage.jpg" });
						cities.Add(new City() { Title = "Rome", Lat = 41.907074, Lng = 12.498474, Kota = "", Prov = "", HorV = "", Tipe = "", Rate = 0, Price = "0", Image = "~/image/NoImage.jpg" });
						Mapsdata.Cities = cities;
						Mapsdata.BillBoard = BillBoards;
						//log response status here..
						//sites = Enumerable.Empty<SiteListOutputModel>();

						ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
					}
				}
				return View(Mapsdata);
			}
			else
			{
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

		public IActionResult ToDetail(string NoBillboard)
		{
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				HttpContext.Session.SetString(SessionKeyDomain, Domain);
				List<SiteListOutputModel> sites = new List<SiteListOutputModel>();

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Site/");
					FilterBillboard filter = new FilterBillboard();
					filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
					filter.Latitude = "";
					filter.Longitude = "";
					filter.MinimumPrice = 0;
					filter.MaximumPrice = 0;
					filter.showWithDisabledSite = false;
					filter.isFilterDataTable = true;
					filter.FilterDataTableValue = NoBillboard;
					filter.PageNumber = 1;
					filter.PageSize = 250;
					var responseTask = client.PostAsJsonAsync<FilterBillboard>("GetListSite", filter);
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						SiteListResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteListResponseModel>(content.Result);

						sites = resutl.data;
						return RedirectToAction("ViewDetail", "Site", new { @id = sites.Select(x => x.SiteID).FirstOrDefault() });
					}
					else //web api sent error response 
					{
						ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
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
			else
			{
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

		public ActionResult PartialList(ForMaps Model)
		{
			return PartialView(Model);
		}

		[HttpPost]
		public async Task<ActionResult> SearchFilter(string Data, int Pages)
		{
			int BlockSize = 250;
			int totalData = 0;
			var Mapsdata = new ForMaps();
			var cities = new List<City>();
			var BillBoards = new List<BillboardDetail>();
			List<SiteListOutputModel> sites = new List<SiteListOutputModel>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				FilterBillboard filter = new FilterBillboard();
				if (HttpContext.Session.GetString(SessionKeyRole) == null || HttpContext.Session.GetString(SessionKeyRole) == "" || HttpContext.Session.GetString(SessionKeyRole) == "ADM")
				{
					filter.UserID = Guid.Empty;
				}
				else
				{
					filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
				}

				filter.Latitude = "";
				filter.Longitude = "";
				filter.MinimumPrice = 0;
				filter.MaximumPrice = 0;
				filter.showWithDisabledSite = false;
				filter.isFilterDataTable = Data == "" ? false : true;
				filter.FilterDataTableValue = Data;
				filter.PageNumber = 1;
				filter.PageSize = 100;
				var responseTask = client.PostAsJsonAsync<FilterBillboard>("GetListSite", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					SiteListResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteListResponseModel>(content.Result);

					totalData = resutl.TotalData;
					sites = resutl.data;
					foreach (var p in sites)
					{
						var city = new City();
						var BillBoard = new BillboardDetail();
						if (p.NoBillboard != null && p.Latitude != null && p.Longitude != null && p.NoBillboard != "" && p.Latitude != "" && p.Longitude != "")
						{
							city.Title = p.NoBillboard;
							city.Lat = double.Parse(p.Latitude, System.Globalization.CultureInfo.InvariantCulture);
							city.Lng = double.Parse(p.Longitude, System.Globalization.CultureInfo.InvariantCulture);
							city.Kota = p.Kota;
							city.Prov = p.Provinsi;
							city.HorV = p.HorV;
							city.Tipe = p.Tipe;
							city.Rate = p.RateScoreAverage;
							city.Price = p.HargaPerhari.ToString("N");
							city.Image = "../image/NoImage.jpg";

							BillBoard.SiteID = p.SiteID.ToString();
							BillBoard.NoBillBoard = p.NoBillboard;
							BillBoard.Kota = p.Kota;
							BillBoard.Cab = p.Cabang;
							BillBoard.HorV = p.HorV;
							BillBoard.Tipe = p.Tipe;
							BillBoard.Lat = double.Parse(p.Latitude, System.Globalization.CultureInfo.InvariantCulture);
							BillBoard.Lng = double.Parse(p.Longitude, System.Globalization.CultureInfo.InvariantCulture);
							BillBoard.Pjg = 0;
							BillBoard.Lbr = 0;
							BillBoard.Hawl = 0;
							BillBoard.Hahr = 0;
							BillBoard.Img = "-";
							BillBoard.RateScoreAvg = p.RateScoreAverage;
							BillBoard.Alamat = p.Alamat;

							if (p.ImageHeader != null && p.ImageHeader.Contains("/Upload")) {
								BillBoard.Img = p.ImageHeader;
								city.Image = p.ImageHeader;
							}

							if (p.SiteItem != null && p.SiteItem.Count > 0)
							{
								BillBoard.SiteItemID = p.SiteItem[0].SiteItemID.ToString();
								BillBoard.Pjg = p.SiteItem[0].Panjang;
								BillBoard.Lbr = p.SiteItem[0].Lebar;
								if (p.SiteItem[0].Price != null && p.SiteItem[0].Price.Count > 0)
								{
									BillBoard.Hawl = p.SiteItem[0].Price[0].HargaAwal;
									BillBoard.Hahr = p.SiteItem[0].Price[0].HargaAkhir;
								}
								//if (p.SiteItem[0].Image != null && p.SiteItem[0].Image.Count > 0)
								//{
								//	BillBoard.Img = p.SiteItem[0].Image[0].Image;
								//}
							}
						}

						cities.Add(city);
						BillBoards.Add(BillBoard);
					}
					Mapsdata.Cities = cities;
					Mapsdata.BillBoard = BillBoards;
				}
			}

			JsonModel jsonModel = new JsonModel();
			jsonModel.NoMoreData = (totalData / BlockSize) == 1;
			if (Pages == 1)
			{
				jsonModel.HTMLString = await RenderPartialViewToString("PartialList", Mapsdata);
			}
			else if (Pages == 2)
			{
				jsonModel.HTMLString = await RenderPartialViewToString("PartialCompare", Mapsdata);
			}

			return Json(jsonModel);
		}

		[HttpPost]
		public async Task<ActionResult> InfinateScrollAsync(int BlockNumber, int Pages, string tipeBillboard = "", string provinsi = "", string kota = "", double minharga = 0, double maxharga = 0)
		{
			int BlockSize = 250;
			int totalData = 0;
			var Mapsdata = new ForMaps();
			var cities = new List<City>();
			var BillBoards = new List<BillboardDetail>();
			//IEnumerable<SiteListOutputModel> sites = null;
			List<SiteListOutputModel> sites = new List<SiteListOutputModel>();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				FilterBillboard filter = new FilterBillboard();
				if (HttpContext.Session.GetString(SessionKeyRole) == null || HttpContext.Session.GetString(SessionKeyRole) == "" || HttpContext.Session.GetString(SessionKeyRole) == "ADM")
				{
					filter.UserID = Guid.Empty;
				}
				else
				{
					filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
				}

				filter.TypeBillboard = tipeBillboard != null ? tipeBillboard : "";
				filter.Province = provinsi != null && provinsi.Split("|").Length > 1 ? provinsi.Split("|")[1] : "";
				filter.City = kota != null ? kota : "";
				filter.Latitude = "";
				filter.Longitude = "";
				filter.MinimumPrice = minharga;
				filter.MaximumPrice = maxharga;
				filter.showWithDisabledSite = false;
				filter.FilterDataTableValue = "";
				filter.PageNumber = BlockNumber;
				filter.PageSize = 100;
				var responseTask = client.PostAsJsonAsync<FilterBillboard>("GetListSite", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					SiteListResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteListResponseModel>(content.Result);

					totalData = resutl.TotalData;
					sites = resutl.data;
					foreach (var p in sites)
					{
						var city = new City();
						var BillBoard = new BillboardDetail();
						if (p.NoBillboard != null && p.Latitude != null && p.Longitude != null && p.NoBillboard != "" && p.Latitude != "" && p.Longitude != "")
						{
							city.Title = p.NoBillboard;
							city.Lat = double.Parse(p.Latitude, System.Globalization.CultureInfo.InvariantCulture);
							city.Lng = double.Parse(p.Longitude, System.Globalization.CultureInfo.InvariantCulture);
							city.Kota = p.Kota;
							city.Prov = p.Provinsi;
							city.HorV = p.HorV;
							city.Tipe = p.Tipe;
							city.Rate = p.RateScoreAverage;
							city.Price = p.HargaPerhari.ToString("N");
							city.Image = "../image/NoImage.jpg";

							BillBoard.SiteID = p.SiteID.ToString();
							BillBoard.NoBillBoard = p.NoBillboard;
							BillBoard.Kota = p.Kota;
							BillBoard.Cab = p.Cabang;
							BillBoard.HorV = p.HorV;
							BillBoard.Tipe = p.Tipe;
							BillBoard.Lat = double.Parse(p.Latitude, System.Globalization.CultureInfo.InvariantCulture);
							BillBoard.Lng = double.Parse(p.Longitude, System.Globalization.CultureInfo.InvariantCulture);
							BillBoard.Pjg = 0;
							BillBoard.Lbr = 0;
							BillBoard.Hawl = 0;
							BillBoard.Hahr = 0;
							BillBoard.Img = "-";
							BillBoard.RateScoreAvg = p.RateScoreAverage;
							BillBoard.Alamat = p.Alamat;

							if (p.ImageHeader != null && p.ImageHeader.Contains("/Upload")) {
								BillBoard.Img = p.ImageHeader;
								city.Image = p.ImageHeader;
							}

							if (p.SiteItem != null && p.SiteItem.Count > 0)
							{
								BillBoard.SiteItemID = p.SiteItem[0].SiteItemID.ToString();
								BillBoard.Pjg = p.SiteItem[0].Panjang;
								BillBoard.Lbr = p.SiteItem[0].Lebar;
								if (p.SiteItem[0].Price != null && p.SiteItem[0].Price.Count > 0)
								{
									BillBoard.Hawl = p.SiteItem[0].Price[0].HargaAwal;
									BillBoard.Hahr = p.SiteItem[0].Price[0].HargaAkhir;
								}
								//if (p.SiteItem[0].Image != null && p.SiteItem[0].Image.Count > 0)
								//{
								//	BillBoard.Img = p.SiteItem[0].Image[0].Image;
								//}
							}
						}

						cities.Add(city);
						BillBoards.Add(BillBoard);
					}
					Mapsdata.Cities = cities;
					Mapsdata.BillBoard = BillBoards;
				}
			}

			JsonModel jsonModel = new JsonModel();
			jsonModel.NoMoreData = (totalData/BlockSize) == BlockNumber;
			if (Pages == 1)
			{
				jsonModel.HTMLString = await RenderPartialViewToString("PartialList", Mapsdata);
			}
			else if(Pages == 2)
			{
				jsonModel.HTMLString = await RenderPartialViewToString("PartialCompare", Mapsdata);
			}

			return Json(jsonModel);
		}

		private async Task<string> RenderPartialViewToString(string viewName, object model)
		{
			var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };

			if (string.IsNullOrEmpty(viewName))
				viewName = ControllerContext.ActionDescriptor.ActionName;

			ViewData.Model = model;

			using (var writer = new StringWriter())
			{
				ViewEngineResult viewResult = _viewEngine.FindView(ControllerContext, viewName, false);

				if (viewResult.View == null)
				{
					throw new ArgumentNullException($"{viewName} does not match any available view");
				}

				var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
				{
					Model = model
				};

				var viewContext = new ViewContext(
					ControllerContext,
					viewResult.View,
					viewDictionary,
					new TempDataDictionary(ControllerContext.HttpContext, _tempDataProvider),
					writer,
					new HtmlHelperOptions()
				);

				await viewResult.View.RenderAsync(viewContext);

				return writer.GetStringBuilder().ToString();
			}
		}

		[HttpPost]
        public JsonResult GetAnswer(string question)
        {
            int index = _rnd.Next(_db.Count);
            var answer = _db[index];
            return Json(answer);
        }

        private static Random _rnd = new Random();

        private static List<string> _db = new List<string> { "Yes", "No", "Definitely, yes", "I don't know", "Looks like, yes" };

		public IActionResult ErrorHandling()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
