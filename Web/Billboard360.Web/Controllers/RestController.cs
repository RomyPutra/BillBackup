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
using System.Text;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Billboard360.Web.Controllers
{
	public class RestController : Controller
	{
		const string SessionKeyID = "_ID";
		const string SessionKeyName = "_NAME";
		const string SessionKeyFName = "_FNAME";
		const string SessionKeyLName = "_LNAME";
		const string SessionKeyRole = "_ROLE";
		const string SessionKeyProv = "_PROV";
		string BaseAPI = "";
		string Domain = "";
		private readonly IConfiguration _config;
		public RestController(IConfiguration config)
		{
			_config = config;
			BaseAPI = _config.GetValue<string>("APIBaseUrl");
			Domain = _config.GetValue<string>("Domain");
		}

		public IActionResult Index()
		{
			return View();
		}

		#region Admin
		public IActionResult GetUserListBankAccount()
		{
			IEnumerable<ListBankAccountOutputModel> OutPutData = null;

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Admin/");
				var responseTask = client.PostAsync("GetListUserBankAccount", null);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					ListBankAccountResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ListBankAccountResponseModel>(content.Result);
					OutPutData = resutl.data;
				}
				else //web api sent error response 
				{
					//log response status here..
					OutPutData = Enumerable.Empty<ListBankAccountOutputModel>();
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return Json(OutPutData);
		}

		public IActionResult GetVRPSL()
		{
			IEnumerable<ReportProblemListOutputModel> OutPutData = null;

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Admin/");
				var responseTask = client.PostAsync("GetListBank", null);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					ReportProblemListResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportProblemListResponseModel>(content.Result);
					OutPutData = resutl.data;
				}
				else //web api sent error response 
				{
					//log response status here..
					OutPutData = Enumerable.Empty<ReportProblemListOutputModel>();
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return Json(OutPutData);
		}
		#endregion

		#region Bank
		public IActionResult ChangeStatusBank()
		{
			InActiveBankOutputModel OutPutData = new InActiveBankOutputModel();
			InActiveBankInputModel filter = new InActiveBankInputModel();
			filter.BankID = Guid.Parse("");
			filter.Value = true;
			filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Bank/");
				var responseTask = client.PostAsJsonAsync<InActiveBankInputModel>("ChangeStatusBank", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					InActiveBankResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<InActiveBankResponseModel>(content.Result);
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
		#endregion

		#region Base
		public IActionResult GetRoleDropDown()
		{
			IEnumerable<RoleOutputModel> OutPutData = null;
			IsRoleInputModel filter = new IsRoleInputModel();
			filter.IsSupervisor = true;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Base/");
				var responseTask = client.PostAsJsonAsync<IsRoleInputModel>("GetRoleDropDown", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					RoleResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<RoleResponseModel>(content.Result);
					OutPutData = resutl.data;
				}
				else //web api sent error response 
				{
					//log response status here..
					OutPutData = Enumerable.Empty<RoleOutputModel>();
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return Json(OutPutData);
		}
		#endregion

		#region DashBoard
		public IActionResult GetRptPurchaseBook()
		{
			IEnumerable<ReportPurchaseAndBookOutputModel> OutPutData = null;
			ReportPurchaseAndBookInputModel filter = new ReportPurchaseAndBookInputModel();
			filter.PageSize = 1000;
			filter.PageNumber = 1;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Dashboard/");
				var responseTask = client.PostAsJsonAsync<ReportPurchaseAndBookInputModel>("ReportPurchaseAndBook", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					ReportPurchaseAndModelResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportPurchaseAndModelResponseModel>(content.Result);
					OutPutData = resutl.data;
				}
				else //web api sent error response 
				{
					//log response status here..
					OutPutData = Enumerable.Empty<ReportPurchaseAndBookOutputModel>();
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return Json(OutPutData);
		}

		public IActionResult GetRptSite()
		{
			IEnumerable<ReportSiteOutputModel> OutPutData = null;
			ReportSiteInputModel filter = new ReportSiteInputModel();
			filter.NoBillboard = "";
			filter.Kota = "";
			filter.Provinsi = "";
			filter.MediaOwnerID = Guid.Parse("");
			filter.IsBooked = true;
			filter.IsPurchased = true;
			filter.PageSize = 1000;
			filter.PageNumber = 1;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Dashboard/");
				var responseTask = client.PostAsJsonAsync<ReportSiteInputModel>("ReportSite", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					ReportSiteResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportSiteResponseModel>(content.Result);
					OutPutData = resutl.data;
				}
				else //web api sent error response 
				{
					//log response status here..
					OutPutData = Enumerable.Empty<ReportSiteOutputModel>();
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return Json(OutPutData);
		}

		public IActionResult ReportRekapBox()
		{
			RekapKotakEmpatOutputModel OutPutData = null;
			ReportKotaEmpatInputModel filter = new ReportKotaEmpatInputModel();
			if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
			{
				filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(BaseAPI + "Dashboard/");
					var responseTask = client.PostAsJsonAsync<ReportKotaEmpatInputModel>("ReportRekapBox", filter);
					responseTask.Wait();

					var result = responseTask.Result;
					if (result.IsSuccessStatusCode)
					{
						var content = result.Content.ReadAsStringAsync();
						RekapKotakEmpatResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<RekapKotakEmpatResponseModel>(content.Result);
						OutPutData = resutl.data;
					}
					else //web api sent error response 
					{
						//log response status here..
						ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
					}
				}
			}
			return Json(OutPutData);
		}

		#endregion

		#region MediaBuyer
		public IActionResult ProcessToPurchase()
		{
			PurchaseOutputModel OutPutData = new PurchaseOutputModel();
			PurchaseInputModel filter = new PurchaseInputModel();
			filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			filter.SubTotal = double.Parse("");
			filter.Diskon = double.Parse("");
			filter.GrandTotal = double.Parse("");
			//filter.BookID = [""];
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "MediaBuyer/");
				var responseTask = client.PostAsJsonAsync<PurchaseInputModel>("GetRoleDropDown", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					PurchaseResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<PurchaseResponseModel>(content.Result);
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

		public IActionResult TesEmailInvoice()
		{
			PurchaseOutputModel OutPutData = new PurchaseOutputModel();

			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "MediaBuyer/");
				var responseTask = client.PostAsync("TesEmailInvoice", null);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					PurchaseResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<PurchaseResponseModel>(content.Result);
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

		public IActionResult ReportToSPV()
		{
			AddReportProblemOutputModel OutPutData = new AddReportProblemOutputModel();
			AddReportProblemInputModel filter = new AddReportProblemInputModel();
			filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			filter.SiteID = Guid.Parse("");
			filter.Desc= "";
			filter.isToSPV = true;
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "MediaBuyer/");
				var responseTask = client.PostAsJsonAsync<AddReportProblemInputModel>("ReportToSPV", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					AddReportProblemResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<AddReportProblemResponseModel>(content.Result);
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
		#endregion

		#region Site
		public IActionResult AddToCompare(string Site, string Item)
		{
			AddCompareOutputModel OutPutData = new AddCompareOutputModel();
			AddCompareInputModel filter = new AddCompareInputModel();
			filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			filter.SiteID = Guid.Parse(Site);
			filter.SiteItemID = Guid.Parse(Item);
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				var responseTask = client.PostAsJsonAsync<AddCompareInputModel>("AddToCompare", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					CompareResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<CompareResponseModel>(content.Result);
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

		public IActionResult ViewCompare()
		{
			List<CompareListResultModel> OutPutData = new List<CompareListResultModel>();
			StringBuilder sb = new StringBuilder();
			CompareListInputModel filter = new CompareListInputModel();
			filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				var responseTask = client.PostAsJsonAsync<CompareListInputModel>("ViewCompare", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					CompareListResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<CompareListResponseModel>(content.Result);
					OutPutData = resutl.data;

					if (OutPutData.Count > 3)
					{
						for (int j = 0; j < 3; j++)
						{
							var quote = "'";
							sb.Append("<div class='column'>");
							BillboardDetail dataBillboard = new BillboardDetail();
							if (OutPutData[j].Item != null && OutPutData[j].Item.Count > 0)
							{
								var item = OutPutData[j].Item;
								foreach (CompareListSiteItemModel data in item)
								{
									dataBillboard.Pjg = data.Panjang;
									dataBillboard.Lbr = data.Lebar;
									if (data.Price != null)
									{
										dataBillboard.Hawl = data.Price[0].HargaAwal;
										dataBillboard.Hahr = data.Price[0].HargaAkhir;
									}
									else
									{
										dataBillboard.Hawl = 0;
										dataBillboard.Hahr = 0;
									}
									if (data.Image != null && data.Image.Count > 0)
									{
										dataBillboard.Img = data.Image[0].Image;
									}
									else
									{
										dataBillboard.Img = "-";
									}
								}
							}

							if (OutPutData[j].Detail != null)
							{
								if (dataBillboard.Img == "-")
								{
									sb.Append("<div class='viewprod'>Photo not available</div>");
								}
								else
								{
									sb.Append("<img src='" + dataBillboard.Img + "' class='listimage'/>");
								}
								sb.Append("<div class='detailprod'>");
								sb.Append("<p class='pheader'>" + OutPutData[j].Detail.NoBillboard + "</p>");
								sb.Append("<p class='pdetail'>Alamat Lokasi Billboard</p>");
								sb.Append("<p class='pdetail'>Tipe    : " + OutPutData[j].Detail.Tipe + "</p>");
								sb.Append("<p class='pdetail'>Ukuran  : " + dataBillboard.Pjg + " X " + dataBillboard.Lbr + "</p>");
								sb.Append("<p class='pdetail'>H/V     : " + OutPutData[j].Detail.HorV + "</p>");
								sb.Append("</div>");
								sb.Append("<div class='detailprice'>");
								sb.Append("<p>Rp. <span class='price'>" + Convert.ToDecimal(dataBillboard.Hawl).ToString("#,##0") + "</span>,-</p>");
								sb.Append("</div>");
								sb.Append("<div class='detailbtn'>");
								if (Domain != "")
								{
									sb.Append("<input type=\"button\" value=\"Delete\" style=\"padding: 5px 10px; font - size: 15px; margin: 5px 5px;\" onclick=\"deleteCompare('/" + Domain + "/Rest/DeleteFromCompare','" + OutPutData[j].Detail.CompareID + "')\" />");
								}
								else
								{
									sb.Append("<input type=\"button\" value=\"Delete\" style=\"padding: 5px 10px; font - size: 15px; margin: 5px 5px;\" onclick=\"deleteCompare('/Rest/DeleteFromCompare','" + OutPutData[j].Detail.CompareID + "')\" />");
								}
								sb.Append("</div>");
							}
							sb.Append("</div>");
						}
					}
					else
					{
						for (int j = 0; j < OutPutData.Count; j++)
						{
							var quote = "'";
							sb.Append("<div class='column'>");
							BillboardDetail dataBillboard = new BillboardDetail();
							if (OutPutData[j].Item != null && OutPutData[j].Item.Count > 0)
							{
								var item = OutPutData[j].Item;
								foreach (CompareListSiteItemModel data in item)
								{
									dataBillboard.Pjg = data.Panjang;
									dataBillboard.Lbr = data.Lebar;
									if (data.Price != null)
									{
										dataBillboard.Hawl = data.Price[0].HargaAwal;
										dataBillboard.Hahr = data.Price[0].HargaAkhir;
									}
									else
									{
										dataBillboard.Hawl = 0;
										dataBillboard.Hahr = 0;
									}
									if (data.Image != null && data.Image.Count > 0)
									{
										dataBillboard.Img = data.Image[0].Image;
									}
									else
									{
										dataBillboard.Img = "-";
									}
								}
							}

							if (OutPutData[j].Detail != null)
							{
								if (dataBillboard.Img == "-")
								{
									sb.Append("<div class='viewprod'>Photo not available</div>");
								}
								else
								{
									sb.Append("<img src='" + dataBillboard.Img + "' class='listimage'/>");
								}
								sb.Append("<div class='detailprod'>");
								sb.Append("<p class='pheader'>" + OutPutData[j].Detail.NoBillboard + "</p>");
								sb.Append("<p class='pdetail'>Alamat Lokasi Billboard</p>");
								sb.Append("<p class='pdetail'>Tipe    : " + OutPutData[j].Detail.Tipe + "</p>");
								sb.Append("<p class='pdetail'>Ukuran  : " + dataBillboard.Pjg + " X " + dataBillboard.Lbr + "</p>");
								sb.Append("<p class='pdetail'>H/V     : " + OutPutData[j].Detail.HorV + "</p>");
								sb.Append("</div>");
								sb.Append("<div class='detailprice'>");
								sb.Append("<p>Rp. <span class='price'>" + Convert.ToDecimal(dataBillboard.Hawl).ToString("#,##0") + "</span>,-</p>");
								sb.Append("</div>");
								sb.Append("<div class='detailbtn'>");
								if (Domain  != "")
								{
									sb.Append("<input type=\"button\" value=\"Delete\" style=\"padding: 5px 10px; font - size: 15px; margin: 5px 5px;\" onclick=\"deleteCompare('/" + Domain + "/Rest/DeleteFromCompare','" + OutPutData[j].Detail.CompareID + "')\" />");
								}
								else
								{
									sb.Append("<input type=\"button\" value=\"Delete\" style=\"padding: 5px 10px; font - size: 15px; margin: 5px 5px;\" onclick=\"deleteCompare('/Rest/DeleteFromCompare','" + OutPutData[j].Detail.CompareID + "')\" />");
								}
								sb.Append("</div>");
							}
							sb.Append("</div>");
						}
					}
					return this.Content(sb.ToString());
				}
				else //web api sent error response 
				{
					//log response status here..
					//OutPutData = Enumerable.Empty<CompareListResultModel>();
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return Json(OutPutData);
		}
		
		public IActionResult DeleteFromCompare(string compareID)
		{
			DeleteCompareOutputModel OutPutData = new DeleteCompareOutputModel();
			DeleteCompareInputModel filter = new DeleteCompareInputModel();
			filter.CompareID= Guid.Parse(compareID);
			filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				var responseTask = client.PostAsJsonAsync<DeleteCompareInputModel>("DeleteFromCompare", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					DeleteCompareResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<DeleteCompareResponseModel>(content.Result);
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

		public IActionResult ClearFromCompare()
		{
			ClearCompareOutputModel OutPutData = new ClearCompareOutputModel();
			ClearCompareInputModel filter = new ClearCompareInputModel();
			filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				var responseTask = client.PostAsJsonAsync<ClearCompareInputModel>("ClearFromCompare", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					ClearCompareResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<ClearCompareResponseModel>(content.Result);
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

		public IActionResult AddToBooked()
		{
			AddBookOutputModel OutPutData = new AddBookOutputModel();
			AddBookInputModel filter = new AddBookInputModel();
			filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			filter.SiteID = Guid.Parse("");
			filter.SiteItemID = Guid.Parse("");
			filter.SitePriceID = Guid.Parse("");
			filter.Qty = 1;
			filter.Price = double.Parse("");
			filter.TotalPerItem = double.Parse("");
			//filter.StartDate = 
			//filter.EndDate = 
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				var responseTask = client.PostAsJsonAsync<AddBookInputModel>("AddToBooked", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					AddBookResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<AddBookResponseModel>(content.Result);
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

		public IActionResult ViewBooked()
		{
			
			CustomViewBook OutPutData = new CustomViewBook();
			ViewBookInputModel filter = new ViewBookInputModel();
			filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
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
					OutPutData.Data = resutl.Data;
					OutPutData.TotalBayar = resutl.TotalBayar;
				}
				else //web api sent error response 
				{
					//log response status here..
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return Json(OutPutData);
		}

		public IActionResult DeleteFromBooked()
		{
			DeleteBookOutputModel OutPutData = new DeleteBookOutputModel();
			DeleteBookInputModel filter = new DeleteBookInputModel();
			filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			//filter.BookID = 
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				var responseTask = client.PostAsJsonAsync<DeleteBookInputModel>("DeleteFromBooked", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					DeleteBookResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<DeleteBookResponseModel>(content.Result);
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

		public IActionResult SendNotifiExpired()
		{
			List<NotificationBookOutputModel> OutPutData = new List<NotificationBookOutputModel>();
			NotificationBookInputModel filter = new NotificationBookInputModel();
			filter.ExpiredDate = DateTime.Parse("");
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				var responseTask = client.PostAsJsonAsync<NotificationBookInputModel>("SendNotifiExpired", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					NotificationBookResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<NotificationBookResponseModel>(content.Result);
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

		public IActionResult SendNotifiWhislistAvailable()
		{
			List<NotificationWishListOutput> OutPutData = new List<NotificationWishListOutput>();
			NotificationWishListInput filter = new NotificationWishListInput();
			filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				var responseTask = client.PostAsJsonAsync<NotificationWishListInput>("SendNotifiWhislistAvailable", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					NotificationWishListResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<NotificationWishListResponseModel>(content.Result);
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

		public IActionResult SendRateScore()
		{
			AddRateOutputModel OutPutData = new AddRateOutputModel();
			AddRateInputModel filter = new AddRateInputModel();
			filter.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
			filter.SiteID = Guid.Parse("");
			filter.RateScore = double.Parse("");
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(BaseAPI + "Site/");
				var responseTask = client.PostAsJsonAsync<AddRateInputModel>("SendRateScore", filter);
				responseTask.Wait();

				var result = responseTask.Result;
				if (result.IsSuccessStatusCode)
				{
					var content = result.Content.ReadAsStringAsync();
					AddRateResponseModel resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<AddRateResponseModel>(content.Result);
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
		#endregion
	}
}
