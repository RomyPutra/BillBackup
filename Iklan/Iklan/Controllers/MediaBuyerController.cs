using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Iklan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
namespace Iklan.Controllers
{
    public class MediaBuyerController : Controller
    {
        const string SessionKeyID = "_ID";
        string BaseAPI = "";
        string Domain = "";
        private readonly IConfiguration _config;
        public MediaBuyerController(IConfiguration config)
        {
            _config = config;
            BaseAPI = _config.GetValue<string>("APIBaseUrl");
            Domain = _config.GetValue<string>("Domain");
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Search(Guid? type)
        {
            int Draw = 10;
            int totalResultsCount = 0;
            int filteredResultsCount = 0;
            List<SiteListOutputModel> sites = new List<SiteListOutputModel>();
            SiteListResponseModel resutl = new SiteListResponseModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAPI + "Site/");
                FilterBillboard filter = new FilterBillboard();

                filter.showWithDisabledSite = true;
                filter.isFilterDataTable = true;

                filter.PageSize = 10;
                filter.PageNumber = 1;
                filter.TypeBillboard = type.ToString();
                filter.Latitude = "";
                filter.Longitude = "";
                filter.Province = "";
                filter.City = "";
                filter.Alamat = "";
                //filter.UserID = "";
                filter.FilterDataTableValue = "";

                var responseTask = client.PostAsJsonAsync<FilterBillboard>("GetListSite", filter);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();
                    resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteListResponseModel>(content.Result);
                    sites = resutl.data;
                    foreach (SiteListOutputModel data in sites)
                    {
                        data.Status = data.Status == "Active" ? "Tersedia" : "Tidak Tersedia";
                    }
                    totalResultsCount = resutl.TotalData;
                    filteredResultsCount = resutl.TotalData;
                }
                else //web api sent error response 
                {
                    ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
                }
            }

