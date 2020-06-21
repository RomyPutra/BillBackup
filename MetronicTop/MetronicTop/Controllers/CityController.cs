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
    public class CityController : Controller
    {

        string BaseAPI = "";
        private readonly IConfiguration _config;

        public CityController(IConfiguration config)
        {
            _config = config;
            BaseAPI = _config.GetValue<string>("APIBaseUrl");
        }

        public IActionResult City()
        {
            return View();
        }

        public IActionResult Create()
		{
			return View();
		}

        public JsonResult GetCity(DataTableAjaxPostModel model)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;
        
            List<CityOutputModel> city = new List<CityOutputModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAPI + "Base/");
                var cityTask = client.PostAsync("GetAllCity", null);
                cityTask.Wait();
                var results = cityTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    var content = results.Content.ReadAsStringAsync();
                    CityResponseModel cityResponse = JsonConvert.DeserializeObject<CityResponseModel>(content.Result);
                    city = cityResponse.data.OrderBy(x => x.Kode).ToList();
                }
                else
                {
                    city = null;
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            var result = new List<JsonCity>();

            foreach(var x in city)
            {
                result.Add(new JsonCity
                {
                    CityID = x.CityID,
                    kode = x.Kode.ToString(),
                    city = x.Kota.ToString()
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