using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MetronicTop.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MetronicTop.Controllers
{
    public class RegionController : Controller
    {

        string BaseAPI = "";
        private readonly IConfiguration _config;

        public RegionController(IConfiguration config)
        {
            _config = config;
            BaseAPI = _config.GetValue<string>("APIBaseUrl");
        }

        public IActionResult Provinsi()
        {
            return View();
        }

        public JsonResult GetProvince(DataTableAjaxPostModel model)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;
        
            List<ProvinceOutputModel> province = new List<ProvinceOutputModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAPI + "Base/");
                var provTask = client.PostAsync("GetProvince", null);
                provTask.Wait();
                var results = provTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    var content = results.Content.ReadAsStringAsync();
                    ProvinceResponseModel provinceResponse = JsonConvert.DeserializeObject<ProvinceResponseModel>(content.Result);
                    province = provinceResponse.data.OrderBy(x => x.Kode).ToList();
                }
                else
                {
                    province = null;
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            var result = new List<JsonProvince>();

            foreach(var x in province)
            {
                result.Add(new JsonProvince
                {
                    ProvinceID = x.ProvinceID,
                    kode = x.Kode.ToString(),
                    provinsi = x.Provinsi
                });

            }

            totalResultsCount = result.Count();

            return Json(new 
            {
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
            });
        }


    }
}