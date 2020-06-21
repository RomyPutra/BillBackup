using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billboard360.API.Models;
using Billboard360.DataAccess.Repositories;
using Billboard360.DataAccess;
using Billboard360.DataAccess.Entities;
using Billboard360.Helper;


namespace Billboard360.API.BussinessLogic
{
    public class ForgotPasswordBL
    {
        protected readonly DatabaseContext DbContext;

        public ForgotPasswordBL(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        public ForgotPasswordResponseModel SendURL(string userName, string URL)
        {
            Guid id = Guid.NewGuid();

            bool isSuccess = false;
            string message = "";

            LogForgotPassword temp = new LogForgotPassword();
            ForgotPasswordResponseModel responseModel = new ForgotPasswordResponseModel();
            ForgotPasswordMobileOutputModel outputModel = new ForgotPasswordMobileOutputModel();

            UserRepository userRepo = new UserRepository(DbContext);

            var userInfo = userRepo.FindByUserName(userName);

            if (userInfo != null && userInfo.ID != null && userInfo.ID != Guid.Empty)
            {
                LogForgotPasswordRepository repo = new LogForgotPasswordRepository(DbContext);
                temp.ID = id;
                temp.IsUsed = false;
                temp.UserName = userName;
                temp.DateRequest = DateTime.Now;
                temp.DateExpired = DateTime.Now.AddHours(3);
                temp.CreateByUserID = userInfo.ID;
                temp.CreateDate = DateTime.Now;

                var result = repo.Insert(temp);

                if (result.Result)
                {

                    isSuccess = result.Result;
                    message = result.Message;

                    outputModel.ID = result.ID;
                    outputModel.UserName = userName;
                    outputModel.URL = URL + result.ID.ToString();

                    responseModel.data = outputModel;
                }

                responseModel.Message = message;
                responseModel.Response = isSuccess;

            }
            else
            {
                responseModel.Message = "Can't find Username";
                responseModel.Response = false;
            }

            return responseModel;


        }

        public ValidationForgotPasswordOutputModel CheckValidate(ValidationForgotPasswordInputModel data)
        {
            LogForgotPasswordRepository logRepo = new LogForgotPasswordRepository(DbContext);
            ValidationForgotPasswordOutputModel output = new ValidationForgotPasswordOutputModel();

            var isValid = logRepo.IsValid(data.ForgotID);

            if (isValid)
            {
                var logInfo = logRepo.GetLogForgotPasswordByID(data.ForgotID).FirstOrDefault();

                UserRepository userRepo = new UserRepository(DbContext);

                var userInfo = userRepo.FindByID(logInfo.CreateByUserID).FirstOrDefault();


                if (userInfo != null)
                {
                    output.IsStillUse = true;
                    output.UserID = userInfo.ID;
                    output.UserName = userInfo.UserName;
                }

                return output;
                
            } else
            {
                return output;
            }

        }
    }
}