            return View(resutl);
        }

        public IActionResult SearchBar(string location, string type, string city)
        {
            int Draw = 10;
            int totalResultsCount = 0;
            int filteredResultsCount = 0;
            List<SiteListOutputModel> sites = new List<SiteListOutputModel>();
            SiteListResponseModel resutl = new SiteListResponseModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAPI + "Site/");
                FilterBillboard filter = new FilterBillboard();

                filter.showWithDisabledSite = true;
                filter.isFilterDataTable = true;

                filter.PageSize = 10;
                filter.PageNumber = 1;
                filter.TypeBillboard = type == null ? "" : type;
                filter.Latitude = "";
                filter.Longitude = "";
                filter.Province = "";
                filter.City = city == null ? "" : city;
                filter.Alamat = location == null ? "" : location;
                //filter.UserID = "";
                filter.FilterDataTableValue = "";

                var responseTask = client.PostAsJsonAsync<FilterBillboard>("GetListSite", filter);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();
                    resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteListResponseModel>(content.Result);
                    sites = resutl.data;
                    foreach (SiteListOutputModel data in sites)
                    {
                        data.Status = data.Status == "Active" ? "Tersedia" : "Tidak Tersedia";
                    }
                    totalResultsCount = resutl.TotalData;
                    filteredResultsCount = resutl.TotalData;
                }
                else //web api sent error response 
                {
                    ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
                }
            }

            return View("SearchMedia", resutl);
        }

        public IActionResult SearchMedia(int page, string location, string type, string city, string provinsi, DateTime? filterfrom, DateTime? filterto, int? panjang, int? lebar, double? minprice, double? maxprice, sort? sorting)
        {
            int Draw = 10;
            int totalResultsCount = 0;
            int filteredResultsCount = 0;
            List<SiteListOutputModel> sites = new List<SiteListOutputModel>();
            SiteListResponseModel resutl = new SiteListResponseModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAPI + "Site/");
                FilterBillboard filter = new FilterBillboard();

                filter.showWithDisabledSite = false;
                filter.isFilterDataTable = false;

                filter.PageSize = 10;
                filter.PageNumber = page;
                filter.TypeBillboard = type == null ? "" : type == "null" ? "" : type;
                filter.Latitude = "";
                filter.Longitude = "";
                filter.Province = provinsi == null ? "" : provinsi == "null" ? "" : provinsi;
                filter.City = city == null  ? "" : city=="null" ? "" : city;
                filter.Alamat = location == null ? "" : location == "null" ? "" : location == "undefined"?"" : location;
                filter.FilterDataTableValue = "";
                filter.startDate = filterfrom;
                filter.endDate = filterto;
                filter.panjang = panjang == null ? 0  : panjang;
                filter.lebar = lebar == null ? 0 : lebar;
                filter.MinimumPrice = minprice == null ? 0 : minprice;
                filter.MaximumPrice = maxprice == null ? 0 : maxprice;
                filter.sorting = sorting == null ? sort.AtoZ : sorting;
                var responseTask = client.PostAsJsonAsync<FilterBillboard>("GetListSite", filter);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();
                    resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteListResponseModel>(content.Result);
                    sites = resutl.data;

                    //cuman buat testing
                    //var searchDataTest = sites.Where(x => x.NoBillboard == "6537JIJW").ToList() ; 

                    foreach (SiteListOutputModel data in sites)
                    {
                        data.Status = data.Status == "Active" ? "Tersedia" : "Tidak Tersedia";
                    }
                    totalResultsCount = resutl.TotalData;
                    filteredResultsCount = resutl.TotalData;
                    resutl.CurrentPage = page;
                    resutl.type = filter.TypeBillboard;
                    resutl.city = filter.City;
                    resutl.location = filter.Alamat;
                    resutl.provinsi = filter.Province;
                    resutl.startDate = filter.startDate;
                    resutl.endDate = filter.endDate;
                    resutl.panjang = filter.panjang;
                    resutl.lebar = filter.lebar;
                    resutl.MinimumPrice = filter.MinimumPrice;
                    resutl.MaximumPrice = filter.MaximumPrice;
                    resutl.sorting = filter.sorting;

                }
                else //web api sent error response 
                {
                    ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
                }
            }

            if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
            {
                resutl.isLogin = true;
            }
            else
            {
                resutl.isLogin = false;
            }

            return View(resutl);
        }
        public IActionResult ViewDetail(Guid id)
        {
            ForDetail site = new ForDetail();
            var cities = new List<City>();
            var BillBoards = new SiteDetailResultOutputModel();
            SiteDetailResponseModel resutl = new SiteDetailResponseModel();

          
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAPI + "Site/");
                //HTTP POST
                SiteDetailInputModel filter = new SiteDetailInputModel();
                filter.SiteID = id;
                var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();
                    resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);

                }
                else //web api sent error response 
                {
                    //log response status here..
                    site = null;
                    ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
                }
            }

            if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
            {
                resutl.isLogin = true;
            }
            else
            {
                resutl.isLogin = false;
            }

            return View(resutl);
        }
        public IActionResult AddtoCart(AddtoCartSite info)
        {
            AddBookInputModel data = new AddBookInputModel();
            System.TimeSpan diffResult = info.EndDate - info.StartDate;
            BaseResponseModel resutl = new BaseResponseModel();

            //temp user
            data.UserID = Guid.Parse(HttpContext.Session.GetString(SessionKeyID));
          //  data.UserID = Guid.Parse("8E8C7738-5076-4E04-A1FB-DAD4A7C2AC21");
            //end temp user
            data.SiteID = info.SiteID;
            data.SiteItemID = info.SiteItemID;
            data.SitePriceID = info.SitePriceID;
            data.StartDate = info.StartDate;
            data.EndDate = info.EndDate;
            data.Price = info.Price;
            data.TotalPerItem = info.Price * diffResult.Days;
            data.Qty = diffResult.Days;

            CheckDateAvailableInputModel checkDate = new CheckDateAvailableInputModel();
            checkDate.SiteID = info.SiteID;
            checkDate.SiteItemID = info.SiteItemID;
            checkDate.StartDate = info.StartDate;
            checkDate.EndDate = info.EndDate;


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
                            var postTask = client.PostAsJsonAsync<AddBookInputModel>("AddToCart", data);
                            postTask.Wait();

                            var result = postTask.Result;
                            if (result.IsSuccessStatusCode)
                            {
                                resutl.Response = true;
                                resutl.Message = "Add To Cart Successfully.";
                            }
                            else
                            {
                                resutl.Response = false;
                                resutl.Message = "Fail to add data. Please contact administrator.";
                            }
                        }
                    }
                    else
                    {
                        resutl.Response = false;
                        resutl.Message = "Fail to add data, date not available. Please contact administrator.";
                    }
                }
                else
                {
                    resutl.Response = false;
                    resutl.Message = "Fail to add data. Please contact administrator.";
                }
            }


            //ForDetail site = new ForDetail();
            //using (var client = new HttpClient())
            //{

            //}

            return Ok(resutl);
        }

        public IActionResult PopUpRequest(Guid id, string act)
        {
            ForDetail site = new ForDetail();
            var cities = new List<City>();
            var BillBoards = new SiteDetailResultOutputModel();
            SiteDetailResponseModel resutl = new SiteDetailResponseModel();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAPI + "Site/");
                //HTTP POST
                SiteDetailInputModel filter = new SiteDetailInputModel();
                filter.SiteID = id;
                var postTask = client.PostAsJsonAsync<SiteDetailInputModel>("GetSite", filter);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = result.Content.ReadAsStringAsync();
                    resutl = Newtonsoft.Json.JsonConvert.DeserializeObject<SiteDetailResponseModel>(content.Result);

                }
                else //web api sent error response 
                {
                    //log response status here..
                    site = null;
                    ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
                }
            }

            if (HttpContext.Session.GetString(SessionKeyID) != null && HttpContext.Session.GetString(SessionKeyID) != "")
            {
                resutl.isLogin = true;
            }
            else
            {
                resutl.isLogin = false;
            }
            resutl.act = act;
            return PartialView(resutl);
        }

    }
}