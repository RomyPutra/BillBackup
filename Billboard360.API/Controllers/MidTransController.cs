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
using System.Net.Http;
using System.Net.Http.Headers;
using RestSharp;
using RestSharp.Deserializers;
using System.IO;
using RestSharp.Extensions;
using RestSharp.Authenticators;
using Billboard360.Helper.Encrypt;
using System.Net;
using System.Text;

namespace Billboard360.API.Controllers
{
    public class MidTransController : ControllerBase
    {

        protected readonly DatabaseContext DbContext;
        AppSettings AppSettings { get; }


        public MidTransController(DatabaseContext dbContext, AppSettings appSettings)
        {
            DbContext = dbContext;
            AppSettings = appSettings;
        }


        [Route("charge")]
        [HttpPost]
        public ActionResult<MidTransTransactionResponseModel> Charge([FromBody] MidTransChargeInputModel data)
        {
     
            MidTransBL bl = new MidTransBL(DbContext, AppSettings);

            var dat = bl.TransactionMidTrans(data);

            MidTransTransactionResponseModel res = new MidTransTransactionResponseModel();

            res = dat;
            return res;

        }

        [Route("UpdatePaymentStatus")]
        [HttpPost]
        public ActionResult<UpdatePaymentStatusResponseModel> UpdatePayment([FromBody] UpdatePaymentStatusInputModel data)
        {
            MidTransBL bl = new MidTransBL(DbContext, AppSettings);

            return bl.SaveMidtransLog(data, ModeMidTransEnum.MobileSDK);
            
        }

    }
}