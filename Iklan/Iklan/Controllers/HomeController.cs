using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Iklan.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace Iklan.Controllers
{
    public class HomeController : Controller
    {
        const string Loginfrom = "_LoginF";
        string BaseAPI = "";
        string Domain = "";
        private readonly IConfiguration _config;
        public HomeController(IConfiguration config)
        {
            _config = config;
            BaseAPI = _config.GetValue<string>("APIBaseUrl");
            Domain = _config.GetValue<string>("Domain");
        }
        public IActionResult Index()
        {
            List<BillboardTypeListOutputModel> billboardtype = new List<BillboardTypeListOutputModel>();
            List<ProvinceOutputModel> province = new List<ProvinceOutputModel>();

            LandingPageModel model = new LandingPageModel();
            try
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

                        model.BillBoardType = new List<BillboardTypeListOutputModel>();
                        model.BillBoardType = billboardtype;

                        typeTank = client.PostAsync("GetProvince", null);
                        typeTank.Wait();
                        results = typeTank.Result;
                        if (results.IsSuccessStatusCode)
                        {
                            content = results.Content.ReadAsStringAsync();
                            ProvinceResponseModel provinceResponse = JsonConvert.DeserializeObject<ProvinceResponseModel>(content.Result);
                            province = provinceResponse.data.OrderBy(x => x.Provinsi).ToList();

                            model.Province = new List<ProvinceOutputModel>();
                            model.Province = province;
                            HttpContext.Session.SetString(Loginfrom, "MDB");
                        }
                        else
                        {
                            province = null;
                            ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
                        }


                    }
                    else
                    {
                        billboardtype = null;
                        ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
                    }
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SyaratKetentuan()
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
