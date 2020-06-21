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
    public class MediaBuyerController : ControllerBase
    {
        protected readonly DatabaseContext DbContext;

        AppSettings AppSettings { get; }

        public MediaBuyerController(DatabaseContext dbContext, AppSettings appSettings)
        {
            DbContext = dbContext;
            AppSettings = appSettings;
        }

        [Route("Register")]
        [HttpPost]
        public ActionResult<RegisterResponseModel> Register([FromBody] RegisterInputModel data)
        {
            try
            {
                RegisterBL userBL = new RegisterBL(DbContext);
                return userBL.Register(data, RoleEnum.MediaBuyer);
            }
            catch (Exception ex)
            {
                RegisterResponseModel logres = new RegisterResponseModel();
                logres.Message = ex.Message;
                logres.Response = false;

                return logres;
            }

        }

        [Route("ProcessToPurchase")]
        [HttpPost]
        public ActionResult<PurchaseResponseModel> ProcessToPurchase([FromBody] PurchaseInputModel data)
        {
            PurchaseResponseModel res = new PurchaseResponseModel();

            try
            {
                PurchaseBL bl = new PurchaseBL(DbContext);
                var temp = bl.Process(data);

                res.data = temp;
                res.Message = "Success";
                res.Response = true;

                if (temp.IDTransaction != Guid.Empty)
                {
                    ReportBL invoice = new ReportBL(DbContext);

                    var body = invoice.BuildInvoice(temp);

                    MailMessage message = new MailMessage();
                    message.To.Add("enggarprathitaj@gmail.com");
                    message.Body = body;
                    message.Subject = "Pembelian 7061BTJW Telah Selesai";
                    message.From = new MailAddress(AppSettings.EmailConfig.FromAddress);
                    message.IsBodyHtml = true;

                    EmailSenderEngine emailEngine = new EmailSenderEngine();
                    string configJSON = JsonConvert.SerializeObject(AppSettings.EmailConfig);

                    emailEngine.SendEmail(message, configJSON);
                }

                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return res;
            }

        }


        [Route("TesEmailInvoice")]
        [HttpPost]
        public ActionResult<PurchaseResponseModel> TesEmailInvoice()
        {
            PurchaseResponseModel res = new PurchaseResponseModel();

            try
            {
                PurchaseOutputModel temp = new PurchaseOutputModel();

                ReportBL invoice = new ReportBL(DbContext);

                var body = invoice.BuildInvoice(temp);

                MailMessage message = new MailMessage();
                message.To.Add("enggarprathitaj@gmail.com");
                message.Body = body;
                message.Subject = "Pembelian 7061BTJW Telah Selesai";
                message.From = new MailAddress(AppSettings.EmailConfig.FromAddress);
                message.IsBodyHtml = true;

                PdfConvertEngine pdfEngine = new PdfConvertEngine();

                var pdfPath = pdfEngine.ConvertHTMLToPDF(body, AppSettings.PDFPath, "INV_01_123");

                Attachment dataPDF = new Attachment(pdfPath);

                message.Attachments.Add(dataPDF);

                EmailSenderEngine emailEngine = new EmailSenderEngine();
                string configJSON = JsonConvert.SerializeObject(AppSettings.EmailConfig);

                emailEngine.SendEmail(message, configJSON);
                dataPDF.Dispose();


                return res;
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return res;
            }

        }

        [Route("ReportToSPV")]
        [HttpPost]
        public ActionResult<AddReportProblemResponseModel> ReportToSPV([FromBody] AddReportProblemInputModel data)
        {
            AddReportProblemResponseModel res = new AddReportProblemResponseModel();

            try
            {
                ReportBL bl = new ReportBL(DbContext);

                //artinya ini report di kirim ke SPV/Admin
                //kalau false berarti kebalikannya
                data.isToSPV = true;

                var temp = bl.Save(data);

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


    }
}