using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MetronicTop.Models;
using System.Net.Http;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MetronicTop.Controllers
{
    public class DashboardController : Controller
    {
		const string SessionKeyID = "_ID";
		const string SessionKeyName = "_NAME";
		const string SessionKeyFName = "_FNAME";
		const string SessionKeyLName = "_LNAME";
		const string SessionKeyRole = "_ROLE";
		const string SessionKeyProv = "_PROV";
		const string SessionKeyBank = "_BANK";
		const string SessionKeyType = "_TYPE";
		const string SessionKeyCity = "_CITY";
		const string Loginfrom = "_LoginF";
		string BaseAPI = "";
		string Domain = "";
		private readonly IConfiguration _config;
        public DashboardController(IConfiguration config)
        {
            _config = config;
            BaseAPI = _config.GetValue<string>("APIBaseUrl");
			Domain = _config.GetValue<string>("Domain");
		}

		public IActionResult GetProvince()
		{
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
					province = provinceResponse.data.OrderBy(x => x.Provinsi).ToList();
				}
				else
				{
					province = null;
					ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
				}
			}
			return Json(province);
		}

	}
}
