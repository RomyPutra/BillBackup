using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Billboard360.API.Models;
using Billboard360.API.BussinessLogic;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;

namespace Billboard360.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        protected readonly DatabaseContext db;


        public DashboardController(DatabaseContext dbContext)
        {
            db = dbContext;
        }


        [Route("ReportPurchaseAndBook")]
        [HttpPost]
        public ActionResult<ReportPurchaseAndModelResponseModel> ReportPurchaseAndBook([FromBody] ReportPurchaseAndBookInputModel data)
        {
            ReportPurchaseAndModelResponseModel res = new ReportPurchaseAndModelResponseModel();

            try
            {
                ReportBL bl = new ReportBL(db);
                var temp = bl.GetReportPurchaseAndBook(data);

                res.data = temp.result;
                
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


        [Route("ReportSite")]
        [HttpPost]
        public ActionResult<ReportSiteResponseModel> ReportSite([FromBody] ReportSiteInputModel data)
        {
            ReportSiteResponseModel res = new ReportSiteResponseModel();

            try
            {
                ReportBL bl = new ReportBL(db);
                var temp = bl.GetReportSite(data);

                res.data = temp.data;
                res.Message = "Success";
                res.Response = true;
                res.TotalPages = temp.TotalPages;
                res.TotalData = temp.TotalData;

                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return res;
            }

        }

        [Route("ReportRekapBox")]
        [HttpPost]
        public ActionResult<RekapKotakEmpatResponseModel> ReportRekapBox([FromBody] ReportKotaEmpatInputModel data)
        {
            RekapKotakEmpatResponseModel res = new RekapKotakEmpatResponseModel();

            try
            {
                ReportBL bl = new ReportBL(db);
                var temp = bl.GetKotakEmpat(data);

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

        [Route("ReportChartTransactionPerMonth")]
        [HttpPost]
        public ActionResult<chartResponse> ReportChartTransaction()
        {
            chartResponse res = new chartResponse();

            try
            {
                ReportBL bl = new ReportBL(db);
                res.data = bl.GetTransactionPerMonth();
                res.Message = "Success";
                res.Response = true;

                return res;

            }catch(Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return res;
            }
        }


        [Route("ReportChartBookPerMonth")]
        [HttpPost]
        public ActionResult<chartResponse> ReportChartBookPerMonth()
        {
            chartResponse res = new chartResponse();

            try
            {
                ReportBL bl = new ReportBL(db);
                res.data = bl.GetBookPerMonth();
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
    }
}