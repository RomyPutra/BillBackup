using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Billboard360.API.Models;
using Billboard360.API.BussinessLogic;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;
using Billboard360.Helper.Engine;
using Newtonsoft.Json;
using Billboard360.API.Core;
using Newtonsoft;
using Billboard360.API.Models.Enums;

namespace Billboard360.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class notificationController : Controller
    {
        protected readonly DatabaseContext DbContext;

        AppSettings AppSettings { get; }

        public notificationController(DatabaseContext dbContext, AppSettings appSettings)
        {
            DbContext = dbContext;
            AppSettings = appSettings;
        }



        [Route("handling")]
        [HttpPost]
        public ActionResult<UpdatePaymentStatusResponseModel> handling([FromBody] NotificationHandlingModel data)
        {
            MidTransBL bl = new MidTransBL(DbContext, AppSettings);

            UpdatePaymentStatusInputModel input = new UpdatePaymentStatusInputModel();

            return bl.SaveMidtransLog(data, ModeMidTransEnum.Listener);
        }
    }
}