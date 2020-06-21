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
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        protected readonly DatabaseContext DbContext;

        AppSettings AppSettings { get; }

        public AdminController(DatabaseContext dbContext, AppSettings appSettings)
        {
            DbContext = dbContext;
            AppSettings = appSettings;
        }


        [Route("GetListUserBankAccount")]
        [HttpPost]
        public ActionResult<ListBankAccountResponseModel> GetListBankAccount()
        {
            ListBankAccountResponseModel res = new ListBankAccountResponseModel();

            try
            {
                BankAccountBL bl = new BankAccountBL(DbContext);
                var temp = bl.GetBankAccount();

                res.data = temp;
                res.Message = "Success";
                res.Response = true;

                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return res;
            }

        }

        [Route("EnableDisabledUser")]
        [HttpPost]
        public ActionResult<ListUserResponseModel> EnableDisabledUser([FromBody] UserEnableDisabledInputModel data)
        {
            ListUserResponseModel res = new ListUserResponseModel();

            try
            {
                UserBL bl = new UserBL(DbContext);
                var temp = bl.EnabledDisabledUser(data);

                res.data = temp;
                res.Message = "Success";
                res.Response = true;

                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return res;
            }

        }

        [Route("GetUserList")]
        [HttpPost]
        public ActionResult<ListUserResponseModel> GetUserList([FromBody] ListUserInputModel data)
        {
            ListUserResponseModel res = new ListUserResponseModel();

            try
            {
                UserBL bl = new UserBL(DbContext);
                var temp = bl.GetListUser(data);

                res.data = temp;
                res.Message = "Success";
                res.Response = true;

                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return res;
            }

        }

        [Route("ViewReportProblemSiteList")]
        [HttpPost]
        public ActionResult<ReportProblemListResponseModel> ViewReportProblemSiteList([FromBody] ReportProblemListInputModel data)
        {
            ReportProblemListResponseModel res = new ReportProblemListResponseModel();

            try
            {
                ReportBL bl = new ReportBL(DbContext);
                var temp = bl.GetProblem(data);

                res.data = temp.data;
                res.Message = "Success";
                res.Response = true;

                res.TotalData = temp.TotalData;
                res.TotalPages = temp.TotalPages;

                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return res;
            }

        }


        [Route("AddSupervisor")]
        [HttpPost]
        public ActionResult<RegisterResponseModel> AddSupervisor([FromBody] RegisterInputModel data)
        {
            try
            {
                RegisterBL userBL = new RegisterBL(DbContext);
                return userBL.Register(data, RoleEnum.Supervisor);
            }
            catch (Exception ex)
            {
                RegisterResponseModel logres = new RegisterResponseModel();
                logres.Message = ex.Message;
                logres.Response = false;

                return logres;
            }

        }




    }
}