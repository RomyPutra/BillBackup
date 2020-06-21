using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Iklan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Iklan.Controllers
{
    public class BaseController : Controller
    {
        string BaseAPI = "";
        string Domain = "";
        private readonly IConfiguration _config;
        public BaseController(IConfiguration config)
        {
            _config = config;
            BaseAPI = _config.GetValue<string>("APIBaseUrl");
            Domain = _config.GetValue<string>("Domain");
        }
        public IActionResult GetBillBoardType()
        {
            List<BillboardTypeListOutputModel> billboardtype = new List<BillboardTypeListOutputModel>();
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
            return Ok(billboardtype);
        }

        public IActionResult GetCity()
        {
            List<CityOutputModel> city = new List<CityOutputModel>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseAPI + "Base/");
                    var provTask = client.PostAsync("GetAllCity", null);
                    provTask.Wait();
                    var results = provTask.Result;
                    if (results.IsSuccessStatusCode)
                    {
                        var content = results.Content.ReadAsStringAsync();
                        CityResponseModel cityResponse = JsonConvert.DeserializeObject<CityResponseModel>(content.Result);
                        city = cityResponse.data.OrderBy(x => x.Kota).ToList();
                    }
                    else
                    {
                        city = null;
                        ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
                    }
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Terjadi kesalahan server. Hubungi admin.");
            }
            return Ok(city);
        }
    }
}