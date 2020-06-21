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
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Billboard360.Web.Controllers
{
	public class BookingController : Controller
	{
		const string SessionKeyID = "_ID";
		const string SessionKeyName = "_NAME";
		const string SessionKeyFName = "_FNAME";
		const string SessionKeyLName = "_LNAME";
		const string SessionKeyRole = "_ROLE";
		const string SessionKeyDomain = "_Domain";
		const string SessionKeyPrevSite = "_PrevSite";
		const string SessionKeyBookID = "_BookID";
		const string Loginfrom = "_LoginF";
		string BaseAPI = "";
		string Domain = "";
		private readonly IConfiguration _config;
		private ICompositeViewEngine _viewEngine;
		private readonly ITempDataProvider _tempDataProvider;
		private readonly IServiceProvider _serviceProvider;
		public BookingController(IConfiguration config, ICompositeViewEngine viewEngine,
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

		// GET: /<controller>/
		public IActionResult Index()
		{
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				List<ViewResultBookModel> OutPutData = null;
				ViewBookInputModel filter = new ViewBookInputModel();
				filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
				filter.Status = string.Empty;
				filter.PageSize = 1000;
				filter.PageNumber = 1;
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Site/");
					var responseTask = client.PostAsJsonAsync<ViewBookInputModel>("ViewBooked", filter);
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						ViewArrayBookResponse resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewArrayBookResponse>(content.Result);
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

		public IActionResult BookDetail(string id)
		{
			HttpContext.Session.SetString(SessionKeyPrevSite, "");
			HttpContext.Session.SetString(SessionKeyBookID, id);
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				List<ViewResultBookModel> OutPutData = null;
				ViewBookInputModel filter = new ViewBookInputModel();
				filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
				filter.BookID = Guid.Parse(id);
				filter.Status = string.Empty;
				filter.PageSize = 1000;
				filter.PageNumber = 1;
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Site/");
					var responseTask = client.PostAsJsonAsync<ViewBookInputModel>("ViewBooked", filter);
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						ViewArrayBookResponse resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewArrayBookResponse>(content.Result);
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

		public IActionResult BookDetailCheck(string order_id, string status_code, string transaction_status)
		{
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				if (order_id != "" && order_id != null && status_code == "200" && transaction_status == "capture")
				{
					return RedirectToAction("Index", "Booking");
				}
				else
				{
					return RedirectToAction("Index", "Home");
				}
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

		public IActionResult AddToBooked()
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
						AddToBookListInputModel bookData = new AddToBookListInputModel();
						bookData.isFromCart = true;
						bookData.Data = new List<AddBookInputModel>();
						foreach (var book in OutPutData)
						{
							AddBookInputModel Data = new AddBookInputModel();
							Data.SitePriceID = book.Site.Detail.Item.Price.PriceID;
							Data.SiteID = book.Site.Detail.Item.SiteID;
							Data.SiteItemID = book.Site.Detail.Item.SiteItemID;
							Data.UserID = book.UserID;
							Data.StartDate = book.StartDate;
							Data.EndDate = book.EndDate;
							Data.Qty = book.Qty;
							Data.Price = book.Price;
							Data.TotalPerItem = book.TotalHargaPerItem;
							Data.CartID = book.CartID;
							bookData.Data.Add(Data);
						}

						using (var clientBook = new HttpClient())
						{
							clientBook.BaseAddress = new Uri(BaseAPI + "Site/");
							var responseTaskbook = clientBook.PostAsJsonAsync<AddToBookListInputModel>("AddToBooked", bookData);
							responseTaskbook.Wait();

							var resultbook = responseTaskbook.Result;
							if (resultbook.IsSuccessStatusCode)
							{
								return RedirectToAction("Index", "Booking");
							}
							else //web api sent error response 
							{
								//log response status here..
								ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
								return RedirectToAction("Keranjang", "Wish");
							}
						}
					}
					else //web api sent error response 
					{
						//log response status here..
						ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
						return RedirectToAction("Keranjang", "Wish");
					}
				}
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

		#region BookPopUp
		[HttpPost]
		public ActionResult AddtoBook(string siteID, string sitePriceID, string siteItemID, string price, string durasi, string totalprice, string startDate, string endDate)
		{
			AddBookOutputModel OutPutData = new AddBookOutputModel();
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				AddToBookListInputModel bookData = new AddToBookListInputModel();
				bookData.isFromCart = false;
				bookData.Data = new List<AddBookInputModel>();

				AddBookInputModel Data = new AddBookInputModel();
				Data.SitePriceID = Guid.Parse(sitePriceID);
				Data.SiteID = Guid.Parse(siteID);
				Data.SiteItemID = Guid.Parse(siteItemID);
				Data.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
				Data.StartDate = DateTime.Parse(startDate);
				Data.EndDate = DateTime.Parse(endDate);
				Data.Qty = int.Parse(durasi);
				Data.Price = double.Parse(price);
				Data.TotalPerItem = double.Parse(totalprice);
				Data.CartID = Guid.Empty;
				bookData.Data.Add(Data);
				JsonConvert.SerializeObject(bookData);

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
								var postTask = client.PostAsJsonAsync<AddToBookListInputModel>("AddToBooked", bookData);
								postTask.Wait();

								var result = postTask.Result;
								if (result.IsSuccessStatusCode)
								{
									return RedirectToAction("Index", "Booking");
									//var content = result.Content.ReadAsStringAsync();
									//AddBookResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<AddBookResponseModel>(content.Result);
									//OutPutData = resutl.data;
								}
								else
								{
									TempData["CustomError"] = "Fail to add data. Please contact administrator.";
								}
							}
						}
						else
						{
							TempData["CustomError"] = "Fail to add data, date not available. Please contact administrator.";
						}
					}
					else
					{
						TempData["CustomError"] = "Fail to add data. Please contact administrator.";
					}
				}
				return Json(OutPutData);
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
		public ActionResult ReportSite(string siteID, string desc)
		{
			AddReportProblemOutputModel OutPutData = new AddReportProblemOutputModel();
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				AddReportProblemInputModel siteData = new AddReportProblemInputModel();
				siteData.isToSPV = true;
				siteData.SiteID = Guid.Parse(siteID);
				siteData.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
				siteData.Desc = desc;

				JsonConvert.SerializeObject(siteData);
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "MediaBuyer/");
					//HTTP POST
					var postTask = client.PostAsJsonAsync<AddReportProblemInputModel>("ReportToSPV", siteData);
					postTask.Wait();

					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						AddReportProblemResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<AddReportProblemResponseModel>(content.Result);
						OutPutData = resutl.data;
					}
					else
					{
						TempData["CustomError"] = "Fail to add data. Please contact administrator.";
					}
				}
				return Json(OutPutData);
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
		#endregion

		#region Payment
		[HttpPost]
		public ActionResult ProceedPayment(string bookID, double hargaAmt, double discAmt, double totalAmt, double payAmt)
		{
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				MidTransTransactionResponseModel OutPutData = null;
				MidTransChargeInputModel filter = new MidTransChargeInputModel();
				PurchaseOutputModel OutPutPurch = null;
				PurchaseInputModel inputPurch = new PurchaseInputModel();
				inputPurch.SubTotal = hargaAmt;
				inputPurch.Diskon = discAmt;
				inputPurch.GrandTotal = totalAmt;
				inputPurch.UangMuka = payAmt;
				inputPurch.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
				inputPurch.BookID = new string[1];
				inputPurch.BookID[0] = bookID;
				using (var clientPurch = new HttpClient())
				{
					clientPurch.BaseAddress = new Uri(BaseAPI + "MediaBuyer/");
					var responseTaskPurch = clientPurch.PostAsJsonAsync<PurchaseInputModel>("ProcessToPurchase", inputPurch);
					responseTaskPurch.Wait();

					var resultPurch = responseTaskPurch.Result;
					if (resultPurch.IsSuccessStatusCode)
					{
						var contentPurch = resultPurch.Content.ReadAsStringAsync();
						PurchaseResponseModel resutlPurch = Newtonsoft.Json.JsonConvert.DeserializeObject<PurchaseResponseModel>(contentPurch.Result);
						OutPutPurch = resutlPurch.data;
						if (OutPutPurch != null)
						{
							filter.customer_details = new CustomerDetails();
							filter.customer_details.billing_address = new BillingAddress();
							filter.customer_details.shipping_address = new ShippingAddress();
							filter.transaction_details = new TransactionDetails();
							filter.transaction_details.currency = "IDR";
							filter.transaction_details.order_id = OutPutPurch.InvoiceNo;
							filter.transaction_details.gross_amount = payAmt;
							filter.user_id = HttpContext.Session.GetString(SessionKeyID);
							using (var client = new HttpClient())
							{
								client.BaseAddress = new Uri(BaseAPI.Replace("api/", ""));
								var responseTask = client.PostAsJsonAsync<MidTransChargeInputModel>("charge", filter);
								responseTask.Wait();

								var result = responseTask.Result;
								if (result.IsSuccessStatusCode)
								{
									var content = result.Content.ReadAsStringAsync();
									MidTransTransactionResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<MidTransTransactionResponseModel>(content.Result);
									OutPutData = resutl;
								}
								else //web api sent error response 
								{
									//log response status here..
									ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
								}
							}
						}
					}
					else //web api sent error response 
					{
						//log response status here..
						ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
					}
				}

				return Json(OutPutData);
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
		#endregion

		#region ChangeSite
		public IActionResult ChangeList(string tipeBillboard, string provinsi, string kota, string prevSite, double minharga = 0, double maxharga = 0)
		{
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "" && prevSite != null && prevSite != "")
			{
				HttpContext.Session.SetString(SessionKeyPrevSite, prevSite);
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

								if (p.SiteItem != null && p.SiteItem.Count > 0)
								{
									BillBoard.SiteItemID = p.SiteItem[0].SiteItemID.ToString();
									BillBoard.Pjg = p.SiteItem[0].Panjang;
									BillBoard.Lbr = p.SiteItem[0].Lebar;
									if (p.SiteItem[0].Price != null && p.SiteItem[0].Price.Count > 0)
									{
										BillBoard.SitePriceID = p.SiteItem[0].Price[0].SitePriceID.ToString();
										BillBoard.Hawl = p.SiteItem[0].Price[0].HargaAwal;
										BillBoard.Hahr = p.SiteItem[0].Price[0].HargaAkhir;
									}
									if (p.SiteItem[0].Image != null && p.SiteItem[0].Image.Count > 0)
									{
										BillBoard.Img = p.SiteItem[0].Image[0].Image;
										city.Image = p.SiteItem[0].Image[0].Image;
									}
								}
							}

							cities.Add(city);
							BillBoards.Add(BillBoard);
						}
						Mapsdata.Cities = cities;
						Mapsdata.BillBoard = BillBoards;
					}
					else //web api sent error response 
					{
						cities.Add(new City() { Title = "Paris", Lat = 48.855901, Lng = 2.349272, Kota = "", Prov = "", HorV = "", Tipe = "", Rate = 0, Price = "0", Image = "~/image/NoImage.jpg" });
						cities.Add(new City() { Title = "Berlin", Lat = 52.520413, Lng = 13.402794, Kota = "", Prov = "", HorV = "", Tipe = "", Rate = 0, Price = "0", Image = "~/image/NoImage.jpg" });
						cities.Add(new City() { Title = "Rome", Lat = 41.907074, Lng = 12.498474, Kota = "", Prov = "", HorV = "", Tipe = "", Rate = 0, Price = "0", Image = "~/image/NoImage.jpg" });
						Mapsdata.Cities = cities;
						Mapsdata.BillBoard = BillBoards;

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

		public ActionResult PartialList(ForMaps Model)
		{
			return PartialView(Model);
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
								if (p.SiteItem[0].Image != null && p.SiteItem[0].Image.Count > 0)
								{
									BillBoard.Img = p.SiteItem[0].Image[0].Image;
								}
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
			jsonModel.NoMoreData = (totalData / BlockSize) == BlockNumber;
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
		public ActionResult AddItemToCurrentBook(string siteID, string sitePriceID, string siteItemID, string price, string durasi, string totalprice, string startDate, string endDate, string prevSite)
		{
			AddBookOutputModel OutPutData = new AddBookOutputModel();
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				AddItemToCurrentBookListInputModel bookData = new AddItemToCurrentBookListInputModel();
				bookData.BookID = Guid.Parse(HttpContext.Session.GetString(SessionKeyBookID));
				bookData.BookDetailIDForDeleted = Guid.Parse(prevSite);
				bookData.Data = new List<AddItemToCurrentBookInputModel>();

				AddItemToCurrentBookInputModel Data = new AddItemToCurrentBookInputModel();
				Data.SitePriceID = Guid.Parse(sitePriceID);
				Data.SiteID = Guid.Parse(siteID);
				Data.SiteItemID = Guid.Parse(siteItemID);
				Data.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
				Data.StartDate = DateTime.Parse(startDate);
				Data.EndDate = DateTime.Parse(endDate);
				Data.Qty = int.Parse(durasi);
				Data.Price = double.Parse(price);
				Data.TotalPerItem = double.Parse(totalprice);
				bookData.Data.Add(Data);

				JsonConvert.SerializeObject(bookData);
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Site/");
					//HTTP POST
					var postTask = client.PostAsJsonAsync<AddItemToCurrentBookListInputModel>("AddItemToCurrentBook", bookData);
					postTask.Wait();

					var result = postTask.Result;
					if (result.IsSuccessStatusCode)
					{
						return RedirectToAction("Index", "Booking");
					}
					else
					{
						TempData["CustomError"] = "Fail to add data. Please contact administrator.";
					}
				}
				return Json(OutPutData);
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
		#endregion

		public IActionResult DeleteItemFromBook(string BookDetailID)
		{
			List<Guid> BookIDs = new List<Guid>();
			BookIDs.Add(Guid.Parse(BookDetailID));
			DeleteItemBookOutputModel OutPutData = new DeleteItemBookOutputModel();
			DeleteItemBookInputModel filter = new DeleteItemBookInputModel();
			filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			filter.BookID = BookIDs;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				var responseTask = client.PostAsJsonAsync<DeleteItemBookInputModel>("DeleteItemFromBooked", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					DeleteItemBookResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<DeleteItemBookResponseModel>(content.Result);
					OutPutData = resutl.data;
					return RedirectToAction("BookDetail", "Booking", new { @id = BookDetailID });
				}
				else //web api sent error response 
				{
					//log response status here..
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return RedirectToAction("Index", "Home");
		}

		public IActionResult SyaratKetentuan()
		{
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
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
	}
}
