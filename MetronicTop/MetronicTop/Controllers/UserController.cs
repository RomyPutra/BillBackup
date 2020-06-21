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
    public class UserController : Controller
    {
        string BaseAPI = "";
        private readonly IConfiguration _config;

        public UserController(IConfiguration config)
        {
            _config = config;
            BaseAPI = _config.GetValue<string>("APIBaseUrl");
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetUser(DataTableAjaxPostModel model)
        {
            int filteredResultsCount = 0;
            int totalResultsCount = 0;
        
            List<ListUserOutputModel> user = new List<ListUserOutputModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAPI + "Admin/");
                ListUserInputModel filter = new ListUserInputModel();
                filter.UserName = "";
                filter.CompanyName = "";
                filter.RoleName = "";
                filter.PageNumber = 1;
                filter.PageSize = 1000;
                // var userTask = client.PostAsync("GetUserList", null);
                // userTask.Wait();
                // var results = userTask.Result;
                // if (results.IsSuccessStatusCode)
                // {
                //     var content = results.Content.ReadAsStringAsync();
                //     ListUserResponseModel userResponse = JsonConvert.DeserializeObject<ListUserResponseModel>(content.Result);
                //     user = userResponse.data.OrderBy(x => x.UserID).ToList();
                // }
                // else
                // {
                //     user = null;
                //     ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                // }
            }

            var result = new List<JsonUser>();

            foreach(var x in user)
            {
                result.Add(new JsonUser
                {
                    UserID = x.UserID,
                    FirstName = x.FirstName.ToString(),
                    LastName = x.LastName.ToString()
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