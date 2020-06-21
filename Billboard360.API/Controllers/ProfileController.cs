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
using System.IO;

namespace Billboard360.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : Controller
    {
        protected readonly DatabaseContext DbContext;

        AppSettings AppSettings { get; }

        public ProfileController(DatabaseContext dbContext, AppSettings appSettings)
        {
            DbContext = dbContext;
            AppSettings = appSettings;
        }


        [Route("Login")]
        [HttpPost]
        public ActionResult<LoginResponseModel> Login([FromBody] LoginInputModel data)
        {

            try
            {
                LoginBL loginBL = new LoginBL(DbContext);
                return loginBL.Login(data.UserName, data.Password);
            }
            catch (Exception ex)
            {
                LoginResponseModel logres = new LoginResponseModel();
                logres.Message = ex.Message;
                logres.Response = false;

                return logres;
            }


        }

        [Route("ChangePassword")]
        [HttpPost]
        public ActionResult<ChangePasswordResponseModel> ChangePassword([FromBody] ChangePasswordInputModel data)
        {
            ChangePasswordBL bl = new ChangePasswordBL(DbContext);
            return bl.ChangePasswordWithOldPassword(data);
        }


        [Route("ChangePasswordForgot")]
        [HttpPost]
        public ActionResult<ChangePasswordResponseModel> ChangePasswordForgot([FromBody] ChangePasswordForgotInputModel data)
        {
            ChangePasswordBL bl = new ChangePasswordBL(DbContext);
            return bl.ChangePasswordWithoutOldPassword(data);
        }


        [Route("ForgotPassword")]
        [HttpPost]
        public ActionResult<ForgotPasswordResponseModel> ForgotPassword([FromBody] ForgotPasswordMobileInputModel data)
        {
            try
            {

                ForgotPasswordBL forPasswordBL = new ForgotPasswordBL(DbContext);

                var url = AppSettings.URLForgotPassword;
                var result = forPasswordBL.SendURL(data.UserName, url);

                if (result.Response)
                {

                    var locationHtml = AppSettings.TemplatePath + "EmailResetPassword.html";
                    string content = System.IO.File.ReadAllText(locationHtml);

                    content = content.Replace("$BASE_URL", AppSettings.BaseURL);
                    content = content.Replace("$ForgetPassword_URL", AppSettings.BaseURL + AppSettings.URLForgotPassword + result.data.ID);


                    MailMessage message = new MailMessage();

                    message.To.Add(result.data.UserName);
                    message.Body = content;
                    message.Subject = "Forgot Password";
                    message.From = new MailAddress(AppSettings.EmailConfig.FromAddress);
                    message.IsBodyHtml = true;


                    EmailSenderEngine emailEngine = new EmailSenderEngine();
                    string configOnJSON = JsonConvert.SerializeObject(AppSettings.EmailConfig);

                    emailEngine.SendEmail(message, configOnJSON);

                }

                return result;

            }
            catch (Exception ex)
            {
                ForgotPasswordResponseModel logres = new ForgotPasswordResponseModel();
                logres.Message = ex.Message;
                logres.Response = false;

                return logres;
            }

        }


        [Route("CheckValidIDForgotPassword")]
        [HttpPost]
        public ActionResult<ValidationForgotPasswodResponseModel> CheckValidIDForgotPassword([FromBody] ValidationForgotPasswordInputModel data)
        {
            try
            {

                ForgotPasswordBL forPasswordBL = new ForgotPasswordBL(DbContext);

                var url = AppSettings.URLForgotPassword;
                var result = forPasswordBL.CheckValidate(data);

                if (result.IsStillUse)
                {
                    ValidationForgotPasswodResponseModel response = new ValidationForgotPasswodResponseModel();

                    response.data = result;
                    response.Response = true;
                    response.Message = "Valid";

                    return response;

                } else
                {
                    ValidationForgotPasswodResponseModel response = new ValidationForgotPasswodResponseModel();

                    response.Response = false;
                    response.Message = "Waktu anda meminta reset password sudah tidak berlaku, lakukan permintaan ulang";

                    return response;
                }

                

            }
            catch (Exception ex)
            {
                ValidationForgotPasswodResponseModel logres = new ValidationForgotPasswodResponseModel();
                logres.Message = ex.Message;
                logres.Response = false;

                return logres;
            }

        }

        [Route("GetProfile")]
        [HttpPost]
        public ActionResult<ProfileResponseModel> GetProfile([FromBody] ProfileInputModel data)
        {
            try
            {
                ProfileBL profileBL = new ProfileBL(DbContext);
                var res = profileBL.GetCompleteProfie(data.UserID);
                res.PPNDefault = AppSettings.PPNDefault;

                ProfileResponseModel response = new ProfileResponseModel();
                
                response.data = res;
                response.Message = "Success";
                response.Response = true;

                return response;
            }
            catch (Exception ex)
            {
                ProfileResponseModel logres = new ProfileResponseModel();
                logres.Message = ex.Message;
                logres.Response = false;

                return logres;
            }

        }

        [Route("EditProfile")]
        [HttpPost]
        public ActionResult<EditProfileResponseModel> EditProfile([FromBody] EditProfileInputModel data)
        {
            try
            {
                EditProfileResponseModel res = new EditProfileResponseModel();

                ProfileBL bl = new ProfileBL(DbContext);

                var output = bl.EditProfile(data);

                res.data = output;
                res.Message = "Success update data";
                res.Response = true;

                return res;
            }
            catch (Exception ex)
            {
                EditProfileResponseModel logres = new EditProfileResponseModel();
                logres.Message = ex.Message;
                logres.Response = false;

                return logres;
            }

        }

        [Route("AddBankAccount")]
        [HttpPost]
        public ActionResult<AddBankAccountResponseMoodel> AddBankAccount([FromBody] AddBankAccountInputModel data)
        {
            BankAccountBL bl = new BankAccountBL(DbContext);
            return bl.Save(data);
        }

        [Route("EditBankAccount")]
        [HttpPost]
        public ActionResult<EditBankAccountResponseMoodel> EditBankAccount([FromBody] EditBankAccountInputModel data)
        {
            BankAccountBL bl = new BankAccountBL(DbContext);
            return bl.Edit(data);
        }

        [Route("GetUserBankAccount")]
        [HttpPost]
        public ActionResult<GetUserBankResponseModel> GetBank([FromBody] GetUserBankInputModel data)
        {
            GetUserBankResponseModel res = new GetUserBankResponseModel();
            try
            {
                BankBL posBL = new BankBL(DbContext);
                var result = posBL.GetUserBankAccount(data);

                res.data = result;

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

        [Route("DeleteBankAccount")]
        [HttpPost]
        public ActionResult<DeleteBankAccountResponseModel> DeleteBankAccount([FromBody] DeleteBankAccountInputModel data)
        {
            DeleteBankAccountResponseModel res = new DeleteBankAccountResponseModel();
            try
            {
                BankAccountBL posBL = new BankAccountBL(DbContext);
                var result = posBL.Delete(data);

                res.data = result;

                res.Message = result.Message;
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
    }
}