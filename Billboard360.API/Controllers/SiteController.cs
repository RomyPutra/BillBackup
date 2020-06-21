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
    public class SiteController : ControllerBase
    {
        protected readonly DatabaseContext DbContext;

        AppSettings AppSettings { get; }

        public SiteController(DatabaseContext dbContext, AppSettings appSettings)
        {
            DbContext = dbContext;
            AppSettings = appSettings;
        }

        [Route("GetListSite")]
        [HttpPost]
        public ActionResult<SiteListResponseModel> GetListSite([FromBody] FilterBillboard filter)
        {
            SiteListResponseModel res = new SiteListResponseModel();

            try
            {
                SiteBL bl = new SiteBL(DbContext);
                var data = bl.GetListSite(filter);

                res.data = data.Result;
                res.Message = "Success";
                res.Response = true;
                res.TotalPages = data.TotalPage;
                res.TotalData = data.TotalData;
                

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return BadRequest(res);
            }

        }

        [Route("GetSite")]
        [HttpPost]
        public ActionResult<SiteDetailResponseModel> GetSite([FromBody] SiteDetailInputModel data)
        {
            SiteDetailResponseModel res = new SiteDetailResponseModel();

            try
            { 
                SiteBL bl = new SiteBL(DbContext);
                var temp = bl.GetSiteDetail(data);

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

        [Route("ImportSite")]
        [HttpPost]
        public ActionResult<ImportSiteResponseModel> ImportSite([FromBody] ImportSiteInputModel data)
        {
            ImportSiteResponseModel res = new ImportSiteResponseModel();

            LogAppBL blLog = new LogAppBL(DbContext);

            try
            {
                string message = data.UserID + " Start upload excel list site";

                //blLog.SaveLog(LogAppEnum.StartUploadExcelSite, data.UserID, message);

                SiteBL bl = new SiteBL(DbContext);
                bl.ImportSite(data);

                ImportSiteOutputModel x = new ImportSiteOutputModel();
                x.ImportDate = DateTime.Now;

                res.data = x;
                res.Message = "Success upload file excel";
                res.Response = true;

                //blLog.SaveLog(LogAppEnum.EndUploadExcelSite, data.UserID, res.Message);

                return res;
            }
            catch (Exception ex)
            {

                res.Message = ex.Message;
                res.Response = false;

                //blLog.SaveLog(LogAppEnum.ErrorUploadExcelSite, data.UserID, ex.Message);

                return res;
            }


        }

        [Route("UpdateSite")]
        [HttpPost]
        public ActionResult<EditSiteResponseModel> UpdateSite([FromBody] EditSiteResultInputModel data)
        {
            EditSiteResponseModel res = new EditSiteResponseModel();

            try
            {
                SiteBL bl = new SiteBL(DbContext);
                var temp = bl.Edit(data);

                res.data = temp;
                res.Message = "Success";
                res.Response = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return BadRequest(res);
            }

        }

        [Route("DeleteSite")]
        [HttpPost]
        public ActionResult<DeleteSiteResponseModel> DeleteSite([FromBody] DeleteSiteInputModel data)
        {
            DeleteSiteResponseModel res = new DeleteSiteResponseModel();

            try
            {
                SiteRepository repo = new SiteRepository(DbContext);
                var tes = repo.Delete(data.SiteID, data.UserID);

                DeleteSiteOutputModel output = new DeleteSiteOutputModel();
                output.message = tes.Message;

                res.data = output;
                res.Message = "Success";
                res.Response = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return BadRequest(res);
            }

        }

        [Route("ChangeStatusSite")]
        [HttpPost]
        public ActionResult<ChangeStatusResponseModel> ChangeStatusSite([FromBody] ChangeStatusInputModel data)
        {
            ChangeStatusResponseModel res = new ChangeStatusResponseModel();

            try
            {
                SiteRepository repo = new SiteRepository(DbContext);
                var tes = repo.ChangeStatus(data.SiteID, data.UserID, data.Value);

                ChangeStatusOutputModel output = new ChangeStatusOutputModel();
                output.message = tes.Message;

                res.data = output;
                res.Message = "Success";
                res.Response = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return BadRequest(res);
            }

        }

        [Route("AddSiteDetail")]
        [HttpPost]
        public ActionResult<AddSiteResponseModel> AddSiteDetail([FromBody] AddSiteResultDetailInputModel data)
        {
            AddSiteResponseModel res = new AddSiteResponseModel();

            try
            {
                SiteBL bl = new SiteBL(DbContext);
                var x = bl.SaveDetailSite(data);

                res.data = x;
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

        [Route("AddSiteItemDetail")]
        [HttpPost]
        public ActionResult<AddSiteItemDetailResponseModel> AddSiteItemDetail([FromBody] AddSiteItemDetailInputModel data)
        {
            AddSiteItemDetailResponseModel res = new AddSiteItemDetailResponseModel();

            try
            {
                SiteBL bl = new SiteBL(DbContext);
                var x = bl.SaveDetailSiteItem(data);

                res.data = x;
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

        [Route("AddSitePrice")]
        [HttpPost]
        public ActionResult<AddSiteResponseModel> AddSitePrice([FromBody] AddSiteResultPriceInputModel data)
        {
            AddSiteResponseModel res = new AddSiteResponseModel();

            try
            {
                SiteBL bl = new SiteBL(DbContext);
                var x = bl.SavePriceSite(data);

                res.data = x;
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

        [Route("AddSiteImage")]
        [HttpPost]
        public ActionResult<AddSiteResponseModel> AddSiteImage([FromBody] AddSiteResultImageInputModel data)
        {
            AddSiteResponseModel res = new AddSiteResponseModel();

            try
            {
                SiteBL bl = new SiteBL(DbContext);
                var x = bl.SaveImageSite(data);

                res.data = x;
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

        [Route("GetWishList")]
        [HttpPost]
        public ActionResult<WishListResponseModel> GetWishList([FromBody] WishListInputModel data)
        {
            WishListResponseModel res = new WishListResponseModel();

            try
            {
                WishListBL bl = new WishListBL(DbContext);
                var temp = bl.GetWishList(data).ToList();

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

        [Route("AddToWishList")]
        [HttpPost]
        public ActionResult<AddToWishListResponseModel> AddToWishList([FromBody] AddToWishListInputModel data)
        {
            AddToWishListResponseModel res = new AddToWishListResponseModel();

            try
            {
                WishListBL bl = new WishListBL(DbContext);
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

        [Route("DeleteFromWishList")]
        [HttpPost]
        public ActionResult<DeleteFromWishListResponseModel> DeleteFromWishList([FromBody] DeleteFromWishListInputModel data)
        {
            DeleteFromWishListResponseModel res = new DeleteFromWishListResponseModel();

            try
            {
                WishListBL bl = new WishListBL(DbContext);
                var temp = bl.DeleteFromWishList(data.WishListID);

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

        [Route("AddToCompare")]
        [HttpPost]
        public ActionResult<CompareResponseModel> AddToCompare([FromBody] AddCompareInputModel data)
        {
            CompareResponseModel res = new CompareResponseModel();

            try
            {
                CompareBL bl = new CompareBL(DbContext);
                var temp = bl.Save(data);

                res.data = temp;
                res.Message = temp.Message;
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

        [Route("ViewCompare")]
        [HttpPost]
        public ActionResult<CompareListResponseModel> ViewCompare([FromBody] CompareListInputModel data)
        {
            CompareListResponseModel res = new CompareListResponseModel();

            try
            {
                CompareBL bl = new CompareBL(DbContext);
                var temp = bl.GetCompareList(data);

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


        [Route("DeleteFromCompare")]
        [HttpPost]
        public ActionResult<DeleteCompareResponseModel> DeleteFromCompare([FromBody] DeleteCompareInputModel data)
        {
            DeleteCompareResponseModel res = new DeleteCompareResponseModel();

            try
            {
                CompareBL bl = new CompareBL(DbContext);
                var temp = bl.DeleteCompare(data);

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

        [Route("ClearFromCompare")]
        [HttpPost]
        public ActionResult<ClearCompareResponseModel> ClearFromCompare([FromBody] ClearCompareInputModel data)
        {
            ClearCompareResponseModel res = new ClearCompareResponseModel();

            try
            {
                CompareBL bl = new CompareBL(DbContext);
                var temp = bl.DeleteCompare(data);

                res.data = temp;
                res.Message = temp.Message;
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

        [Route("AddToCart")]
        [HttpPost]
        public ActionResult<AddToCartResponseModel> AddToCart([FromBody] AddBookInputModel data)
        {
            AddToCartResponseModel res = new AddToCartResponseModel();

            try
            {
                BookBL bl = new BookBL(DbContext);
                var temp = bl.SaveCart(data);


                res.data = temp;

                if(temp.BookID == Guid.Empty)
                {
                    res.Message = "Item ini sudah ada di list keranjang / list order";
                    res.Response = false;
                } else
                {
                    res.Message = "Item berhasil ditambahkan di keranjang";
                    res.Response = true;
                }

                

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return BadRequest(res);
            }

        }

        [Route("ViewCart")]
        [HttpPost]
        public ActionResult<ViewArrayCartResponse> ViewCart([FromBody] ViewCartInputModel data)
        {
            ViewArrayCartResponse res = new ViewArrayCartResponse();

            try
            {
                BookBL bl = new BookBL(DbContext);
                res = bl.ViewCart(data);

                res.Message = "Success";
                res.Response = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return BadRequest(res);
            }

        }

        [Route("DeleteFromCart")]
        [HttpPost]
        public ActionResult<DeleteBookResponseModel> DeleteFromCart([FromBody] DeleteBookInputModel data)
        {
            DeleteBookResponseModel res = new DeleteBookResponseModel();

            try
            {
                BookBL bl = new BookBL(DbContext);
                var respo = bl.DeleteCart(data);

                res.Message = respo.Message;
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

        [Route("AddToBooked")]
        [HttpPost]
        public ActionResult<AddBookResponseModel> AddToBooked([FromBody] AddToBookListInputModel data)
        {
            AddBookResponseModel res = new AddBookResponseModel();

            try
            {
                BookBL bl = new BookBL(DbContext);
                var temp = bl.Save(data);

                string configJSON = JsonConvert.SerializeObject(AppSettings.EmailConfig);

                //start send Email to Admin
                var bodyAdmin = bl.BuilBookReceiptToAdmin(data);
                MailMessage message = new MailMessage();
                message.To.Add("enggarprathitaj@gmail.com");
                message.Body = bodyAdmin;
                message.Subject = "Booking " ;
                message.From = new MailAddress(AppSettings.EmailConfig.FromAddress);
                message.IsBodyHtml = true;
                PdfConvertEngine pdfEngine = new PdfConvertEngine();
                var pdfPath = pdfEngine.ConvertHTMLToPDF(bodyAdmin, AppSettings.PDFPath, temp.BookNo.Replace('/','_'));
                Attachment dataPDF = new Attachment(pdfPath);
                message.Attachments.Add(dataPDF);
                EmailSenderEngine emailEngine = new EmailSenderEngine();
                emailEngine.SendEmail(message, configJSON);
                dataPDF.Dispose();
                //End send Email to admin

                //start send Email to Media buyer
                var bodyBuyer = bl.BuilBookReceiptToMediaBuyer(data);
                UserRepository userRepo = new UserRepository(DbContext);
                var mediaBuyerInfo = userRepo.FindByID(data.Data[0].UserID).FirstOrDefault();
                message = new MailMessage();
                message.To.Add(mediaBuyerInfo.UserName);
                message.Body = bodyBuyer;
                message.Subject = "Booking ";
                message.From = new MailAddress(AppSettings.EmailConfig.FromAddress);
                message.IsBodyHtml = true;
                pdfEngine = new PdfConvertEngine();
                pdfPath = pdfEngine.ConvertHTMLToPDF(bodyBuyer, AppSettings.PDFPath, temp.BookNo.Replace('/','_'));
                dataPDF = new Attachment(pdfPath);
                message.Attachments.Add(dataPDF);
                emailEngine = new EmailSenderEngine();
                emailEngine.SendEmail(message, configJSON);
                dataPDF.Dispose();
                //End send Email to Media buyer


                res.data = temp;
                res.Message = "Success";
                res.Response = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return BadRequest(res);
            }

        }

        [Route("ApprovalBooked")]
        [HttpPost]
        public ActionResult<ApprovalBookResponseModel> ApprovalBooked([FromBody] ApprovalBookInputModel data)
        {
            ApprovalBookResponseModel res = new ApprovalBookResponseModel();

            try
            {
                BookBL bl = new BookBL(DbContext);
                var temp = bl.ApprovalBook(data);

                string configJSON = JsonConvert.SerializeObject(AppSettings.EmailConfig);


                res.data = temp;
                res.Response = true;
                res.Message = temp.Message;
                res.TotalPages = 1;


                //Send email to Media Buyer
                var bodyBuyer = bl.BuilBookEmailApprovalToMediaBuyer(data, temp.Message);
                UserRepository userRepo = new UserRepository(DbContext);
                var mediaBuyerInfo = userRepo.FindByID(data.UserID).FirstOrDefault();
                MailMessage message = new MailMessage();
                message.To.Add(mediaBuyerInfo.UserName);
                message.Body = bodyBuyer;
                message.Subject = "Booking ";
                message.From = new MailAddress(AppSettings.EmailConfig.FromAddress);
                message.IsBodyHtml = true;
                PdfConvertEngine pdfEngine = new PdfConvertEngine();
                var pdfPath = pdfEngine.ConvertHTMLToPDF(bodyBuyer, AppSettings.PDFPath, "INV_01_123");
                Attachment dataPDF = new Attachment(pdfPath);
                dataPDF = new Attachment(pdfPath);
                message.Attachments.Add(dataPDF);
                EmailSenderEngine emailEngine = new EmailSenderEngine();
                emailEngine.SendEmail(message, configJSON);
                dataPDF.Dispose();
                //end send email to media buyer

                return Ok(res);
            }
            catch (Exception ex)
            {

                res.Message = ex.Message;
                res.Response = false;

                return BadRequest(res);

            }

        }

        [Route("ViewBooked")]
        [HttpPost]
        public ActionResult<ViewArrayBookResponse> ViewBooked([FromBody] ViewBookInputModel data)
        {
            ViewArrayBookResponse res = new ViewArrayBookResponse();

            try
            {
                BookBL bl = new BookBL(DbContext);
                res = bl.GetBook(data);

                res.Message = "Success";
                res.Response = true;

                return  Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return BadRequest(res);
            }

        }

        [Route("DeleteFromBooked")]
        [HttpPost]
        public ActionResult<DeleteBookResponseModel> DeleteFromBooked([FromBody] DeleteBookInputModel data)
        {
            DeleteBookResponseModel res = new DeleteBookResponseModel();

            try
            {
                BookBL bl = new BookBL(DbContext);
                var respo = bl.Delete(data);

                res.Message = respo.Message;
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

        [Route("DeleteItemFromBooked")]
        [HttpPost]
        public ActionResult<DeleteItemBookResponseModel> DeleteItemFromBooked([FromBody] DeleteItemBookInputModel data)
        {
            DeleteItemBookResponseModel res = new DeleteItemBookResponseModel();

            try
            {
                BookBL bl = new BookBL(DbContext);
                var respo = bl.DeleteItem(data);

                res.Message = respo.Message;
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


        [Route("AddItemToCurrentBook")]
        [HttpPost]
        public ActionResult<AddItemToCurrentBookResponseModel> AddItemToCurrentBook([FromBody] AddItemToCurrentBookListInputModel data)
        {
            AddItemToCurrentBookResponseModel res = new AddItemToCurrentBookResponseModel();

            try
            {
                BookBL bl = new BookBL(DbContext);
                var temp = bl.ChangeItemCurrentBook(data);

                string configJSON = JsonConvert.SerializeObject(AppSettings.EmailConfig);

                //start send Email to Admin
                var bodyAdmin = bl.BuilBookChangeReceiptToAdmin(data);
                MailMessage message = new MailMessage();
                message.To.Add("enggarprathitaj@gmail.com");
                message.Body = bodyAdmin;
                message.Subject = "Booking ";
                message.From = new MailAddress(AppSettings.EmailConfig.FromAddress);
                message.IsBodyHtml = true;
                PdfConvertEngine pdfEngine = new PdfConvertEngine();
                var pdfPath = pdfEngine.ConvertHTMLToPDF(bodyAdmin, AppSettings.PDFPath, temp.BookNo.Replace('/', '_'));
                Attachment dataPDF = new Attachment(pdfPath);
                message.Attachments.Add(dataPDF);
                EmailSenderEngine emailEngine = new EmailSenderEngine();
                emailEngine.SendEmail(message, configJSON);
                dataPDF.Dispose();
                //End send Email to admin

                //start send Email to Media buyer
                var bodyBuyer = bl.BuilBookChangeReceiptToMediaBuyer(data);
                UserRepository userRepo = new UserRepository(DbContext);
                var mediaBuyerInfo = userRepo.FindByID(data.Data[0].UserID).FirstOrDefault();
                message = new MailMessage();
                message.To.Add(mediaBuyerInfo.UserName);
                message.Body = bodyBuyer;
                message.Subject = "Booking ";
                message.From = new MailAddress(AppSettings.EmailConfig.FromAddress);
                message.IsBodyHtml = true;
                pdfEngine = new PdfConvertEngine();
                pdfPath = pdfEngine.ConvertHTMLToPDF(bodyBuyer, AppSettings.PDFPath, temp.BookNo.Replace('/', '_'));
                dataPDF = new Attachment(pdfPath);
                message.Attachments.Add(dataPDF);
                emailEngine = new EmailSenderEngine();
                emailEngine.SendEmail(message, configJSON);
                dataPDF.Dispose();
                //End send Email to Media buyer


                res.data = temp;
                res.Message = "Success";
                res.Response = true;

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                res.Response = false;

                return BadRequest(res);
            }

        }


        [Route("SendNotifiExpired")]
        [HttpPost]
        public ActionResult<NotificationBookResponseModel> SendNotifiExpired([FromBody] NotificationBookInputModel data)
        {
            NotificationBookResponseModel res = new NotificationBookResponseModel();

            try
            {
                BookBL bl = new BookBL(DbContext);
                var temp = bl.GetExpiredBook(data);
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

        [Route("SendNotifiWhislistAvailable")]
        [HttpPost]
        public ActionResult<NotificationWishListResponseModel> SendNotifiWhislistAvailable([FromBody] NotificationWishListInput data)
        {
            NotificationWishListResponseModel res = new NotificationWishListResponseModel();

            try
            {
                WishListBL bl = new WishListBL(DbContext);
                var temp = bl.FindWishListAvailable(data.UserID);
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

        [Route("SendRateScore")]
        [HttpPost]
        public ActionResult<AddRateResponseModel> SendRateScore([FromBody] AddRateInputModel data)
        {
            AddRateResponseModel res = new AddRateResponseModel();

            try
            {
                RateBL bl = new RateBL(DbContext);
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


        [Route("CheckAvailableSite")]
        [HttpPost]
        public ActionResult<CheckDateAvailableResponseModel> CheckAvailableSite([FromBody] CheckDateAvailableInputModel data)
        {
            CheckDateAvailableResponseModel res = new CheckDateAvailableResponseModel();

            try
            {
                SiteBL bl = new SiteBL(DbContext);
                var temp = bl.CheckSiteAvailable(data);
                res.data = temp;

                res.Message = "Success";
                res.Response = true;

                return Ok(res);
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